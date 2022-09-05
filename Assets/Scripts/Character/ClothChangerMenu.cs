using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothChangerMenu : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private List<ClothChanger.ClothPart> hair = new List<ClothChanger.ClothPart>();
    [SerializeField] private List<ClothChanger.ClothPart> arms = new List<ClothChanger.ClothPart>();
    [SerializeField] private List<ClothChanger.ClothPart> torso = new List<ClothChanger.ClothPart>();
    [SerializeField] private List<ClothChanger.ClothPart> legs = new List<ClothChanger.ClothPart>();
    [SerializeField] private List<ClothChanger.ClothPart> sword = new List<ClothChanger.ClothPart>();

    private void Start()
    {
        LoadClothes();
    }

    private void LoadClothes()
    {
        ChangeHair(gameData.hairEquip);
        ChangeArms(gameData.armsEquip);
        ChangeTorso(gameData.torsoEquip);
        ChangeLegs(gameData.legsEquip);
        ChangeSword(gameData.swordEquip);
    }

    public void ChangeHair(int index)
    {
        gameData.hairAvailable[index] = true;
        gameData.hairEquip = index;

        for (int i = 0; i < hair.Count; i++)
            hair[i].renderer.sprite = hair[i].bodySprite[index];
    }
    public void ChangeArms(int index)
    {
        gameData.armsAvailable[index] = true;
        gameData.armsEquip = index;

        for (int i = 0; i < arms.Count; i++)
            arms[i].renderer.sprite = arms[i].bodySprite[index];
    }
    public void ChangeTorso(int index)
    {
        gameData.torsoAvailable[index] = true;
        gameData.torsoEquip = index;

        for (int i = 0; i < torso.Count; i++)
            torso[i].renderer.sprite = torso[i].bodySprite[index];
    }
    public void ChangeLegs(int index)
    {
        gameData.legsAvailable[index] = true;
        gameData.legsEquip = index;

        for (int i = 0; i < legs.Count; i++)
            legs[i].renderer.sprite = legs[i].bodySprite[index];
    }
    public void ChangeSword(int index)
    {
        gameData.swordAvailable[index] = true;
        gameData.swordEquip = index;

        for (int i = 0; i < sword.Count; i++)
            sword[i].renderer.sprite = sword[i].bodySprite[index];
    }
}
