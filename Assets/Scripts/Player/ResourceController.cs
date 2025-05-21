using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public ResourceManager resourceManager;
    public PlayerController playerController;
    
    Resource health {get {return resourceManager.health;}}
    Resource stamina {get {return resourceManager.stamina;}}

    private void Update()
    {
        stamina.SubtractionResource(stamina.passiveValue*Time.deltaTime);
        if (stamina.currentValue <= 0f)
        {
            playerController.Speed = 2.0f;
        }
    }
    
    
}
