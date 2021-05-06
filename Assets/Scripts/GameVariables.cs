using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameVariables
{
    static float Volume=0.2f;
    static Resolution resolution;
    public static void setVolume(float vol)
    {
        Volume = vol;
    }
    public static void setResolution(Resolution resol)
    {
        resolution = resol;
    }
    public static float getVolume()
    {
        return Volume;
    }
    public static Resolution getResolution()
    {
        return resolution;
    }
    public static void callSetup(int gold, int kills)
    {
        GameObject.Find("GameOverScreen").GetComponent<GameOverScreen>().Setup(gold, kills);
    }
}
