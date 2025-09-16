using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverDetector : MonoBehaviour
{
    //��ֹ�ӵ������Խ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            //����
            enemy.AcceptAttack(999999f, GlobalData.AttackAttribute.None, GlobalData.ElementAttribute.NONE);
        }

        if (collision.CompareTag("Bullet"))
        {
            //ֱ������
            Destroy(collision.gameObject);
        }
    }

}
