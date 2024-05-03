using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charger : MonoBehaviour
{
    [SerializeField] Image _chargebar;

    public void UpdateChargeBar(float charge, float max)
    {
        _chargebar.fillAmount = charge / max;
    }

    public void Charge(bool isCharge)
    {
        _chargebar.enabled = isCharge;
    }
}
