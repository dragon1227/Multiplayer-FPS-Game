using UnityEngine;

[System.Serializable] // unity will know how to deal with this class
public class Weapon  {

    private string name = "smg";
    private int damage = 10;
    private int range = 75;

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
