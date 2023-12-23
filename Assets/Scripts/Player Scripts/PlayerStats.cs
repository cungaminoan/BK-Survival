using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private Image healStats, staminaStats;
    
    public void DisplayHealthStats(float healthValue)
    {
        healthValue /= 100f;
        healStats.fillAmount = healthValue;
    }

    public void DisplayStaminaStats(float staminaValue)
    {
        staminaValue /= 100f;
        staminaStats.fillAmount = staminaValue;
    }
}
