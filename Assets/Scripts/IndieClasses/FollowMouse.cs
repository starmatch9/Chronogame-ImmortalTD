using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    //UI图像的RectTransform
    RectTransform rectTransform = null;

    //画布坐标
    RectTransform canvasRectTransform = null;

    //UI的原位置
    Vector2 originalPosition;

    TextMeshProUGUI priceText = null;
    TextMeshProUGUI introText = null;

    bool follow = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        //GetComponentInParent<Canvas>() 方法的作用是：在当前物体自身及其所有父物体（包括直接父物体、间接父物体，直到场景根节点）中，查找第一个类型为 Canvas 的组件并返回
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        //直接获得UI原来的画布内坐标
        originalPosition = rectTransform.localPosition;

        TextMeshProUGUI[] textMeshProUGUIs = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in textMeshProUGUIs)
        {
            if (text.gameObject.name == "PriceText")
            {
                priceText = text;
            }
            if (text.gameObject.name == "IntroText")
            {
                introText = text;
            }
        }
    }

    public void StartFollow(int price, string introduction)
    {
        priceText.text = "售价：" + price;
        introText.text = "    " + introduction;

        follow = true;
    }
    public void StopFollow()
    {
        follow = false;
    }


    //跟随
    void Update()
    {
        if (!follow) { 
            rectTransform.localPosition = originalPosition;
            return;
        }

        //将鼠标位置转换为UI坐标
        Vector2 mousePosition;

        Canvas canvas = canvasRectTransform.GetComponent<Canvas>();

        // 根据Canvas模式处理相机参数
        Camera uiCamera = (canvas.renderMode == RenderMode.ScreenSpaceOverlay) ? null : canvas.worldCamera;

        //_____使用RectTransformUtility将屏幕坐标转换为Canvas内的局部坐标
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,
            Input.mousePosition,
            uiCamera,
            out mousePosition))
        {
            rectTransform.localPosition = mousePosition;
        }
    }

    //
}
