using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TurnManager turnManager;
    [SerializeField] GameObject _endgameCanvas;

    bool _active = false;

    private void Start()
    {
        Time.timeScale = 1.0f;
        _endgameCanvas.SetActive(false);
    }

    private void Update()
    {
        //if(turnManager.AlivePlayers == 1)
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            _endgameCanvas.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 1f;
            _endgameCanvas.SetActive(false);
        }
    }
}
