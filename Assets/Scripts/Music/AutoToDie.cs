using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoToDie : MonoBehaviour
{
    //播放完就去死
    void Start()
    {
        StartCoroutine(ToDie());
    }

    IEnumerator ToDie()
    {
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }

}
