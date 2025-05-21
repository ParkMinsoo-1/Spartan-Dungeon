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
}
