using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingTower : Tower
{

    //    *- �� -*

    //�����ӵ�������û��ʲô�ر�֮��

    //�������Ҫ�ӵ�Ԥ����
    [Header("�ӵ�Ԥ�Ƽ�")]
    public GameObject bingBullet;

    [Header("������κ󶳽�")]
    public int freezeCount = 4;

    [TextArea]
    public string Tips = "ע�⣺�ӵ����˺������ǵ�Ҫȥ�ӵ�Ԥ�Ƽ��������";

    //��дÿ��һ��ʱ��ִ�е���Ϊ
    public override void TowerAction()
    {
        if (FindClosestToFinishEnemy() == null)
        {
            return;
        }
        GameObject target = FindClosestToFinishEnemy().gameObject;

        Shoot(target);
    }

    //�����ӵ����������ӵ�ʵ��
    void Shoot(GameObject enemy)
    {
        // ƫ�� ���ӵ������Ϸ�1.5�׵�λ�÷���
        Vector3 offset = new Vector3(0, 1f, 0);

        //ʵ�����ӵ�
        GameObject bullet = Instantiate(bingBullet, transform.position + offset, Quaternion.identity);

        //��Ҫê���ӵ���Ŀ�꣬��ȡ�ӵ�����Ϊ�ű�
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        //���ö����������
        bullet.GetComponent<BingBullet>().SetMaxCount(freezeCount);

        bulletScript.SetTarget(enemy);
    }
}
