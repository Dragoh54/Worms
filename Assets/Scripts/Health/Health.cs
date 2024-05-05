using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    [SerializeField] Healthbar _healthbar;
    
    [SerializeField] Sounds _hurtSounds;

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
        _hurtSounds.PlaySound(0, rnd: true, destroy: true, p1: 2.5f, p2: 3f);
        Debug.Log(_hp);

        if (_hp <= 0)
        {
            Debug.Log("DEAD");
            InstantDeath();
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            _turnManager = FindAnyObjectByType<TurnManager>();
            //Debug.Log(_turnManager.AlivePlayers);
            InstantDeath();
        }
    }

    public void TakeDamage(float amount)
    {
        _turnManager = FindAnyObjectByType<TurnManager>();
        //Debug.Log(_turnManager.AlivePlayers);
        _hp -= amount;
        _healthbar.UpdateHealthBar(maxHealth, _hp);
        Debug.Log(_hp);

        if (_hp <= 0)
        {
            Debug.Log("DEAD");
            InstantDeath();
        }
    }*/

    public void InstantDeath()
    {
        _hp = 0;
        Destroy(gameObject);
    }
}
