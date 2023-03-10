using UnityEngine;
using UnityEngine.Networking;

/*  The following will confugure our service
    this will be a match making web service which will allow players to join into available rooms
 */

public class Host : MonoBehaviour
{
    [SerializeField]
    private uint playerCap = 8;
    private string roomName;
    private NetworkManager netMgr;

    private void Start()
    {
        // create a match making system
        netMgr = NetworkManager.singleton;
        if (netMgr.matchMaker == null) // check to see if we haven't already created a match maker
        {
            netMgr.StartMatchMaker();   // create one
        }
    } // start


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
            netMgr.matchMaker.CreateMatch(roomName, playerCap, true, "", "", "", 0, 0, netMgr.OnMatchCreate);

        }
    }// create room


    public void SetRoomName(string roomName)
    {
        this.roomName = roomName;
    }
}
