using UnityEngine;

[System.Serializable] // unity will know how to deal with this class
public class Weapon  {
    [SerializeField]
    private string name = "smg";
    [SerializeField]
    private int damage = 10;
    [SerializeField]
    private int range = 200;

    public float fireRate = 0f;
    public GameObject gfx;

    public int getDamage()
    {
        return this.damage;
    }

    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    public int getRange()
    {
        return this.range;
    }

    public void setRange(int range)
    {
        this.range = range;
    }

    public string getName()
    {
        return this.name;
    }

    public void setName(string name)
    {
        this.name = name;
    }
}
