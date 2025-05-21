using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInfoUI : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;

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
