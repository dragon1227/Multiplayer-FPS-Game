using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

// Create a list of rooms on the server

class RoomListItem : MonoBehaviour {

    public delegate void JoinRoomDelegate(MatchInfoSnapshot match);
    private JoinRoomDelegate joinRoomCallback;

    private MatchInfoSnapshot match;

    [SerializeField]
    private Text nameText;

    public void Setup(MatchInfoSnapshot m, JoinRoomDelegate joinRoomDelegate) //put the join room function in as a paaram
    {
        match = m;

        joinRoomCallback = joinRoomDelegate; // delecate the task else where 

        nameText.text = match.name + " (" + match.currentSize + "/" + match.maxSize + ")"; // show the room information

    }

    public void JoinRoom()
    {
        joinRoomCallback.Invoke(match); // invoce the join room callback on match x
    }
}

