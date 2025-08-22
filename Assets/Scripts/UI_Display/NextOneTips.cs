using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextOneTips : MonoBehaviour
{

    //此脚本用来单独设置字体样式
    TextMeshProUGUI textMeshPro;

    void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        
        //设置轮廓宽度
        textMeshPro.outlineWidth = 0.1f;
    }

}
