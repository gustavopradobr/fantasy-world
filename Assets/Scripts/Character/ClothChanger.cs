using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothChanger : MonoBehaviour
{
    [System.Serializable]
    public class ClothPart
    {
        public SpriteRenderer renderer;
        public Sprite[] bodySprite = new Sprite[0];
    }

    [SerializeField] private List<ClothPart> hair = new List<ClothPart>();
    [SerializeField] private List<ClothPart> arms = new List<ClothPart>();
    [SerializeField] private List<ClothPart> torso = new List<ClothPart>();
    [SerializeField] private List<ClothPart> legs = new List<ClothPart>();
    [SerializeField] private List<ClothPart> sword = new List<ClothPart>();

    private void Start()
    {
        LoadClothes();
    }

    private void LoadClothes()
    {
        ChangeHair(GameManager.Instance.gameData.hairEquip);
        ChangeArms(GameManager.Instance.gameData.armsEquip);
        ChangeTorso(GameManager.Instance.gameData.torsoEquip);
        ChangeLegs(GameManager.Instance.gameData.legsEquip);
        ChangeSword(GameManager.Instance.gameData.swordEquip);
    }

    public void ChangeHair(int index)
    {
        GameManager.Instance.gameData.hairAvailable[index] = true;
        GameManager.Instance.gameData.hairEquip = index;

        for (int i = 0; i < hair.Count; i++)
            hair[i].renderer.sprite = hair[i].bodySprite[index];
    }
    public void ChangeArms(int index)
    {
        GameManager.Instance.gameData.armsAvailable[index] = true;
        GameManager.Instance.gameData.armsEquip = index;

        for (int i = 0; i < arms.Count; i++)
            arms[i].renderer.sprite = arms[i].bodySprite[index];
    }
    public void ChangeTorso(int index)
    {
        GameManager.Instance.gameData.torsoAvailable[index] = true;
        GameManager.Instance.gameData.torsoEquip = index;

        for (int i = 0; i < torso.Count; i++)
            torso[i].renderer.sprite = torso[i].bodySprite[index];
    }
    public void ChangeLegs(int index)
    {
        GameManager.Instance.gameData.legsAvailable[index] = true;
        GameManager.Instance.gameData.legsEquip = index;

        for (int i = 0; i < legs.Count; i++)
            legs[i].renderer.sprite = legs[i].bodySprite[index];
    }
    public void ChangeSword(int index)
    {
        GameManager.Instance.gameData.swordAvailable[index] = true;
        GameManager.Instance.gameData.swordEquip = index;

        for (int i = 0; i < sword.Count; i++)
            sword[i].renderer.sprite = sword[i].bodySprite[index];
    }
}
