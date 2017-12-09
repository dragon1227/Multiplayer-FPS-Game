using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountManager : MonoBehaviour {

    public static AccountManager instance;

    public static bool isLoggedIn { get; protected set; }

    //These store the username and password of the player when they have logged in
    public static string playerUsername { get; protected set; }
    private static string playerPassword = "";

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void LogOut()
    {
        playerUsername = "";
        playerPassword = "";
        isLoggedIn = false;
    }

    public void LogIn(string un, string pw)
    {
        playerUsername = un;
        playerPassword = pw;
        isLoggedIn = true;
    }

    //IEnumerator GetData()
    //{
    //    IEnumerator e = GetUserData(playerUsername, playerPassword); // << Send request to get the player's data string. Provides the username and password
    //    while (e.MoveNext())
    //    {
    //        yield return e.Current;
    //    }
    //    string response = e.Current as string; // << The returned string from the request

    //    if (response == "Error")
    //    {
    //        //There was another error. Automatically logs player out. This error message should never appear, but is here just in case.
    //        ResetAllUIElements();
    //        playerUsername = "";
    //        playerPassword = "";
    //        loginParent.gameObject.SetActive(true);
    //        loadingParent.gameObject.SetActive(false);
    //        Login_ErrorText.text = "Error: Unknown Error. Please try again later.";
    //    }
    //    else
    //    {
    //        //The player's data was retrieved. Goes back to loggedIn UI and displays the retrieved data in the InputField
    //        loadingParent.gameObject.SetActive(false);

    //    }
    //}
    //IEnumerator SetData(string data)
    //{
    //    IEnumerator e = DCF.SetUserData(playerUsername, playerPassword, data); // << Send request to set the player's data string. Provides the username, password and new data string
    //    while (e.MoveNext())
    //    {
    //        yield return e.Current;
    //    }
    //    string response = e.Current as string; // << The returned string from the request

    //    if (response == "Success")
    //    {
    //        //The data string was set correctly. Goes back to LoggedIn UI
    //        loadingParent.gameObject.SetActive(false);

    //    }
    //    else
    //    {
    //        //There was another error. Automatically logs player out. This error message should never appear, but is here just in case.
    //        ResetAllUIElements();
    //        playerUsername = ""; 
    //        playerPassword = "";
    //        loginParent.gameObject.SetActive(true);
    //        loadingParent.gameObject.SetActive(false);
    //        Login_ErrorText.text = "Error: Unknown Error. Please try again later.";
    //    }
    }


