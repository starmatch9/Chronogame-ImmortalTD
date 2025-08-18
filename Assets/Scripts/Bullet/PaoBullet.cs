using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaoBullet : Bullet
{
    [Header("��ը������˺�")]
    [Range(0f, 100f)] public float explosionAttack = 30f;

    [Header("��ը�뾶")]
    [Range(0f, 10f)] public float explosionRange = 2f;

    [Header("�ӵ�������ͼ")]
    public Renderer bulletRenderer;

    [Header("�������ͼ����")]
    public Transform dash;

    public override IEnumerator DieAction()
    {
        //��ͼ����
        bulletRenderer.enabled = false;

        //��ײ������
        GetComponent<Collider2D>().enabled = false;

        //ֹͣ�ƶ�
        StopMove();

        Explode(); //��ը
        yield return StartCoroutine(Dash());

        yield return base.DieAction(); //���û���������߼�
    }

    IEnumerator Dash()
    {
        //0.25���ڽ�dash����Ϊ��ը�뾶�Ĵ�С
        float timer = 0f;
        Vector3 targetScale = Vector3.one * explosionRange * explosionRange;//Ӧ����ƽ������Ҫ����

        //ʱ��
        float timeLength = 0.2f;

        while (timer < timeLength)
        {
            dash.localScale = Vector3.Lerp(Vector3.zero, targetScale, timer / timeLength);
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f); //�ȴ�0.1�룬ģ�ⱬը�ӳ�
    }


    void Explode()
    {
        foreach (Enemy enemy in GlobalData.globalEnemies)
        {
            //��������Ҫ�����ĵ���(һ�������������)
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }

            //������ÿ�����˵ľ���
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            //����ڱ�ը��Χ��
            if (distance <= explosionRange)
            {
                //�Ե�������˺�
                enemy.AcceptAttack(explosionAttack);
            }
        }
    }



    void OnDrawGizmos()
    {
        // ����Gizmo��ɫ
        Gizmos.color = Color.red;
        // ���������ԲȦ
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
