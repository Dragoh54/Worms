using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Renderer _renderer;

    Rigidbody2D _playerRB;
    PlayerController _playerContr;
    Camera _camera;

    private void Start()
    {
        _renderer.enabled = false;
        _camera = Camera.main;
        GameObject player = GameObject.FindWithTag("Player");
        _playerRB = player.GetComponent<Rigidbody2D>();
        _playerContr = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _playerContr.IsGrounded)
        {
            _renderer.enabled = true;
            _playerRB.bodyType = RigidbodyType2D.Static;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 screenMousePosition = Input.mousePosition;
            Vector3 worldMousePosition = _camera.ScreenToWorldPoint(screenMousePosition);
            Vector3 direction = worldMousePosition - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _playerRB.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
