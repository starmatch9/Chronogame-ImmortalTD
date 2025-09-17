using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementLock : MonoBehaviour
{
    public GameObject JinLock = null;
    public GameObject MuLock = null;
    public GameObject ShuiLock = null;
    public GameObject HuoLock = null;
    public GameObject TuLock = null;

    private void Start()
    {
        //那个塔是true，就激活，否则不激活
        if (GlobalLimit.towerElement[GlobalLimit.Element.JIN])
        {
            JinLock.SetActive(true);
        }
        else
        {
            JinLock.SetActive(false);
        }

        if (GlobalLimit.towerElement[GlobalLimit.Element.MU])
        {
            MuLock.SetActive(true);
        }
        else
        {
            MuLock.SetActive(false);
        }

        if (GlobalLimit.towerElement[GlobalLimit.Element.SHUI])
        {
            ShuiLock.SetActive(true);
        }
        else
        {
            ShuiLock.SetActive(false);
        }

        if (GlobalLimit.towerElement[GlobalLimit.Element.HUO])
        {
            HuoLock.SetActive(true);
        }
        else
        {
            HuoLock.SetActive(false);
        }

        if (GlobalLimit.towerElement[GlobalLimit.Element.TU])
        {
            TuLock.SetActive(true);
        }
        else
        {
            TuLock.SetActive(false);
        }
    }


}
