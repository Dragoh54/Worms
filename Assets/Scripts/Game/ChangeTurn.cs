using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTurn : MonoBehaviour
{
    [SerializeField] List<GameObject> _players;

    int _turn;
    int _playersNumber;
    Shooting _shoot;

    private void Start()
    {
        _turn = 0;
        _playersNumber = _players.Count;
    }

    private void Update()
    {
        ChangePerson();
        Debug.Log(_turn);
    }

    public void ChangePerson()
    {
        _shoot = _players[_turn].GetComponentInChildren<Shooting>();
        if (_shoot.IsShoot) 
        {
            if(_turn + 1 == _playersNumber)
            {
                _turn = 0;
            }
            else
            {
                _turn++;
            }
        }
    }
}
