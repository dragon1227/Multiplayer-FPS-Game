using UnityEngine;
using UnityEngine.UI;

// the following script will update the ui that the player will see on screen
public class PlayerUI : MonoBehaviour
{

    [SerializeField]
    RectTransform healthBar;

    [SerializeField]
    Text ammoText;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject scoreboard;

    private PlayerManager plyrMgr;
    private PlayerController ctrl;
    private WeaponManger weapMgr;

    public void SetPlayer(PlayerManager p)
    {
        plyrMgr = p; 
        ctrl = plyrMgr.GetComponent<PlayerController>();        // assign the controller
        weapMgr = plyrMgr.GetComponent<WeaponManger>();       // assign the weapon manager
    }

    void Start()
    {
        PauseMenu.isActive = false;                     // disable the pause menu at the begining
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseActive();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scoreboard.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreboard.SetActive(false);
        }
    }

    public void PauseActive()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseMenu.isActive = pauseMenu.activeSelf;
    }

    void SetHealthAmount(float _amount)
    {
        healthBar.localScale = new Vector3(1f, _amount, 1f);
    }

    void SetAmmoAmount(int _amount)
    {
        ammoText.text = _amount.ToString();
    }

}