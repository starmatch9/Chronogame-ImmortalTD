using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextOneTips : MonoBehaviour
{

    //�˽ű�������������������ʽ
    TextMeshProUGUI textMeshPro;

    void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        
        //�����������
        textMeshPro.outlineWidth = 0.1f;
    }

}
