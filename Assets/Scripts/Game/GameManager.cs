using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TurnManager turnManager;
    [SerializeField] GameObject _endgamePanel;
    [SerializeField] TextMeshProUGUI _winPlayer;

    bool isEnd = false;

    private void Start()
    {
        Time.timeScale = 1.0f;
        _endgamePanel.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (turnManager.AlivePlayers == 1)
        {
            Time.timeScale = 0f;
            //Debug.Log(turnManager.GetLastPlayer().PlayerName);
            _winPlayer.text = turnManager.GetLastPlayer().PlayerName;
            _endgamePanel.SetActive(true);
            isEnd = true;
        }
    }

    public bool IsEnd { get { return isEnd; } }
}
