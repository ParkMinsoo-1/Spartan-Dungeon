using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public Resource health;
    public Resource stamina;

    private void Start()
    {
        CharacterManager.Instance.Player._resourceController.resourceManager = this;
    }
}
