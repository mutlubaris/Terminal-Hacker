using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    string[] level1Passwords = { "apple", "nice", "print", "lucky", "easy" };
    string[] level2Passwords = { "sandwich", "dinosaur", "address", "bicycle", "amazing" };
    string[] level3Passwords = { "dictionary", "dolphin", "spectacular", "unbelievable", "skyscraper" };
    int level;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;
    string password;

    // Start is called before the first frame updated
    void Start()
    {
        ShowMainMenu();
    }

    void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("1  for your neighbor's home network");
        Terminal.WriteLine("2  for the local police station");
        Terminal.WriteLine("3  for National Intelligence Agency");
        Terminal.WriteLine("Please enter your selection:");
    }

    void OnUserInput(string input)
    {
        if (input == "menu")
        {
            ShowMainMenu();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            AskForPassword();
        }
        else
        {
            Terminal.WriteLine("Please choose a valid level");
        }
    }

    void AskForPassword()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine("Enter your password, hint: " + password.Anagram());
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                password = level3Passwords[Random.Range(0, level3Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }
    }

    void CheckPassword(string input)
    {
        if (input==password)
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    void ShowLevelReward()
    {
        switch(level)
        {
            case 1:
                Terminal.WriteLine(@"

    ___________      ____  _____
   / ____/__  /     / __ \/__  /
  / __/    / /     / /_/ /  / / 
 / /___   / /__   / ____/  / /__
/_____/  /____/  /_/      /____/
"
                );
                Terminal.WriteLine("Type 'menu' to restart");
                break;
            case 2:
                Terminal.WriteLine(@"

   __________     ____  _   __
  / ____/ __ \   / __ \/ | / /
 / / __/ / / /  / / / /  |/ / 
/ /_/ / /_/ /  / /_/ / /|  /  
\____/\____/   \____/_/ |_/   
"
                );
                Terminal.WriteLine("Type 'menu' to restart");
                break;
            case 3:
                Terminal.WriteLine(@"

   ____________   _       ______ 
  / ____/ ____/  | |     / / __ \
 / / __/ / __    | | /| / / /_/ /
/ /_/ / /_/ /    | |/ |/ / ____/ 
\____/\____/     |__/|__/_/      
"
                );
                Terminal.WriteLine("Type 'menu' to restart");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
