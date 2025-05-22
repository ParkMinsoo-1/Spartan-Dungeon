using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController _playerController;
    public ResourceController _resourceController;
    public ItemData _itemData;
    public Action<ItemData> getItem;

    private Coroutine coroutine;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        _playerController = GetComponent<PlayerController>();
        _resourceController = GetComponent<ResourceController>();
        CharacterManager.Instance.Player._resourceController.playerController = _playerController;
    }

    void Start()
    {
        getItem += GetItem;
    }

    public void GetItem(ItemData _itemData)
    {
        if (_itemData == null)
            return;
        switch (_itemData.itemType)
        {
            case ItemType.Consumable:
                UseConsumableItem();
                break;
            case ItemType.Buff:
                if (coroutine != null)
                    StopCoroutine(coroutine);
                coroutine = StartCoroutine(_resourceController.BuffCoroutine(_itemData));
                break;
        }
    }

    private void UseConsumableItem()
    {
        switch (_itemData.consumableType)
        {
            case ConsumableType.Health:
                _resourceController.resourceManager.health.AddResource(_itemData.value);
                break;
            case ConsumableType.Stamina:
                _resourceController.resourceManager.stamina.AddResource(_itemData.value);
                break;
        }
        _itemData = null;
    }
}