using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerName : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Player _player;

    private void Start()
    {
        var text = _player.PlayerName;
        Debug.Log(text);
        if (text != null)
        {
            _text.text = text;
        }
    }
}
