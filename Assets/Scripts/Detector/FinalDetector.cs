using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�յ�̽����
public class FinalDetector : MonoBehaviour
{


    //���˽��룬����ȫ�ֵ����յ�ĵ��˵��˼�һ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            //����
            enemy.AcceptAttack(99999f);
            CheckNumber();
        }
    }


    //----------------------ע�⣺
    //
    //�������õ�ȫ���������Ϊ5�������������������Ϸȫ�ֹ������п���
    //
    //

    //��⵽�����յ�ĵ�������
    public void CheckNumber()
    {
        //��������
        ++GlobalData.FinalEnemyNumber;

        //����Ƿ�ʧ��
        GlobalData.CheckFinalEnemy();
    }
}
