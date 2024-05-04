using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _pausePanel;
    [SerializeField] GameManager gameManager;

    bool isActive = false;

    private void Start()
    {
        _pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameManager.IsEnd) 
        {
            if (!isActive) 
            {
                Time.timeScale = 0f;
                isActive = true;
                _pausePanel.SetActive(true);
            }
            else 
            {
                Time.timeScale = 1f;
                isActive = false;
                _pausePanel.SetActive(false);
            }
        }
    }

}
