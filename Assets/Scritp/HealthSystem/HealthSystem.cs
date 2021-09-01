using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int heath;
    private int healthMax;

    public HealthSystem (int health){
        this.heath = health;
        heath = healthMax;
    }

    public int GetHeath(){
        return heath;
    }

    public float GetHealthPercent(){
        return (float)heath*0.37f/healthMax;
    }

    public void Damage(int damageAmount){
        heath -= damageAmount;
        if (heath < 0 ) heath = 0;
    }
    public void Heal(int healAmount){
        heath += healAmount;
        if (heath > healthMax) heath = healthMax;
    }
}
