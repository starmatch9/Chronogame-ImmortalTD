using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClick : MonoBehaviour
{
    [Header("关于缩放")]
    //这是轮廓子物体
    public GameObject outline;
    //原来的缩放比例
    Vector3 originalScale;
    //第一级缩放倍数
    public float hoverScale = 1.2f;

    [Header("关于展开")]
    public GameObject optionCanva;//这时，他的缩放是1.428571。为了放置丢失，写在这里

    Vector3 originalCanvaScale;

    //判断用户是否展开选项界面
    bool isSelecting = false;

    Coroutine OpenCloseOptionCavan = null;

    void Awake()
    {
        originalScale = transform.localScale;
        outline.SetActive(false);
        originalCanvaScale = optionCanva.transform.localScale;
    }


    /*以下是鼠标检测方法，前提：必须是碰撞器！！！主摄像机必须添加 Physics2DRaycaster 组件！！！*/
    /*以下两个方法只使用与2D物理系统中的sprite物体！！！*/
    /*想在UI系统使用的话用这个
     * 
     *public void OnPointerEnter(PointerEventData eventData) {}
     *public void OnPointerExit(PointerEventData eventData) {}
     */

    // 鼠标悬停
    public void OnMouseEnter()
    {
        transform.localScale = originalScale * hoverScale;
        outline.SetActive(true);
    }

    // 鼠标离开
    public void OnMouseExit()
    {
        if (isSelecting)
        {
            return;
        }
        transform.localScale = originalScale;
        outline.SetActive(false);
    }

    //鼠标按下
    public void OnMouseDown()
    {
        if (isSelecting)
        {
            return;
        }

        //transform.localScale = originalScale * hoverScale * clickScale;
        transform.localScale = originalScale * hoverScale;
        outline.SetActive(true);

        isSelecting = true;

        //光翼展开！！
        if (OpenCloseOptionCavan != null)
        {
            StopCoroutine(OpenCloseOptionCavan);
            OpenCloseOptionCavan = null;
        }
        OpenCloseOptionCavan = StartCoroutine(optionCancaOpen());

    }

    //鼠标抬起
    public void OnMouseUp()
    {
        //if (isOnHole)
        //{
        //    transform.localScale = originalScale * hoverScale;
        //}
    }

    //
    //关于如何解决鼠标点击其他地方重置状态的记录：
    //建立面板，遮挡其他对象，添加事件点击器重置状态！！很重要的思路！！！！！！！！！！！！！！！！！！！
    //
    public void MouseReset()
    {
        //重置坑大小，重置轮廓
        transform.localScale = originalScale;
        outline.SetActive(false);
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
        GlobalMusic._Window.Play();

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
