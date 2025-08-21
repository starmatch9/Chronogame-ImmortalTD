using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuTower : Tower
{
    //  * - ¯�� - *

    //���յ��ˣ�ֱ�Ӹ����˿�Ѫ���󼴿�

    [Header("����ͼ��")]
    public GameObject icon;

    [Header("����ʱ��")]
    [Range(0, 20)] public float furnaceDuration = 2f;

    [Header("ÿ������Ԫ��������")]
    [Range(1, 1000)] public int elementAmount = 100;

    //Ԫ����ϵͳ��Ԫ�����������ű���
    //public ElementManager elementManager;

    //��������дһ��������Ԫ���������������Ѫ�������

    public override void TowerAction()
    {
        if (FindClosestToFinishEnemy() == null)
        {
            return;
        }
        StartCoroutine(furnaceLife());
    }

    //����¯����������
    IEnumerator furnaceLife()
    {
        //�ѵ���������
        GameObject enemy = FindClosestToFinishEnemy().gameObject;

        float absorbDuration = 0.8f;
        float absorbTimer = 0f; //�����ٶ�
        while (absorbTimer < absorbDuration)
        {
            enemy.transform.position = Vector2.Lerp(enemy.transform.position, transform.position, absorbTimer / absorbDuration);

            absorbTimer += Time.deltaTime;
            yield return null;
        }
        //ͣ��һ��
        yield return new WaitForSeconds(0.2f);

        //�����յ������˺�
        enemy.GetComponent<Enemy>().AcceptAttack(99999f);

        //����ͼ��
        icon.SetActive(true);

        //�ȴ�һ��ʱ��
        yield return new WaitForSeconds(furnaceDuration - 0.2f - absorbDuration);

        //����Ԫ����
        GlobalElementPowerFunction.AddCount(elementAmount);

        //����ͼ��
        icon.SetActive(false);

    }

}
