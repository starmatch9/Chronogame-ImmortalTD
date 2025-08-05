using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;

    public void SetHealth(float healthPercentage)
    {
        //�˷�����ֵ������0��1֮�䣬�ž���ֵ�ͳ���1�����
        healthPercentage = Mathf.Clamp01(healthPercentage);
        //�������ͼ��������
        fillImage.fillAmount = healthPercentage;
    }

}
