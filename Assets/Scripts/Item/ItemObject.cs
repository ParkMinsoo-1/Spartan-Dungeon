using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
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
}
