using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ӵ��ģ������ӵ��ģ������ӵ��ģ�����
//��Ϊ�ӵ�����Ϊ�ű�ֻ����д��Ϊ�߼��������漰�κ���ֵ�߼�
public class Action : MonoBehaviour
{
    [HideInInspector]
    public GameObject target;

    float moveSpeed = 5f;
    Vector3 direction;
    void Update()
    {
        /*�ص�*/
        //
        // - activeSelf��activeInHierarchy������:
        //
        // - activeSelf��ָ������ļ���״̬�����ܸ�����Ӱ�죬��㼶�޹أ��������屻����ʱ���������activeSelf״̬���䡣
        // - activeInHierarchy��ָ�����ڲ㼶�е�ʵ�ʼ���״̬���ܸ�����Ӱ�죬�������屻����ʱ���������activeInHierarchy״̬Ҳ���Ϊfalse��
        //
        if (target != null && target.activeInHierarchy)
        {
            // �����ƶ�����,��һ���������������ڿ����ٶ�
            direction = (target.transform.position - transform.position).normalized;
            // ÿ֡�ƶ�
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            //���û��Ŀ�꣬���ӵ�����ԭ���ķ����ƶ�
            transform.position += direction * moveSpeed * Time.deltaTime;
            target = null; //���Ŀ��
        }
    }
    public void SetTarget(GameObject enemy)
    {
        target = enemy;
    }
}
