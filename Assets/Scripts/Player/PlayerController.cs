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
    [SerializeField] Player player;
    
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _groundLayer;

    [SerializeField] Sounds _moveSounds;

    Shooting _shoot;

    public Animator animator;

    float _horizontal;
    bool _isRight = true;
    bool _isGrounded = false;

    private void Start()
    {
        _shoot = FindAnyObjectByType<Shooting>();
    }

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && player.IsActive && !_shoot.IsAiming)
        {
            _moveSounds.PlaySound(0);
            _rb.AddForce(transform.up * _jumpPower, ForceMode2D.Impulse);
        }

        animator.SetFloat("HorizontalMove", Mathf.Abs(_horizontal));

        //Debug.Log(!_isGrounded);

        if (!_isGrounded)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (_isGrounded && player.IsActive)
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
        Debug.Log("Exit");
        if (collision.gameObject.tag == ("Ground") && _rb.bodyType != RigidbodyType2D.Static)
        {
            _isGrounded = false;
        }
    }

    public bool IsGrounded { get { return _isGrounded; } }
}
