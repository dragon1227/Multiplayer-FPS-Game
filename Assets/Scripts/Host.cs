using UnityEngine;
using UnityEngine.Networking;

/*  The following will confugure our service
    this will be a match making web service which will allow players to join into available rooms
 */

public class Host : MonoBehaviour
{

    private uint playerCap = 8;
    private string roomName;
    private NetworkManager nm;

    private void Start()
    {
        // create a match making system
        nm = NetworkManager.singleton;
        if (nm.matchMaker == null)
        {
            nm.StartMatchMaker();
        }
    }
    //  Use this function to create a new match. The client which calls this function becomes the host of the match.
    //  When creating a match you should call this function and wait for the callback to be invoked before proceeding.
    //  The callback will indicate if the call was successful as well as extended information if it failed. 
    //  After receiving the response callback you then should call StartHost() with the passed in MatchInfo.
    //  Reference : https://docs.unity3d.com/ScriptReference/Networking.Match.NetworkMatch.CreateMatch.html
    public void CreateRoom()
    {
        //creat a room
        if(roomName != "" && roomName != null)
        {
            Debug.Log("Creating: " + roomName + " Player cap: " + playerCap);
            // matchName, matchSize, matchAdvertise, matchPassword, publicClientAddress, privateClientAddress, eloScoreForMatch, requestDomain, callback
            nm.matchMaker.CreateMatch(roomName, playerCap, true, "", "", "", 0, 0, nm.OnMatchCreate);

        }
    }


    public void SetRoomName(string roomName)
    {
        this.roomName = roomName;
    }
    public string GetRoomName()
    {
        return this.roomName;
    }
}
