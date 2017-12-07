using UnityEngine;

[System.Serializable] // unity will know how to deal with this class
public class Weapon  {

    public string name = "AR";

    public int damage = 35;
    public float range = 150f;

    public float fireRate = 0f;

    public int maxBullets = 30;
    [HideInInspector]
    public int bullets;

    public float reloadTime = 1f;

    public GameObject gfx;

    public Weapon()
    {
        bullets = maxBullets;
    }

}
