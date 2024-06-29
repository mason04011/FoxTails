using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float baseStaminaRegen = 1.0f;
    [SerializeField] private float maxStamina = 100.0f;

    [SerializeField] private float healthRegenMultiplier = 0.1f;
    [SerializeField] private float hungerRegenMultiplier = 0.2f;

    [SerializeField] private float health = 100.0f;
    [SerializeField] private float hunger = 100.0f;
    [SerializeField] public float stamina = 100.0f;

    [SerializeField] public bool isMoving = false;

    [SerializeField] private Animator anim;

    void Update()
    {
        float healthRegen = health * healthRegenMultiplier;
        float hungerRegen = hunger * hungerRegenMultiplier;
        float totalStaminaRegen = (baseStaminaRegen + healthRegen + hungerRegen)/2;

        if (!isMoving)
        {
            stamina = Mathf.Clamp(stamina + totalStaminaRegen * Time.deltaTime, 0, maxStamina);
            anim.SetBool("isMoving",false);
        }
        else
        {
            anim.SetBool("isMoving", true);
        }
        
        
      
    }
    
    public void UpdateStamina(float newStaminaValue)
    {
        stamina = newStaminaValue;
    }

    public float GetStamina() {  return stamina; }
}
