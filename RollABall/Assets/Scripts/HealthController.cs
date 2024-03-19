using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{   
    public float health = 14;
    public float maxHealth = 14;
    public Image healthBar;

    private void UpdateHealthBar(){
        healthBar.fillAmount = health / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
    }

    
}
