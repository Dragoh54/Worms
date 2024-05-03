using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int playerId;

    [SerializeField] PlayerController _playerController;
    [SerializeField] Shooting _shooting;

    bool _isActive;
    TurnManager _turnManager;
    CameraFollow _cf;

    private void Start()
    {
        _isActive = false;
        Wait(!_isActive);
        _turnManager = FindAnyObjectByType<TurnManager>();
        _cf = Camera.main.GetComponent<CameraFollow>();
    }

    private void Update()
    {
        Debug.Log(_turnManager.Turn);
        if(_turnManager.Turn == playerId)
        {
            _isActive = true;
            Wait(!_isActive);
            _cf.ChangeTarget(gameObject);
        }
        else
        {
            _isActive = false;
            Wait(!_isActive);
        }
    }

    public void Wait(bool wait)
    {
        _playerController.enabled = !wait;
        _shooting.enabled = !wait;
    }
}
