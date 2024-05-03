using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _players;

    int _turn;
    int _playersNumber;

    Shooting _shoot;
    Ammo _ammo;

    private void Start()
    {
        _turn = 0;
        _playersNumber = _players.Count;
    }

    private void Update()
    {
        _shoot = _players[_turn].GetComponentInChildren<Shooting>();
        _ammo = FindAnyObjectByType<Ammo>();
        //Debug.Log(_turn);
        NextTurn();
    }

    public void NextTurn()
    {
        if (_shoot.IsShoot && !_ammo)
        {
            if (_turn + 1 == _playersNumber)
            {
                _turn = 0;
            }
            else
            {
                _turn++;
            }

            _shoot.IsShoot = false;
        }
    }

    public int Turn { get { return _turn; } }
}
