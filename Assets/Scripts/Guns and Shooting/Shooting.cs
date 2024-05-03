using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Renderer _renderer;
    [SerializeField] Ammo _ammo;
    [SerializeField] GameObject _launcher;
    [SerializeField] SpriteRenderer _pointer;
    [SerializeField] float _maxSpeedMultiplier = 2f;
    [SerializeField] Charger _charger;

    Rigidbody2D _playerRB;
    PlayerController _playerContr;
    Camera _camera;

    bool _isAiming = false;
    float _speedMultiplier = 0.01f;

    private void Start()
    {
        _renderer.enabled = false;
        _pointer.enabled = false;
        _camera = Camera.main;
        GameObject player = GameObject.FindWithTag("Player");
        _playerRB = player.GetComponent<Rigidbody2D>();
        _playerContr = player.GetComponent<PlayerController>();
        _charger.Charge(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && _playerContr.IsGrounded)
        {
            _renderer.enabled = true;
            _playerRB.bodyType = RigidbodyType2D.Static;
            _isAiming = true;
            _charger.Charge(true);
            _pointer.enabled=true;  
        }

        if (_isAiming)
        {
            Vector3 screenMousePosition = Input.mousePosition;
            Vector3 worldMousePosition = _camera.ScreenToWorldPoint(screenMousePosition);
            Vector3 direction = worldMousePosition - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (Input.GetMouseButton(1) && _speedMultiplier <= _maxSpeedMultiplier)
            {
                _speedMultiplier += Time.deltaTime;
                Debug.Log(_speedMultiplier);
                _charger.UpdateChargeBar(_speedMultiplier,_maxSpeedMultiplier); 
            }
        }

        if (Input.GetMouseButtonDown(0) && _isAiming && _speedMultiplier!=0)
        {
            Vector3 screenMousePosition = Input.mousePosition;
            Vector3 worldMousePosition = _camera.ScreenToWorldPoint(screenMousePosition);
            Vector3 direction = worldMousePosition - transform.position;
            Vector3 velocity = direction * _speedMultiplier;

            Ammo newBomb = Instantiate(_ammo, _launcher.transform.position, Quaternion.identity);
            newBomb.SetVelocity(velocity);

            _playerRB.bodyType = RigidbodyType2D.Dynamic;
            _speedMultiplier = 0.01f;
            _charger.Charge(false);
            _pointer.enabled = false;
        }

    }
}
