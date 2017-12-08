using UnityEngine;
using UnityEngine.Networking;

// the following defines functionality related to shooting
// it will make rpc calls to the server to update the information


[RequireComponent (typeof (WeaponManger))]
public class Shoot : NetworkBehaviour {

    private const string PLAYER_TAG = "Player";

    private Weapon weapon;
    private WeaponManger weapMgr;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private LayerMask mask;

    // Use this for initialization
    void Start () {
		if(camera == null)
        {
            this.enabled = false;
        }// if

        weapMgr = GetComponent<WeaponManger>();      // instantiate the weapon manager
	}
	
	// Update is called once per frame
	void Update () {
        weapon = weapMgr.getCurrent();       //get the weapon we are using from the weapon manager

        if (weapon.fireRate <= 0f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Fire();
            }// if we press the fire button 
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Fire", 0f, 1f / weapon.fireRate);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Fire");
            }
        }

	}

    [Client]    //only called by the client never by the server
    void Fire()
    {
        Debug.Log("Player shot");
        RaycastHit hit; // stores information about objects that we hit
        // starting position, direction we are firing, cast out a raycast, the max distance, objects that we can hit
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, weapon.range, mask))
        {
            Debug.Log(hit.collider.name);// return the name of what we hit
            if(hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerHit(hit.collider.name, weapon.damage);
            }
        }
    }

    [Command] // called by the server
    void CmdPlayerHit(string pid, int damage)
    {
        PlayerManager p = GameManager.GetPlayer(pid); // get the player that was shot at
        p.RpcDamagePlayer(damage);
    }// player hit command


}
