using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//�ӵ�����Ϊ����
public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public GameObject target;

    [Header("�����˺�")]
    [Range(0f, 100f)]public float baseAttack = 10f;


    [Header("�ƶ��ٶ�")]
    [Range(0f, 20f)]public float moveSpeed = 5f;

    Vector3 direction;

    //��ֹ�ӵ��ɳ���ͼ������
    float maxDistance = 100f;
    Vector3 initialPosition;

    //ԭ����ͼ�ĳ���
    float rotationOffset = 180f;

    bool isStop = false; //�Ƿ�ֹͣ�ƶ�

    public virtual void Start()
    {
        //��¼��ʼλ��
        initialPosition = transform.position;
    }
    public virtual void Update()
    {
        if (isStop)
        {
            return;
        }
        flyToTarget();
    }

    void flyToTarget()
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

        //������ת�Ƕ�
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);

        //�����룬��Զ����
        float currentDistance = Vector3.Distance(transform.position, initialPosition);
        if (currentDistance >= maxDistance)
        {
            Destroy(gameObject); //����
        }
    }

    //�����˺�
    public virtual float GetBaseAttack()
    {
        return baseAttack;
    }


    public virtual void SetTarget(GameObject enemy)
    {
        target = enemy;
    }

    public void StopMove()
    {
        isStop = true;
    }

    public void ResumeMove()
    {
        isStop = false;
    }


    //�ɵ��˵��ø÷��������಻���������
    //ע�⣬��ʱĿ������Ѿ��յ��˺�
    public void Die()
    {
        //�ӵ�����ʱ��Ҳ��������ǰ��Ҫ���õ��߼�
        StartCoroutine(DieAction());
    }

    //������дʱ�����߼������Base.DieAction()����ȷ�����û���������߼�

    //�������й���Ϊͨ��Э��ʵ�֣����������ʱ����
    public virtual IEnumerator DieAction()
    {

        yield return StartCoroutine(DestoryBullet());
    }

    IEnumerator DestoryBullet()
    {
        yield return new WaitForSeconds(0f);
        Destroy(gameObject);
    }

}
