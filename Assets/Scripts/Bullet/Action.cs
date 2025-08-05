using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ϊ�ӵ�����Ϊ�ű�ֻ����д��Ϊ�߼��������漰�κ���ֵ�߼�
public class Action : MonoBehaviour
{
    GameObject target;
    float moveSpeed = 5f;
    Vector3 direction;
    void Update()
    {
        if (target != null)
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
        }
    }
    public void SetTarget(GameObject enemy)
    {
        target = enemy;
    }
}
