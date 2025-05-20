using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Resource : MonoBehaviour
{
    public Image bar;
    
    [SerializeField] public float currentValue;
    [SerializeField] public float startValue;
    [SerializeField] public float maxValue;
    [SerializeField] public float passiveValue;

    private void Start()
    {
        currentValue = startValue;
    }

    private void Update()
    {
        bar.fillAmount = ChangeValue();
    }

    public float ChangeValue()
    {
        return currentValue/maxValue;
    }

    public void AddResource(float value)
    {
        currentValue = Mathf.Min(currentValue + value, maxValue);
    }

    public void SubtractionResource(float value)
    {
        currentValue = Mathf.Max(currentValue - value, 0.0f);
    }
    
}
