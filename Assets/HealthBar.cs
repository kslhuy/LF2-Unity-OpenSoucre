using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image Health;
    private void Awake()  {
        Entity entity = GetComponentInParent<Entity>();
        entity.ModifierHealth += HandleModifyeHealth;
    }

    private void HandleModifyeHealth(float percent)
    {
        Health.fillAmount = percent;
    }

    // public void SetMaxHealth(int health){
    //     slider.maxValue = health;
    //     slider.value = health;
    // } 
    // public void SetHeath(int health){
    //     slider.value = health;
        
    // }
}
