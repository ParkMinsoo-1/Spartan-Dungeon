using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable,
    Equipable,
    Buff
}

public enum ConsumableType
{
    health,
    stamina,
}

public enum BuffType
{
    none,
    speed
}


[CreateAssetMenu(fileName = "New Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("ItemInfo")]
    public string itemName;
    public string itemDescription;
    public ItemType itemType;
    public GameObject itemPrefab;
    public ConsumableType consumableType;
    public float value;
    
    [Header("BuffInfo")]
    public BuffType buffType;
    public float buffValue;
    public float buffDuration;
    
}
