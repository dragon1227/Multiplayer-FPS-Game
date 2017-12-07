using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerManager))]
public class Setup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] disableComponents;

    [SerializeField]
    string name = "RemotePlayer";

    private Camera globalCamera;

    [SerializeField]
    string ignoreLayerNamed = "IgnoreMe";

    [SerializeField]
    GameObject playerUI;

    private GameObject UIInstance;

    // Use this for initialization
    void Start () {

        if (!isLocalPlayer) { // if we are not controlled by the player 
            DisableComponents();
            SetRemoteLayer();
        } else {
            SetCamera();

            // create the player ui, crosshair, health, ammo etc
            UIInstance = Instantiate(playerUI);
            UIInstance.name = playerUI.name;
        }//if else if
        GetComponent<PlayerManager>().Setup();
	}//start


    public override void OnStartClient()
    {
        base.OnStartClient(); // called when a local client is setup

        GameManager.AddPlayer(GetComponent<NetworkIdentity>().netId.ToString(), GetComponent<PlayerManager>());
    }

    void SetCamera()
    {
        globalCamera = Camera.main; // the global camera is the main camera
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

        Destroy(UIInstance);
        if(globalCamera != null)
        {
            globalCamera.gameObject.SetActive(true);
        }// if 

        GameManager.RemovePlayer(transform.name);
    }// on disable
}
