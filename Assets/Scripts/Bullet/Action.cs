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

    //��ֹ�ӵ��ɳ���ͼ������
    float maxDistance = 100f;
    Vector3 initialPosition;
    private void Start()
    {
        //��¼��ʼλ��
        initialPosition = transform.position;
    }
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


        //�����룬��Զ����
        float currentDistance = Vector3.Distance(transform.position, initialPosition);
        if (currentDistance >= maxDistance)
        {
            Destroy(gameObject); //����
        }
    }
    public void SetTarget(GameObject enemy)
    {
        target = enemy;
    }
}
