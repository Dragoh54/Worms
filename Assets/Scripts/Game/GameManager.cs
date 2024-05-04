using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TurnManager turnManager;
    [SerializeField] GameObject _endgameCanvas;

    private void Start()
    {
        Time.timeScale = 1.0f;
        _endgameCanvas.SetActive(false);
    }

    private void Update()
    {
        Debug.Log(turnManager.AlivePlayers);
        if(turnManager.AlivePlayers == 1)
        {
            Time.timeScale = 0f;
            _endgameCanvas.SetActive(true);
        }
    }
}
