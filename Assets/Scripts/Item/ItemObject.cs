using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
    public (string name, string description) GetInfo();
    public void OnInteraction();

}
public class ItemObject : MonoBehaviour, Interactable
{
    public ItemData itemData;

    private void Start()
    {
        itemData = Resources.Load<ItemData>("ItemData/" + gameObject.name);

        if (itemData == null)
        {
            Debug.Log($"{gameObject.name}을 찾을 수 없습니다.");
        }
        
    }

    public (string name, string description) GetInfo()
    {
        return (itemData.itemName, itemData.itemDescription);
    }

    public void OnInteraction()
    {
        
    }
}
