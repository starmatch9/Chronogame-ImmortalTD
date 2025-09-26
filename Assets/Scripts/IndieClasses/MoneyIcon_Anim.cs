using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyIcon_Anim : MonoBehaviour
{
    public GameObject g;
    private void Awake()
    {
        GlobalElementPowerFunction.elementPower = g;
    }
}
