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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            InstantDeath();
        }
    }

    public void TakeDamage(float amount)
    {
        _hp -= amount;
        _healthbar.UpdateHealthBar(maxHealth, _hp);
        Debug.Log(_hp);

        if (_hp <= 0)
        {
            Debug.Log("DEAD");
            _hp = 0;
            Destroy(gameObject);
        }
    }

    public void InstantDeath()
    {
        _hp = 0;
        Destroy(gameObject);
    }
}
