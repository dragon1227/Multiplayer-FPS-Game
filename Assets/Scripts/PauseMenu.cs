using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

// the following contains all code relating to the pause menu, adds functionality to safely remove the player from the room

public class PauseMenu : MonoBehaviour {
    public static bool isActive = false;

    private NetworkManager netMgr;

    private void Start()
    {
        netMgr = NetworkManager.singleton;
    }

    public void Leave()
    {
        MatchInfo matchInfo = netMgr.matchInfo; 
        netMgr.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, netMgr.OnDropConnection); // this will safely bring us back to the lobby 
        netMgr.StopHost();// cleans the scene correctly (does not stop the actual host) 
    }
}
