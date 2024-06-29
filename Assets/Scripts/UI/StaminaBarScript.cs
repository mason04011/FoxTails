using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarScript : MonoBehaviour
{

    [SerializeField] private Player player;
    // IMAGE
    private Image container;
    private Image bar;

    // LIMITS
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float currentStamina; // for testing

    // LERPING
    [SerializeField] private float lerpTime;
    private float nextValue;

    // COLOUR SETTINGS
    private Color barColour;
    [SerializeField] private Color maxColour;
    [SerializeField] private Color minColour;



    private void Awake()
    {
        container = gameObject.GetComponent<Image>();
    }
    
    private void Start()
    {
        bar = transform.GetChild(0).GetComponent<Image>();
        currentStamina = maxStamina;
    }

    private void Update()
    {
        if (currentStamina > maxStamina) currentStamina = maxStamina;
        else if (currentStamina < 0) currentStamina = 0;

        UpdateStamina();
        UpdateFillAmount();
    }

    private void UpdateFillAmount()
    {
        nextValue = currentStamina / maxStamina;
        bar.fillAmount = Mathf.Lerp(bar.fillAmount, nextValue, Time.deltaTime / lerpTime);
    }

    public void ResetStamina()
    {
        currentStamina = maxStamina;
    }

    private void UpdateStamina()
    {
        if(player)
            currentStamina = player.GetStamina();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeImage(false));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeImage(true));
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                container.color = new Color(1, 1, 1, i);
                bar.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        else
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                container.color = new Color(1, 1, 1, i);
                bar.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}
