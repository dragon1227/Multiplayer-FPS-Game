using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// Network Behaviour is derived from momobehaviour which is a derivative of behaviour
public class PlayerManager : NetworkBehaviour {

    [SerializeField]
    private Behaviour[] disableIfDead;
    private bool[] isDisabled;

    [SerializeField]
    private int initHealth = 100;

    [SyncVar]  // asyncronously updates the health across the clients from the server
    private int health;

    [SyncVar] // asyncronously updates the health across the clients from the server
    private bool dead = false;
    public bool isDead {                // accsessor methods
        get { return dead; }            // anyone can get this
        protected set { dead = value; } // only derived classes can change this 
    }// isDead accessor methods end

    /* Player setup and initialisation
    We Collapse all this stuff into a region which allows us to visually tidy up our code.
    Please click the + / - to the left to expand / collapse
    */
    #region Setup / Player Initialisation
    // Use this for initialization
    public void PlayerSetup() {
        CmdPlayerSetup();
	}

    [Command]
    private void CmdPlayerSetup()
    {
        RpcPlayerSetup();
    }

    [ClientRpc]
    private void RpcPlayerSetup()
    {
        isDisabled = new bool[disableIfDead.Length];    // initialize the array

        for (int i = 0; i < isDisabled.Length; i++)
        {
            isDisabled[i] = disableIfDead[i].enabled;   //set the boolean value of the components
        }
        initializePlayer();
    }

    // set the defaults for a player
    void initializePlayer()
    {
        dead = false;
        health = initHealth;

        for(int i = 0; i < disableIfDead.Length; i++)
        {
            disableIfDead[i].enabled = isDisabled[i];   // set all values to true
        }

        Collider col = GetComponent<Collider>();
        if(col != null)
        {
            col.enabled = true;                         // enable the collider
        }
        
    }// init player
    #endregion

    /*  The health related logic deals with damaging, player death & respawning the player
    We Collapse all this stuff into a region which allows us to visually tidy up our code.
    Please click the + / - to the left to expand / collapse
    */
    #region Health related logic

    [ClientRpc] // called by server, and then invoked on corresponding GameObjects on clients connected to the server.
    public void RpcDamagePlayer(int damage)
    {
        if (dead) { return;}                            // the player cannot be damaged if they're already dead

        health -= damage;

        Debug.Log(transform.name + " has " + health);

        if (health <= 0)
        {
            KillPlayer();                               // if the player has no health call the kill method
        }
        
        
    }//damage the player

    private void KillPlayer()
    {
        Collider col = GetComponent<Collider>();
        dead = true;

        for(int i = 0; i < disableIfDead.Length; i++)
        {
            disableIfDead[i].enabled = false;           // disable components when dead
        }
        
        if (col != null)
        {
            col.enabled = false;                        // disable the collider 
        }

        //start a co-routine to respawn the player
        StartCoroutine(Respawn());
    }// if the player is dead then disable components & respawn

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.GMInstance.rs.spawnDelay); // set the respawn time 

        initializePlayer();
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
    }

#endregion

}// class
