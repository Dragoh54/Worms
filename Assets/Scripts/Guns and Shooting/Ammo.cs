using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] float _dmg;
    [SerializeField] Rigidbody2D _rigidbody;

    [SerializeField] Sounds _bombSounds;

    Destroyer _destroyer;
    PolygonCollider2D _destroyerCollider;
    bool _active = true;

    private void Start()
    {
        _destroyer = FindObjectOfType<Destroyer>();
        _destroyerCollider = _destroyer.GetComponent<PolygonCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _bombSounds.PlaySound(0, rnd: true, destroy: true);
            _destroyer.transform.position = transform.position;
            Invoke(nameof(DoCut), 0.0001f);
            Hit();
        }
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

    public void Hit()
    {
        if(_destroyerCollider != null)
        {
            List<Collider2D> colliders = new List<Collider2D>();
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(LayerMask.GetMask("Player"));
            int colliderCount = _destroyerCollider.OverlapCollider(contactFilter, colliders);
            //Debug.Log(colliderCount);

            if (colliderCount > 0)
            {
                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject.CompareTag("AlivePlayer"))
                    {
                        GameObject targetObject = collider.gameObject;
                        Health playerHp = targetObject.GetComponent<Health>();
                        playerHp.TakeDamage(_dmg);
                        _active = false;
;                   }
                }
            }
        }
    }
    
    public bool IsActive { get { return _active; } }
}
