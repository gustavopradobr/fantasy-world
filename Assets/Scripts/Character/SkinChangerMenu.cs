using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChangerMenu : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private List<SkinChanger.BodyPart> bodyPart = new List<SkinChanger.BodyPart>();

    private void Start()
    {
        UpdateSkin(gameData.skinIndex);
    }

    public void ChangeSkin(int newSkin)
    {
        gameData.skinIndex = newSkin;
        gameData.SaveGameData();
        UpdateSkin(newSkin);
    }
    public void UpdateSkin(int newSkin)
    {
        for (int i = 0; i < bodyPart.Count; i++)
            bodyPart[i].renderer.sprite = bodyPart[i].bodySprite[newSkin];
    }
}
