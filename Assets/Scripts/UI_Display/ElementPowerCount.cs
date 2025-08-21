using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElementPowerCount : MonoBehaviour
{
    //用来显示元素力数量
    int count;
    //获取文本框
    TextMeshProUGUI textBox;

    //貌似这个脚本的Start执行靠后，从而报错，故选择Awake（就这样解决了）
    private void Awake()
    {
        GlobalElementPowerFunction.countDisplay = this;
        count = 0;
        textBox = GetComponent<TextMeshProUGUI>();
        textBox.text = count.ToString();
    }

    //更新数字显示
    public void UpdateDisplay(int newCount)
    {
        count = newCount;
        textBox.text = count.ToString();
    }
}
