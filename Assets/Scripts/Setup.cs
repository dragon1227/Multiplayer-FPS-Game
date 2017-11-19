using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerManager))]
public class Setup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] disableComponents;

    [SerializeField]
    string name = "RemotePlayer";

    private Camera globalCamera;

    // Use this for initialization
    void Start () {

        if (!isLocalPlayer) { // if we are not controlled by the player 
            DisableComponents();
            SetRemoteLayer();
        } else {
            SetCamera();
        }//if else if
        GetComponent<PlayerManager>().Setup();
	}//start
	
	// Update is called once per frame
	void Update () {
		
	}// update

    public override void OnStartClient()
    {
        base.OnStartClient(); // called when a local client is setup

        GameManager.AddPlayer(GetComponent<NetworkIdentity>().netId.ToString(), GetComponent<PlayerManager>());
    }

    void SetCamera()
    {
        globalCamera = Camera.main; // otherwise the global camera is the main camera
        if (globalCamera != null)
        {
            globalCamera.gameObject.SetActive(false);
        }//inner if
    }

    void DisableComponents()
    {
        for (int i = 0; i < disableComponents.Length; i++)
        {
            disableComponents[i].enabled = false; //Disable components that are not neccessary to the player
        }//for
    }

    void SetRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(name);
    }

    void OnDisable()
    {
        if(globalCamera != null)
        {
            globalCamera.gameObject.SetActive(true);
        }// if 

        GameManager.RemovePlayer(transform.name);
    }// on disable
}
