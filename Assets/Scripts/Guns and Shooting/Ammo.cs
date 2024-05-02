using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Ammo : MonoBehaviour
{
    [SerializeField] float _dmg;
    [SerializeField] float _radius;

    [SerializeField] Rigidbody2D _rigidbody;

    Destroyer _destroyer;
    Health _hp;
    bool _active;

    private void Start()
    {
        _destroyer = FindObjectOfType<Destroyer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _destroyer.transform.position = transform.position;
        Invoke(nameof(DoCut), 0.0001f);

        _active = true;
    }

    void DoCut()
    {
        _destroyer.DoCut();
        Destroy(gameObject);
    }

    public void SetVelocity(Vector2 value)
    {
        _rigidbody.velocity = value;
        _rigidbody.AddTorque(Random.Range(-8f, 8f));
    }
}
