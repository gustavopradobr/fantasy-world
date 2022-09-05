using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDataSerialize
{
    public int goldCoins = 0;
    public int skinIndex = 0;
    public int checkpoint = 0;

    public bool[] hairAvailable = { true, false, false };
    public bool[] torsoAvailable = { true, false, false };
    public bool[] armsAvailable = { true, false, false };
    public bool[] legsAvailable = { true, false, false };
    public bool[] swordAvailable = { true, false, false };

    public int hairEquip = 0;
    public int torsoEquip = 0;
    public int armsEquip = 0;
    public int legsEquip = 0;
    public int swordEquip = 0;

    public GameDataSerialize(GameData gameData)
    {
        goldCoins = gameData.goldCoins;
        skinIndex = gameData.skinIndex;
        checkpoint = gameData.checkpoint;

        hairAvailable = gameData.hairAvailable;
        torsoAvailable = gameData.torsoAvailable;
        armsAvailable = gameData.armsAvailable;
        legsAvailable = gameData.legsAvailable;
        swordAvailable = gameData.swordAvailable;

        hairEquip = gameData.hairEquip;
        torsoEquip = gameData.torsoEquip;
        armsEquip = gameData.armsEquip;
        legsEquip = gameData.legsEquip;
        swordEquip = gameData.swordEquip;
    }
    public GameDataSerialize()
    {

    }
}
