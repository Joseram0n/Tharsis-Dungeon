using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameVariables
{
    static float Volume = 0.5f;
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
}
