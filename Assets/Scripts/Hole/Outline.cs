using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    //悬停时的缩放比例
    float scaleFactor = 1.2f;

    //轮廓颜色
    Color outlineColor = new Color(0,0,0,0);

    void Awake()
    {
        CreateOutline();
    }

    //通过同精灵缩放的方式创建轮廓
    void CreateOutline()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //获取父物体
        GameObject parent = transform.parent.gameObject;
        //获取父物体的SpriteRenderer组件
        SpriteRenderer parentSpriteRenderer = parent.GetComponent<SpriteRenderer>();
        //让轮廓的精灵与父物体一致
        spriteRenderer.sprite = parentSpriteRenderer.sprite;
        //让轮廓的位置与父物体一致       (这里涉及local相对的概念)
        transform.localPosition = Vector3.zero;
        //让轮廓的大小为父物体的比例倍数
        transform.localScale = Vector3.one * scaleFactor;
        //让排序图层一致
        spriteRenderer.sortingLayerName = parentSpriteRenderer.sortingLayerName;
        //确保轮廓的排序在主体下面
        spriteRenderer.sortingOrder = parentSpriteRenderer.sortingOrder - 1;
        //设置轮廓颜色
        spriteRenderer.color = outlineColor;
    }
}
