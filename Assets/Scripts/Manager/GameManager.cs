using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("��Ϸʧ�ܵ������������յ������������")]
    [Range(1,10)]public int maxNumber = 5;

    void Awake()
    {
        GlobalData.maxNumber = maxNumber;
    }
}
