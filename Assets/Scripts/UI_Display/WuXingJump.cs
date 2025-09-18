using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WuXingJump : MonoBehaviour
{
    public GameObject window = null;

    bool isIn = false;

    Coroutine tipCoroutine = null;
    public void Switch()
    {
        if (!isIn)
        {
            //状态切换
            isIn = true;

            //弹出
            if (tipCoroutine != null)
            {

                StopCoroutine(tipCoroutine);
                tipCoroutine = null;
            }
            tipCoroutine = StartCoroutine(JumpIn());
        }
        else
        {
            //状态切换
            isIn = false;

            //弹出
            if (tipCoroutine != null)
            {

                StopCoroutine(tipCoroutine);
                tipCoroutine = null;
            }
            tipCoroutine = StartCoroutine(JumpOut());
        }

    }
    public IEnumerator JumpIn()
    {
        GlobalMusic._Window.Play();

        //修改局部坐标而非世界坐标
        Vector3 original = Vector3.zero;
        window.transform.localPosition = original;
        float timer = 0;
        //一秒钟如何
        while (timer < 0.5f)
        {
            //从0到500插值
            float x = Mathf.Lerp(0, 500, timer / 0.5f);
            Vector3 v = new Vector3(x, window.transform.localPosition.y, window.transform.localPosition.z);
            window.transform.localPosition = v;

            timer += Time.deltaTime;
            yield return null;
        }
        window.transform.localPosition = new Vector3(500, window.transform.localPosition.y, window.transform.localPosition.z);

        tipCoroutine = null;
    }

    public IEnumerator JumpOut()
    {
        GlobalMusic._Window.Play();

        //修改局部坐标而非世界坐标
        Vector3 original = new Vector3(500, 0, 0);
        window.transform.localPosition = original;
        float timer = 0;
        //一秒钟如何
        while (timer < 0.5f)
        {
            //从-200到0插值
            float x = Mathf.Lerp(500, 0, timer / 0.5f);
            Vector3 v = new Vector3(x, window.transform.localPosition.y, window.transform.localPosition.z);
            window.transform.localPosition = v;

            timer += Time.deltaTime;
            yield return null;
        }
        window.transform.localPosition = new Vector3(0, window.transform.localPosition.x, window.transform.localPosition.z);

        tipCoroutine = null;
    }
}
