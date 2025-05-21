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

    private Coroutine coroutine;

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
        switch (_itemData.itemType)
        {
            case ItemType.Consumable:
                UseConsumableItem();
                break;
            case ItemType.Buff:
                if(coroutine != null)
                    StopCoroutine(coroutine);
                coroutine = StartCoroutine(BuffCoroutine(_itemData));
                break;
        }    
        _itemData = null;
    }

    private void UseConsumableItem()
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

    private IEnumerator BuffCoroutine(ItemData itemData)
    {
        string methodName = $"Buff_{itemData.buffType}";
        var method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

        if (method != null)
        {
            yield return (IEnumerator)method.Invoke(this, new object[] { itemData });
        }
        else
        {
            Debug.Log($"Buff 메서드 {methodName}가 없습니다.");
        }
    }
    private IEnumerator Buff_speed(ItemData item)
    {
        float originalSpeed = _playerController.Speed;
        _playerController.Speed += item.buffValue;
        yield return new WaitForSeconds(item.buffDuration);
        _playerController.Speed = originalSpeed;
    }
}
