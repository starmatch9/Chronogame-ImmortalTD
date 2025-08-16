using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickTower : MonoBehaviour
{
    [Header("关于缩放")]
    //原来的缩放比例
    Vector3 originalScale;
    //第一级缩放倍数
    public float hoverScale = 1.2f;

    [Header("关于展开")]
    public GameObject optionCanva;

    Vector3 originalCanvaScale;

    //判断用户是否展开选项界面
    bool isSelecting = false;

    Coroutine OpenCloseOptionCavan = null;

    void Awake()
    {
        originalScale = transform.localScale;
        originalCanvaScale = optionCanva.transform.localScale;
    }

    // 鼠标悬停
    public void OnMouseEnter()
    {
        if (isSelecting)
        {
            return;
        }
        transform.localScale = originalScale * hoverScale;
    }

    // 鼠标离开
    public void OnMouseExit()
    {
        if (isSelecting)
        {
            return;
        }
        transform.localScale = originalScale;
    }

    //鼠标按下
    public void OnMouseDown()
    {
        if (isSelecting)
        {
            return;
        }
        //返回原来的大小
        transform.localScale = originalScale;

        isSelecting = true;
        //光翼展开！！
        if (OpenCloseOptionCavan != null)
        {
            StopCoroutine(OpenCloseOptionCavan);
            OpenCloseOptionCavan = null;
        }
        OpenCloseOptionCavan = StartCoroutine(optionCancaOpen());
    }

    public void MouseReset()
    {
        //重置大小
        transform.localScale = originalScale;
        //重置选项界面
        isSelecting = false;

        //收
        if (OpenCloseOptionCavan != null)
        {
            StopCoroutine(OpenCloseOptionCavan);
            OpenCloseOptionCavan = null;
        }
        OpenCloseOptionCavan = StartCoroutine(optionCancaClose());
    }

    IEnumerator optionCancaOpen()
    {
        optionCanva.transform.localScale = Vector3.zero;
        optionCanva.SetActive(true);
        float timer = 0;
        //这里调节时常（有两个地方，别漏了）
        while (timer < 0.3f)
        {
            //从零到原始缩放比例，插值
            optionCanva.transform.localScale = Vector3.Lerp(Vector3.zero, originalCanvaScale, timer / 0.3f);
            timer += Time.deltaTime;
            yield return null;
        }
        optionCanva.transform.localScale = originalCanvaScale;
    }

    IEnumerator optionCancaClose()
    {
        float timer = 0;
        while (timer < 0.3f)
        {
            //从原始缩放比例到零，插值
            optionCanva.transform.localScale = Vector3.Lerp(originalCanvaScale, Vector3.zero, timer / 0.3f);
            timer += Time.deltaTime;
            yield return null;
        }
        optionCanva.SetActive(false);
        optionCanva.transform.localScale = originalCanvaScale;
    }
}
