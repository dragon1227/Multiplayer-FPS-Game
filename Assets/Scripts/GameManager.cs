using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private const string PLAYER_ID = "Player ";

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

}//class
