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

}
