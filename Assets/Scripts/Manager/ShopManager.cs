using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static bool shopIsOpen = false;

    [Header("References")]
    [SerializeField] private ScriptableShopItems shopItems;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private GameObject shopCanvas;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Transform shopContent;
    [SerializeField] private Transform sellContent;
    [SerializeField] private Text headerText;

    [Header("Events")]
    [SerializeField] private UnityEvent shopOpened;
    [SerializeField] private UnityEvent shopClosed;
    [SerializeField] private UnityEvent switchToBuy;
    [SerializeField] private UnityEvent switchToSell;

    private List<bool> availableItems = new List<bool>();
    private List<ShopItem> cachedItem = new List<ShopItem>();
    private const string headerBuyString = "BUY/EQUIP";
    private const string headerSellString = "SELL ITEMS";

    private void Start()
    {
        shopIsOpen = false;
        LoadAvailableItems();
    }
    public void OpenShop(bool open)
    {
        shopIsOpen = open;
        LoadAvailableItems();
        SpawnItems();
        shopCanvas.SetActive(open);
        sellContent.gameObject.SetActive(false);
        shopContent.gameObject.SetActive(true);
        headerText.text = headerBuyString;
        scrollRect.content = shopContent.GetComponent<RectTransform>();

        if (open)
            shopOpened.Invoke();
        else
            shopClosed.Invoke();
    }

    public void SpawnItems()
    {
        if(cachedItem.Count == 0)
        {
            foreach (Transform child in shopContent)
                Destroy(child.gameObject);
        }
        else
        {
            UpdateItems();
            return;
        }

        for (int i = 0; i < shopItems.item.Count; i++)
        {
            GameObject spawnedItem = Instantiate(shopItemPrefab, shopContent);
            spawnedItem.SetActive(true);

            ShopItem itemDetails = spawnedItem.GetComponent<ShopItem>();

            itemDetails.SetInfo(shopItems.item[i].id, availableItems[i], shopItems.item[i].price, false, shopItems.item[i].itemSprite, this);

            cachedItem.Add(itemDetails);
        }
    }
    private void UpdateItems()
    {
        for (int i = 0; i < shopItems.item.Count; i++)
        {
            cachedItem[i].gameObject.SetActive(true);
            cachedItem[i].transform.SetParent(shopContent);
            cachedItem[i].SetInfo(shopItems.item[i].id, availableItems[i], shopItems.item[i].price, false, shopItems.item[i].itemSprite, this);
        }
    }

    private void MoveItemsToSell()
    {
        for (int i = 0; i < cachedItem.Count; i++)
        {
            cachedItem[i].gameObject.SetActive(false);
            cachedItem[i].transform.SetParent(sellContent);

            if (!availableItems[i] || shopItems.item[i].price == 0)
                continue;

            cachedItem[i].gameObject.SetActive(true);            

            cachedItem[i].SetInfo(shopItems.item[i].id, availableItems[i], shopItems.item[i].price, true, shopItems.item[i].itemSprite, this);
        }
    }

    public void SwitchToSell()
    {
        LoadAvailableItems();
        MoveItemsToSell();
        switchToSell.Invoke();
        shopContent.gameObject.SetActive(false);
        sellContent.gameObject.SetActive(true);
        scrollRect.content = sellContent.GetComponent<RectTransform>();
        headerText.text = headerSellString;
    }

    public void SwitchToBuy()
    {
        LoadAvailableItems();
        SpawnItems();
        switchToBuy.Invoke();
        sellContent.gameObject.SetActive(false);
        shopContent.gameObject.SetActive(true);
        scrollRect.content = shopContent.GetComponent<RectTransform>();
        headerText.text = headerBuyString;
    }

    private void LoadAvailableItems()
    {
        availableItems.Clear();

        for (int i=0; i< GameManager.Instance.gameData.hairAvailable.Length; i++)
            availableItems.Add(GameManager.Instance.gameData.hairAvailable[i]);

        for(int i=0; i< GameManager.Instance.gameData.armsAvailable.Length; i++)
            availableItems.Add(GameManager.Instance.gameData.armsAvailable[i]);

        for(int i=0; i< GameManager.Instance.gameData.torsoAvailable.Length; i++)
            availableItems.Add(GameManager.Instance.gameData.torsoAvailable[i]);

        for(int i=0; i< GameManager.Instance.gameData.legsAvailable.Length; i++)
            availableItems.Add(GameManager.Instance.gameData.legsAvailable[i]);

        for(int i=0; i< GameManager.Instance.gameData.swordAvailable.Length; i++)
            availableItems.Add(GameManager.Instance.gameData.swordAvailable[i]);
    }

    public void BuyItem(int id)
    {
        if (shopItems.item[id].price <= GameManager.goldCoins)
        {
            EquipItem(id);
            GameManager.goldCoins -= shopItems.item[id].price;

            GameManager.Instance.gameData.SaveGameData();

            LoadAvailableItems();
            UpdateItems();
        }
    }

    public void SellItem(int id)
    {
        GameManager.goldCoins += Mathf.FloorToInt((float)shopItems.item[id].price / 2);
        RemoveItemFromData(id);
        GameManager.Instance.gameData.SaveGameData();
        LoadAvailableItems();
        MoveItemsToSell();
    }

    public void EquipItem(int index)
    {
        GameManager.Instance.audioManager.ButtonClickLight();

        if (index < 3)
        {
            GameManager.Instance.playerController.clothChanger.ChangeHair(index);
        }
        else if (index < 6)
        {
            index -= 3;
            GameManager.Instance.playerController.clothChanger.ChangeArms(index);
        }
        else if (index < 9)
        {
            index -= 6;
            GameManager.Instance.playerController.clothChanger.ChangeTorso(index);
        }
        else if (index < 12)
        {
            index -= 9;
            GameManager.Instance.playerController.clothChanger.ChangeLegs(index);
        }
        else if (index < 15)
        {
            index -= 12;
            GameManager.Instance.playerController.clothChanger.ChangeSword(index);
        }
    }
    private void RemoveItemFromData(int index)
    {
        GameManager.Instance.audioManager.ButtonClickLight();

        if (index < 3)
        {
            GameManager.Instance.gameData.hairAvailable[index] = false;
            if(index == GameManager.Instance.gameData.hairEquip)
                GameManager.Instance.playerController.clothChanger.ChangeHair(GameManager.Instance.gameData.hairEquip = 0);
        }
        else if (index < 6)
        {
            index -= 3;
            GameManager.Instance.gameData.armsAvailable[index] = false;
            if (index == GameManager.Instance.gameData.armsEquip)
                GameManager.Instance.playerController.clothChanger.ChangeArms(GameManager.Instance.gameData.armsEquip = 0);
        }
        else if (index < 9)
        {
            index -= 6;
            GameManager.Instance.gameData.torsoAvailable[index] = false;
            if (index == GameManager.Instance.gameData.torsoEquip)
                GameManager.Instance.playerController.clothChanger.ChangeTorso(GameManager.Instance.gameData.torsoEquip = 0);
        }
        else if (index < 12)
        {
            index -= 9;
            GameManager.Instance.gameData.legsAvailable[index] = false;
            if (index == GameManager.Instance.gameData.legsEquip)
                GameManager.Instance.playerController.clothChanger.ChangeLegs(GameManager.Instance.gameData.legsEquip = 0);
        }
        else if (index < 15)
        {
            index -= 12;
            GameManager.Instance.gameData.swordAvailable[index] = false;
            if (index == GameManager.Instance.gameData.swordEquip)
                GameManager.Instance.playerController.clothChanger.ChangeSword(GameManager.Instance.gameData.swordEquip = 0);
        }
    }

}
