using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

// This is a service script which handles the join game menu, and network functionality
// a player will be able to view and join games, get an upto date list
// using match making & the network manager. . . 

// TODO: there is currently an issue with the ui which is disabling this from functioning as expected

public class JoinGame : MonoBehaviour
{

    private NetworkManager netManager;
    private List<GameObject> roomList = new List<GameObject>();

    [SerializeField]
    private Text status;

    [SerializeField]
    private GameObject listItemPrefab;

    [SerializeField]
    private Transform listParent;

    // Use this for initialization
    void Start()
    {

        netManager = NetworkManager.singleton;
        if (netManager.matchMaker == null)
        {
            netManager.StartMatchMaker();
        }
        Refresh();
    }

    public void Refresh()
    {
        ClearList();
        netManager.matchMaker.ListMatches(0, 10, "", true, 0, 0, netManager.OnMatchList);
        status.text = "Loading . . .";
    }

    public void onMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        status.text = "";

        if (!success || matchList == null)
        {
            status.text = "Couldn't find any games :(";
            return;
        }

        foreach (MatchInfoSnapshot match in matchList)
        {
            GameObject listItemGO = Instantiate(listItemPrefab);
            listItemGO.transform.SetParent(listParent);

            RoomListItem rli = GetComponent<RoomListItem>();
            if (rli != null)
            {
                rli.Setup(match, JoinRoom);
            }

            roomList.Add(listItemGO);
        }
        if (roomList == null)
        {
            status.text = "No games available right now";
        }
    }

    void ClearList()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }
        roomList.Clear();
    }

    public void JoinRoom(MatchInfoSnapshot match)
    {
        netManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, netManager.OnMatchJoined);

    }

}
