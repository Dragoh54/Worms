using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTurn : MonoBehaviour
{
    [SerializeField] List<GameObject> _players;
    static float Turn;

    private void Start()
    {
        Turn = 0;
    }
}
