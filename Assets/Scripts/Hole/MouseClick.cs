using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClick : MonoBehaviour
{
    //这是轮廓子物体
    public GameObject outline;

    //原来的缩放比例
    Vector3 originalScale;

    //缩放倍数
    public float hoverScale = 1.3f;

    void Awake()
    {
        originalScale = transform.localScale;
        outline.SetActive(false);
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
        transform.localScale = originalScale;
        outline.SetActive(false);
    }
}
