using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInfoUI : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public RectTransform itemInfoPanel;
    
    public Vector2 offset = new Vector2(200f, -150f);

    public void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        itemInfoPanel.position = mousePosition + offset;
    }

    public void ShowInfo(Interactable info)
    {
        var (name, description) = info.GetInfo();
        itemNameText.text = name;
        itemDescriptionText.text = description;
        gameObject.SetActive(true);
    }

    public void HideInfo()
    {
        gameObject.SetActive(false);
    }
}
