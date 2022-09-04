using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class LoadArrayHelper
{
    public static int[] LoadArray(int[] gamedata, int[] savedData)
    {
        if (savedData == null)
            return gamedata;

        for (int i = 1; i < gamedata.Length; i++)
        {
            if (i < savedData.Length)
                gamedata[i] = savedData[i];
            else
                gamedata[i] = 0;
        }
        return gamedata;
    }

    public static float[] LoadArray(float[] gamedata, float[] savedData)
    {
        if (savedData == null)
            return gamedata;

        for (int i = 1; i < gamedata.Length; i++)
        {
            if (i < savedData.Length)
                gamedata[i] = savedData[i];
            else
                gamedata[i] = 0;
        }
        return gamedata;
    }

    public static bool[] LoadArray(bool[] gamedata, bool[] savedData, bool defaultBool)
    {
        if(savedData == null)
            return gamedata;

        bool[] returnData = new bool[gamedata.Length];

        for (int i = 0; i < gamedata.Length; i++)
        {
            if (i < savedData.Length)
                returnData[i] = savedData[i];
            else
                returnData[i] = defaultBool;
        }
        return returnData;
    }
}
