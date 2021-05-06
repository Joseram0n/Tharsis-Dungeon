using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerScore: System.IComparable<PlayerScore>
{
    public int monedas;
    public int kills;
    public string name;
    public PlayerScore(int monedas, int kills, string name)
    {
        this.monedas = monedas;
        this.kills = kills;
        this.name = name;
    }
    public int getMonedas()
    {
        return monedas;
    }
    public int getKills()
    {
        return kills;

    }
    public string getName()
    {
        return name;
    }

    override
    public string ToString()
    {
        return "" + monedas + "-" + kills + "-" + name;
    }

    public int CompareTo(PlayerScore sc)
    {
        if (sc == null)
            return 1;
        else
        {
            int puntuacion = sc.getMonedas() + sc.getKills();
            int mypt = monedas + kills;
            return (puntuacion.CompareTo(mypt));
        }
    }
}
