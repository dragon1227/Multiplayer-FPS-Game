using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager GMInstance;

    public RoundSettings rs;

    void getInstance() // Get an instance of this!
    {
        if(GMInstance == null)
        {
            GMInstance = this;
        } else {
            return; // if there already is an instance exit we can only have one (singleton)
        }
    }


    /*  Manage Players will manage the addition and removal of players from the game
        We Collapse all this stuff into a region which allows us to visually tidy our code.
        Please click the + / - to the left to expand / collapse 
    */
    #region Manage Players

    private const string PLAYER_ID = "Player ";

    // Dictionary containing all players...
    private static Dictionary<string, PlayerManager> playersDict = new Dictionary<string, PlayerManager>();

    public static void AddPlayer(string networkId, PlayerManager player)
    {  
        playersDict.Add(PLAYER_ID + networkId, player); // Delegate task to the Dictionary class
        player.transform.name = PLAYER_ID + networkId; //assing the name of  the player to the id
    }// add a player to the game

    public static void RemovePlayer(string pid)
    {
        playersDict.Remove(pid); // Delegate task to the Dictionary class
    }// remove a player from the game

    public static PlayerManager GetPlayer(string pid)
    {
        return playersDict[pid];
    }//get player from the dictionary of players

    #endregion

}//class
