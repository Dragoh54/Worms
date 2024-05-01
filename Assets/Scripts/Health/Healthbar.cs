using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Image _healthbar;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _healthbar.fillAmount = currentHealth/maxHealth;
    }
}
