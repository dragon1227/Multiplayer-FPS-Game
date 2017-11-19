using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour {

    [SerializeField]
    private int initHealth = 100;

    [SerializeField]
    private Behaviour[] disableIfDead;
    private bool[] isDisabled;

    [SyncVar]  // asyncronously updates the health across the clients from the server
    private int health;

    [SyncVar] // asyncronously updates the health across the clients from the server
    private bool dead = false;
    public bool isDead {                // accsessor methods
        get { return dead; }            // anyone can get this
        protected set { dead = value; } // only derived classes can change this 
    }// isDead accessor methods end



    // Use this for initialization
    public void Setup() {

        isDisabled = new bool[disableIfDead.Length];    // initialize the array

        for (int i = 0; i < isDisabled.Length; i++)
        {
            isDisabled[i] = disableIfDead[i].enabled;   //set the boolean value of the components
        }
        initializePlayer();
	}

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
        
    }

    [ClientRpc] // called by server, and then invoked on corresponding GameObjects on clients connected to the server.
    public void RpcDamagePlayer(int damage)
    {
        if(dead) { return; }                            // the player cannot be damaged if they're already dead

        health -= damage;                           

        if (health <= 0)
        {
            KillPlayer();                               // if the player has no health call the kill method
        }
        
        Debug.Log(transform.name + " has " + health);  
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

    }// if the player is dead then disable components & respawn
	

}// class
