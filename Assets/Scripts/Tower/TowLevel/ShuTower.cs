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

    [Header("Ԫ����ͼ����Ϸ����")]
    public GameObject elementPower = null;

    //Start�����л�ȡԪ������������Ԫ�������ݽ���ȫ�ֹ���

    public override void TowerAction()
    {
        //Ԫ�������ӵ�Э�̶���
        StartCoroutine(AddElementPower());

        //����Ԫ����
        GlobalElementPowerFunction.AddCount(elementAmount);
    }

    public IEnumerator AddElementPower()
    {
        SpriteRenderer renderer = elementPower.GetComponent<SpriteRenderer>();
        Vector3 originalposition = elementPower.transform.position;

        //���ö���ʱ��Ϊ0.3��
        float duration = 1f;

        float timer = 0f;

        elementPower.SetActive(true);
        while (timer < duration) {

            if (elementPower != null)
            {
                //��ɫ�ı�
                float alpha = Mathf.Lerp(1f, 0f, timer / duration);
                Color color = renderer.color;
                color.a = alpha;
                renderer.color = color;

                //�����ƶ�
                float distance = Mathf.Lerp(0f, 0.5f, timer / duration);
                float newY = originalposition.y + distance;
                elementPower.transform.position = new Vector3(originalposition.x, newY, originalposition.z);
            }
            timer += Time.deltaTime;
            yield return null;
        }
        elementPower.SetActive(false);
        elementPower.transform.position = originalposition;
    }
}
