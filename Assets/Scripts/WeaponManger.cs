using UnityEngine;
using UnityEngine.Networking;

public class WeaponManger : NetworkBehaviour {
    [SerializeField]
    private Transform slot;
    [SerializeField]
    private Weapon primary;
    private Weapon current;
    private string weaponLayerName = "gun";



    // Use this for initialization
    void Start () {
        EquipWeapon(primary);
	}
	
    void EquipWeapon(Weapon weapon)
    {
        current = weapon;
        GameObject weaponInstance = (GameObject) Instantiate(primary.gfx, slot.position, slot.rotation);
        weaponInstance.transform.SetParent(slot);

        if (isLocalPlayer)
        {
            weaponInstance.layer = LayerMask.NameToLayer(weaponLayerName);
        }
    }

    public Weapon getCurrent()
    {
        return this.current;
    }
}
