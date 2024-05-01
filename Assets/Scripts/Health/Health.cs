using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    private float _hp;

    public bool isAlive;

    public RectTransform healthBar;

    void Start()
    {
        _hp = maxHealth;
        isAlive = true;
    }

    public void TakeDamage(float amount)
    {
        _hp -= amount;

        if (_hp <= 0)
        {
            _hp = 0;
            isAlive = false;
            Destroy(this);
        }

        healthBar.sizeDelta = new Vector2(_hp, healthBar.sizeDelta.y);
    }
}
