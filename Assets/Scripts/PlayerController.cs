using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _jumpPower;
    [SerializeField] float _jumpSpeed;

    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Collider2D _cl;
    
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _groundLayer;

    float _horizontal;
    bool _isRight = false;


    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal"); 
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
        }

        if(Input.GetButtonUp("Jump") && _rb.velocity.y > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * _jumpSpeed);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * _speed, _rb.velocity.y);
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }

    private void Flip()
    {
        if (_isRight && _horizontal < 0f || !_isRight && _horizontal > 0f)
        {
            _isRight = !_isRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
