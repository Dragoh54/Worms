using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    
    [SerializeField] Healthbar _healthbar;

    float _hp;

    void Start()
    {
        _hp = maxHealth;

        _healthbar.UpdateHealthBar(maxHealth, _hp); 
    }

    public void TakeDamage(float amount)
    {
        _hp -= amount;
        _healthbar.UpdateHealthBar(maxHealth, _hp);
        Debug.Log(_hp);

        if (_hp <= 0)
        {
            _hp = 0;
            Destroy(this);
        }
    }
}
