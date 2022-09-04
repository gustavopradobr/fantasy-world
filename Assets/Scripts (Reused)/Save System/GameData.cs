using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public int goldCoins = 0;
    public int skinIndex = 0;
    public int checkpoint = 0;

    public bool[] hairAvailable;
    public bool[] armsAvailable;
    public bool[] torsoAvailable;
    public bool[] legsAvailable;
    public bool[] swordAvailable;

    public int hairEquip = 0;
    public int armsEquip = 0;
    public int torsoEquip = 0;
    public int legsEquip = 0;
    public int swordEquip = 0;

    private void Awake()
    {
        GameDataSerialize data = SaveSystem.LoadGameData(this);

        goldCoins = data.goldCoins;
        skinIndex = data.skinIndex;
        checkpoint = data.checkpoint;

        hairAvailable = LoadArrayHelper.LoadArray(hairAvailable, data.hairAvailable, false);
        armsAvailable = LoadArrayHelper.LoadArray(armsAvailable, data.armsAvailable, false);
        torsoAvailable = LoadArrayHelper.LoadArray(torsoAvailable, data.torsoAvailable, false);
        legsAvailable = LoadArrayHelper.LoadArray(legsAvailable, data.legsAvailable, false);
        swordAvailable = LoadArrayHelper.LoadArray(swordAvailable, data.swordAvailable, false);

        hairEquip = data.hairEquip;
        armsEquip = data.armsEquip;
        torsoEquip = data.torsoEquip;
        legsEquip = data.legsEquip;
        swordEquip = data.swordEquip;

        CheckEquippedIfAvailable();
    }

    public void SaveGameData()
    {
        SaveSystem.SaveGameData(this);
    }

    private void CheckEquippedIfAvailable()
    {
        if (!hairAvailable[hairEquip])
            hairEquip = 0;
        if (!armsAvailable[armsEquip])
            armsEquip = 0;
        if (!torsoAvailable[torsoEquip])
            torsoEquip = 0;
        if (!legsAvailable[legsEquip])
            legsEquip = 0;
        if (!swordAvailable[swordEquip])
            swordEquip = 0;
    }
}
