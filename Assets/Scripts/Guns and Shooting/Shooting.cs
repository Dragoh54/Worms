using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Renderer _gun;
    [SerializeField] Ammo _ammo;
    [SerializeField] GameObject _launcher;

    [SerializeField] SpriteRenderer _pointer;
    [SerializeField] Charger _charger;

    [SerializeField] float _maxSpeedMultiplier = 2f;
    float _speedMultiplier = 1f;

    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] PlayerController _playerContr;

    Camera _camera;
    CameraFollow _cf;

    [SerializeField] Sounds _gunSounds;

    bool _isAiming = false;
    bool _isShoot;

    private void Start()
    {
        _gun.enabled = false;
        _pointer.enabled = false;
        _isShoot = false;

        _camera = Camera.main;
        _cf = Camera.main.GetComponent<CameraFollow>();

        _charger.Charge(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && _playerContr.IsGrounded)
        {
            _isAiming = true;
            _pointer.enabled=true;
            _gun.enabled = true;

            _rigidbody.bodyType = RigidbodyType2D.Static;
            _playerContr.enabled = false;
            //Debug.Log(_playerContr.enabled);
        }

        if (_isAiming)
        {
            Vector3 screenMousePosition = Input.mousePosition;
            Vector3 worldMousePosition = _camera.ScreenToWorldPoint(screenMousePosition);
            Vector3 direction = worldMousePosition - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (Input.GetKey(KeyCode.Space) && _speedMultiplier <= _maxSpeedMultiplier)
            {
                _charger.Charge(true);
                _speedMultiplier += Time.deltaTime;

                Debug.Log(_gunSounds.GetAudioClip(0).name);
                _gunSounds.PlaySound(0, 0.1f);

                _charger.UpdateChargeBar(_speedMultiplier,_maxSpeedMultiplier); 
            }
        }

        //if (Input.GetMouseButtonDown(0) && _isAiming)
        if (Input.GetKeyUp(KeyCode.Space) && _isAiming)
        {
            _gunSounds.StopSound();
            _gunSounds.PlaySound(0, rnd: true);

            Vector3 screenMousePosition = Input.mousePosition;
            Vector3 worldMousePosition = _camera.ScreenToWorldPoint(screenMousePosition);
            Vector3 direction = worldMousePosition - transform.position;
            Vector3 velocity = direction * _speedMultiplier;

            Ammo newBomb = Instantiate(_ammo, _launcher.transform.position, Quaternion.identity);
            newBomb.SetVelocity(velocity);
            _cf.ChangeTarget(newBomb.gameObject);

            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _speedMultiplier = 0.01f;
            _charger.Charge(false);
            _pointer.enabled = false;
            _isShoot = true;
            _gun.enabled = false;
            _isAiming = false;
            //_playerContr.enabled = true;
        }
    }

    public bool IsShoot {get { return _isShoot; } set { _isShoot = value; } }
    public bool IsAiming { get { return _isAiming; } }
}
