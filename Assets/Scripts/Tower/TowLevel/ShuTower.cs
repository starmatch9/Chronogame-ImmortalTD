using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuTower : Tower
{
    //  ����

    //�ݶ���������Ԫ����

    //����ÿ������Ԫ����������
    [Header("ÿ������Ԫ��������")]
    [Range(1, 1000)]public int elementAmount = 100;

    //Start�����л�ȡԪ������������Ԫ�������ݽ���ȫ�ֹ���

    public override void TowerAction()
    {
        //����Ԫ����
        GlobalElementPowerFunction.AddCount(elementAmount);
    }
}
