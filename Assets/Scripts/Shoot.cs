using UnityEngine;
using UnityEngine.Networking;

public class Shoot : NetworkBehaviour {

    public Weapon weapon;
    private const string PLAYER_TAG= "Player";

    [SerializeField]
    private Camera c;

    [SerializeField]
    private LayerMask mask;

    // Use this for initialization
    void Start () {
		if(c == null)
        {
            this.enabled = false;
        }// if
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }// if we press the fire button 
	}

    [Client]    //only called by the client never by the server
    void Fire()
    {
        RaycastHit hit; // stores information about objects that we hit
        // starting position, direction we are firing, cast out a raycast, the max distance, objects that we can hit
        if (Physics.Raycast(c.transform.position, c.transform.forward, out hit, weapon.getRange(), mask))
        {
            Debug.Log(hit.collider.name);// return the name of what we hit
            if(hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerHit(hit.collider.name, weapon.getDamage());
            }
        }
    }

    [Command] // called by the server
    void CmdPlayerHit(string pid, int damage)
    {
        PlayerManager p = GameManager.GetPlayer(pid);
        p.RpcDamagePlayer(damage);
    }// player hit command
}
