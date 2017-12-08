using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

class RoomListItem : MonoBehaviour {

    public delegate void JoinRoomDelegate(MatchInfoSnapshot match);
    private JoinRoomDelegate jrc;

    private MatchInfoSnapshot match;

    [SerializeField]
    private Text nameText;

    public void Setup(MatchInfoSnapshot m, JoinRoomDelegate jrd) 
    {
        match = m;

        jrc = jrd;

        nameText.text = match.name + " (" + match.currentSize + "/" + match.maxSize + ")";

    }

    public void JoinRoom()
    {
        jrc.Invoke(match);
    }
}

