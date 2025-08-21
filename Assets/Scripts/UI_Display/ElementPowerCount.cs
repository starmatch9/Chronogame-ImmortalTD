using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElementPowerCount : MonoBehaviour
{
    //������ʾԪ��������
    int count;
    //��ȡ�ı���
    TextMeshProUGUI textBox;

    //ò������ű���Startִ�п��󣬴Ӷ�������ѡ��Awake������������ˣ�
    private void Awake()
    {
        GlobalElementPowerFunction.countDisplay = this;
        count = 0;
        textBox = GetComponent<TextMeshProUGUI>();
        textBox.text = count.ToString();
    }

    //����������ʾ
    public void UpdateDisplay(int newCount)
    {
        count = newCount;
        textBox.text = count.ToString();
    }
}
