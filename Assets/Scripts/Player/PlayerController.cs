using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _jumpPower;

    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Collider2D _cl;
    [SerializeField] SpriteRenderer _spriteRenderer;
    
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _groundLayer;

    float _horizontal;
    bool _isRight = true;
    bool _isGrounded = false;

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal"); 
        if(Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (_isGrounded)
        {
            _rb.velocity = new Vector2(_horizontal * _speed, _rb.velocity.y);
        }
    }

    private void Flip()
    {
        if (_isRight && _horizontal < 0f || !_isRight && _horizontal > 0f)
        {
            _isRight = !_isRight;
            _spriteRenderer.flipX = _isRight;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            _isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            _isGrounded = false;
        }
    }

    public bool IsGrounded { get { return _isGrounded; } }
}
