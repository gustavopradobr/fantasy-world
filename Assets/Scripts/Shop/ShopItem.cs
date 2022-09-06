using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopItem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image itemImage;
    [SerializeField] private Text priceText;
    [SerializeField] private GameObject unlockedObject;

    [Header("Button")]
    [SerializeField] private Color disabledColor;
    private Color whiteColor = new Color(1, 1, 1);

    public void SetInfo(int id, bool unlocked, int price, bool selling, Sprite itemSprite, ShopManager manager)
    {
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        Color desiredColor = whiteColor;
        button.interactable = true;

        if (selling)
        {
            button.onClick.AddListener(delegate { manager.SellItem(id); });
            price = Mathf.FloorToInt((float)price / 2);
        }
        else if (!unlocked)
        {    
            button.onClick.AddListener(delegate { manager.BuyItem(id); });

            if (price > GameManager.goldCoins)
            {
                desiredColor = disabledColor;
                button.interactable = false;
            }
        }
        else
            button.onClick.AddListener(delegate { manager.EquipItem(id); });

        button.targetGraphic.color = desiredColor;
        itemImage.color = desiredColor;
        priceText.color = desiredColor;

        priceText.text = price.ToString();
        itemImage.sprite = itemSprite;
        priceText.transform.parent.gameObject.SetActive(!unlocked || selling);
        unlockedObject.SetActive(unlocked && !selling);
    }
}
