using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

// This is a service script which handles the join game menu, and network functionality
// a player will be able to view and join games, get an upto date list
// using match making & the network manager. . . 


public class JoinGame : MonoBehaviour
{

    List<GameObject> ls = new List<GameObject>();

    [SerializeField]
    private Text status;

    [SerializeField]
    private GameObject listItemPrefab;

    [SerializeField]
    private Transform listParent;

    private NetworkManager netMgr;

    void Start()
    {
        netMgr = NetworkManager.singleton;
        if (netMgr.matchMaker == null)
        {
            netMgr.StartMatchMaker();
        }

        RefreshList();
    }
    //This contains all the match list stuff
    #region Match list stuff
    public void RefreshList()
    {
        ClearList();// clear the list

        if (netMgr.matchMaker == null)
        {
            netMgr.StartMatchMaker(); // create a match maker if one doesn't exist already
        }

        netMgr.matchMaker.ListMatches(0, 10, "", true, 0, 0, OnListMatches); // list the matches available
        status.text = "Loading...";
    }

    public void OnListMatches(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        status.text = "";

        if (!success || matchList == null) //this will show if theres a network issue on the client side...
        {
            status.text = "Couldn't get list...";
            return;
        }

        foreach (MatchInfoSnapshot m in matchList)
        {
            GameObject listItemObj = Instantiate(listItemPrefab); // create an instance of the prefab object
            listItemObj.transform.SetParent(listParent);//place the list item obj in the parent object (the list)

            RoomListItem rli = listItemObj.GetComponent<RoomListItem>();
            if (rli != null)
            {
                rli.Setup(m, JoinRoom);
            }//if theres a room list item 

            ls.Add(listItemObj);
        }//for each match in the list of matches

        if (ls.Count == 0)
        {
            status.text = "No rooms available";
        }
    }

    void ClearList()
    {
        for (int i = 0; i < ls.Count; i++)
        {
            Destroy(ls[i]); // destroy each item in the list 1 by 1
        }

        ls.Clear(); // and clear it
    }
    #endregion

    public void JoinRoom(MatchInfoSnapshot match)
    {
        netMgr.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, netMgr.OnMatchJoined);
        ClearList();
        status.text = "Joining: " + match.name;
    }

   

}