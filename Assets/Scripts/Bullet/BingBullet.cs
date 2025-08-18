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

            //����������Ƿ����Ŀ��   �����ڶ����У�����Ȼ���ظ�ִ�У�
            if (Freeze.enemyHitCount[targetEnemy] == maxCount)
            {
                

                //������Ⱦ������ײ��
                transform.Find("Renderer").GetComponent<Renderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;

                //�Ŷ����߼�
                yield return StartCoroutine(SpawnSnow(targetEnemy));

                //���ô���
                if (Freeze.enemyHitCount.ContainsKey(targetEnemy))
                {
                    Freeze.enemyHitCount[targetEnemy] = 0;
                }
            }
        }

        yield return base.DieAction(); //���û���������߼�
    }

    //����ѩ��
    IEnumerator SpawnSnow(Enemy enemy)
    {
        //����ѩ��ʵ��         (���ĸ�����Ϊ������λ�ã���ʾ��������Ϊ������)
        GameObject snow = Instantiate(snowPrefab, enemy.GetGameObject().transform.position, Quaternion.identity, enemy.GetGameObject().transform);
        //����ѩ���ĳ���ʱ��
        yield return StartCoroutine(SnowLifetime(snow, enemy));
    }

    //ѩ����������Э��
    IEnumerator SnowLifetime(GameObject snow, Enemy enemy)
    {
        enemy.gameObject.GetComponent<Move>().StopMove();

        yield return new WaitForSeconds(freezeTime); //�ȴ�����ʱ��

        Destroy(snow);

        //ʱ�䵽���������
        enemy.gameObject.GetComponent<Move>().ContinueMove();
    }


    public void SetMaxCount(int count)
    {
        maxCount = count;
    }

}
