using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    //�˴�������������������ʱ�ķ���

    //��������
    LangTower langTower;

    //ά��һ��Vector3
    [HideInInspector]
    public Vector3 point;

    public void SetTower(LangTower l)
    {
        langTower = l;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            //�������б��Ƿ�Ϊ��
            if (langTower.enemies.Count == 0)
            {
                //���Ϊ�գ�����firstPoint
                //langTower.firstPoint = point;
                //��������points�б���֤˳����ȷ
                langTower.ResortPoint(point);

            }

            if (!langTower.enemies.Contains(enemy))
            {
                langTower.enemies.Add(enemy);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            if (!langTower.enemies.Contains(enemy))
            {
                langTower.enemies.Add(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            //���б����Ƴ�����
            if (langTower.enemies.Contains(enemy))
            {
                langTower.enemies.Remove(enemy);
            }
        }
    }
}
