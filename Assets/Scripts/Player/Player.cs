using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController _playerController;
    public ResourceController _resourceController;
    public ItemData _itemData;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        _playerController = GetComponent<PlayerController>();
        _resourceController = GetComponent<ResourceController>();
        CharacterManager.Instance.Player._resourceController.playerController = _playerController;
    }

    private void Update()
    {
        if (_itemData == null)
            return;
 
        if (_itemData.itemType == ItemType.Consumable)
        {
            switch (_itemData.consumableType)
            {
                case ConsumableType.health:
                    _resourceController.resourceManager.health.AddResource(_itemData.value);
                    break;
                case ConsumableType.stamina:
                    _resourceController.resourceManager.stamina.AddResource(_itemData.value);
                    break;
            }
            _itemData = null;
        }
    }
}
