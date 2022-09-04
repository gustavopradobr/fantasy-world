using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    [System.Serializable]
    public class BodyPart
    {
        public SpriteRenderer renderer;
        public Sprite[] bodySprite = new Sprite[2];
    }

    [SerializeField] private List<BodyPart> bodyPart = new List<BodyPart>();

    private void Start()
    {
        if(gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            UpdateSkin(GameManager.Instance.gameData.skinIndex);
        }
    }

    public void ChangeSkin(int newSkin)
    {
        GameManager.Instance.gameData.skinIndex = newSkin;
        GameManager.Instance.gameData.SaveGameData();
        UpdateSkin(newSkin);
    }
    public void UpdateSkin(int newSkin)
    {
        for(int i=0; i<bodyPart.Count; i++)
            bodyPart[i].renderer.sprite = bodyPart[i].bodySprite[newSkin];
    }
}
