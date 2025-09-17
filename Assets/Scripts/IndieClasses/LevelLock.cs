using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLock : MonoBehaviour
{
    public int nextLevel = 0;

    public GameObject Lock = null;
    public GameObject Lock2 = null;

    private void Start()
    {
        if(Lock != null)
        {
            if(nextLevel > GlobalLimit.level)
            {
                Lock.SetActive(true);
            }
            else
            {
                Lock.SetActive(false);
            }
        }

        if (Lock2 != null) 
        {
            if (nextLevel > GlobalLimit.level)
            {
                Lock2.SetActive(true);
            }
            else
            {
                Lock2.SetActive(false);
            }
        }
    }

}
