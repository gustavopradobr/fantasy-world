using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItems", menuName = "ScriptableObjects/ScriptableShopItems", order = 1)]
public class ScriptableShopItems : ScriptableObject
{
    [System.Serializable]
    public class Item
    {
        public string name;
        public int id;
        public int price;
        public Sprite itemSprite;
    }

    [Header("Shop Items List")]
    public List<Item> item = new List<Item>();

#if UNITY_EDITOR
    private void OnValidate()
    {
        for (int i = 0; i < item.Count; i++)
        {
            item[i].id = i;
            item[i].name = "Item " + i;
        }
    }
#endif
}
