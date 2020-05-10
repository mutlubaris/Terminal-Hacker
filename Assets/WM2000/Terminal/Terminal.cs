using UnityEngine;
using System.Reflection;
using System;

public class Terminal : MonoBehaviour
{
    DisplayBuffer displayBuffer;
    InputBuffer inputBuffer;

    static Terminal primaryTerminal;

    private void Awake()
    {
        if (primaryTerminal == null) { primaryTerminal = this; } // Be the one
        inputBuffer = new InputBuffer();
        displayBuffer = new DisplayBuffer(inputBuffer);
        inputBuffer.onCommandSent += NotifyCommandHandlers;
    }

    public string GetDisplayBuffer(int width, int height)
    {
        return displayBuffer.GetDisplayBuffer(Time.time, width, height);
    }

    public void ReceiveFrameInput(string input)
    {
        inputBuffer.ReceiveFrameInput(input);
    }

    public static void ClearScreen()
    {
        primaryTerminal.displayBuffer.Clear();
    }

    public static void WriteLine(string line)
    {
        primaryTerminal.displayBuffer.WriteLine(line);
    }

    internal static void Writeline(string v)
    {
        throw new NotImplementedException();
    }

    public void NotifyCommandHandlers(string input)
    {
        var allGameObjects = FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour mb in allGameObjects)
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Instance;
            var targetMethod = mb.GetType().GetMethod("OnUserInput", flags);
            if (targetMethod != null)
            {
                object[] parameters = new object[1];
                parameters[0] = input;
                targetMethod.Invoke(mb, parameters);
            }
        }
    }
}