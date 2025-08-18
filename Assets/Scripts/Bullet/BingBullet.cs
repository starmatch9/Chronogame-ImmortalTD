using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingBullet : Bullet
{
    //    *- ���ӵ� -*

    //Ϊ�˲����ŵ��˵��ڲ��߼�����Ϊ����͹����ˣ�
    //���ﲻ�����ڵ��˽ű���Ӷ����߼�
    //����ͨ��ȫ�ֽű����Ƶ��˶��ᣬ���Ʋ���Freeze.cs�ű��в鿴

    [Header("ѩ��Ԥ�Ƽ�")]
    public GameObject snowPrefab;

    [Header("����ʱ��")]
    [Range(0f, 10f)] public float freezeTime = 3f;

    int maxCount = 0;

    //�ӵ�����ǰ�ۼƶ���
    public override IEnumerator DieAction()
    {
        Enemy targetEnemy = target.GetComponent<Enemy>();
        //�������ǰ�ۼƴ���
        if (!Freeze.enemyHitCount.ContainsKey(targetEnemy))
        {
            //�ֵ���û�У��ͼ���
            Freeze.enemyHitCount.Add(targetEnemy, 1);
        }
        else
        {
            //�еĻ����Ӽ�
            ++Freeze.enemyHitCount[targetEnemy];

            //����������Ƿ����Ŀ��
            if (Freeze.enemyHitCount[targetEnemy] >= maxCount)
            {
                //�Ŷ����߼�


            }

        }


        yield return base.DieAction(); //���û���������߼�
    }


    public void SetMaxCount(int count)
    {
        maxCount = count;
    }

}
