using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class PauseMenu : MonoBehaviour {
    public static bool active = false;

    private NetworkManager netMgr;

    private void Start()
    {
        netMgr = NetworkManager.singleton;
    }

    public void Leave()
    {
        MatchInfo matchInfo = netMgr.matchInfo;
        netMgr.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, netMgr.OnDropConnection);
        netMgr.StopHost();// cleans the scene correctly (does not stop the actual host) will allow for host migration also
    }
}
