using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�͵�����һ���������ģ��
//��Ȼһ��ʼ��������������ʣ�ʵ�����á���ͨ���ˡ��ȽϺ���
public class Attribute : Enemy
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            //��ȡ�ӵ������ݽű�
            //����
            MinusHealth(30);
            Destroy(collision.gameObject); //�����ӵ�
        }
    }
}
