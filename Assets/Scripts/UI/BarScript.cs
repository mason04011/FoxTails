using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{

    // IMAGE
    private Image bar;

    // LIMITS
    [SerializeField] private float maxValue = 100f;
    [SerializeField] private float currentValue; // for testing

    // LERPING
    [SerializeField] private float lerpTime;
    private float nextValue;

    // COLOUR SETTINGS
    private Color barColour;
    [SerializeField] private Color maxColour;
    [SerializeField] private Color minColour;



    private void Start()
    {
        bar = transform.GetChild(0).GetComponent<Image>();
        currentValue = maxValue;
    }

    private void Update()
    {
        if(currentValue > maxValue) currentValue = maxValue;
        else if(currentValue < 0) currentValue = 0;

        UpdateFillAmount();
        UpdateColour();
    }

    private void UpdateFillAmount()
    {
        nextValue = 0.25f + (currentValue / maxValue) * 0.75f;
        bar.fillAmount = Mathf.Lerp(bar.fillAmount, nextValue, Time.deltaTime/lerpTime);
    }

    private void UpdateColour()
    {
        barColour = Color.Lerp(minColour, maxColour, currentValue / maxValue);
        bar.color = barColour;
    }

    public void IncreaseValue(float points)
    {
        if(currentValue < maxValue)
            currentValue += points;
    }

    public void DecreaseValue(float points)
    {
        if(currentValue > 0)
            currentValue -= points;
    }

}
