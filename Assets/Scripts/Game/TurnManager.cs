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
        if (!_players[_turn])
        {
            SwitchToNextAlivePlayer();
        }

        Debug.Log(_turn);

        _shoot = _players[_turn].GetComponentInChildren<Shooting>();
        _ammo = FindAnyObjectByType<Ammo>();
        NextTurn();
    }

    public void NextTurn()
    {
        if (_shoot.IsShoot && !_ammo)
        {
            SwitchToNextAlivePlayer();

            _shoot.IsShoot = false;
        }
    }

    private void SwitchToNextAlivePlayer()
    {
        int nextTurnIndex = (_turn + 1) % _playersNumber;

        for (int i = 0; i < _playersNumber; i++)
        {
            int indexToCheck = (nextTurnIndex + i) % _playersNumber;
            if (_players[indexToCheck])
            {
                _turn = indexToCheck;
                break;
            }
        }
    }

    public int Turn { get { return _turn; } }
}
