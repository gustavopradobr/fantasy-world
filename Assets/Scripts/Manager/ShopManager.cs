using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScriptableShopItems shopItems;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private GameObject shopCanvas;
    [SerializeField] private Transform shopContent;

    private List<bool> availableItems = new List<bool>();

    private List<ShopItem> cachedItem = new List<ShopItem>();

    private void Start()
    {
        LoadAvailableItems();
    }
    public void OpenShop(bool open)
    {
        LoadAvailableItems();
        SpawnItems();
        shopCanvas.SetActive(open);
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

            itemDetails.SetInfo(shopItems.item[i].id, availableItems[i], shopItems.item[i].price, shopItems.item[i].itemSprite, this);

            cachedItem.Add(itemDetails);
        }
    }
    private void UpdateItems()
    {
        for (int i = 0; i < shopItems.item.Count; i++)
        {
            cachedItem[i].SetInfo(shopItems.item[i].id, availableItems[i], shopItems.item[i].price, shopItems.item[i].itemSprite, this);
        }
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
        }

        LoadAvailableItems();
        UpdateItems();
    }

    public void EquipItem(int index)
    {
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

}
