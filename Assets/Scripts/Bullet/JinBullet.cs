using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class JinBullet : Bullet
{
    //  金子弹  穿透敌人，有数量限制

    //就是在子弹死亡前可以穿透多个敌人
    int maxCount = 0;

    //穿透计数器
    int counter = 0;

    public override void Start()
    {
        rotationOffset = 315f;
        
        base.Start();
    }

    public void SetPenetrateCount(int Penetrate)
    {
        maxCount = Penetrate;
    }

    public override IEnumerator DieAction()
    {
        if(counter < maxCount)
        {
            counter++;

            //立刻推出协程
            yield break;
        }else
        {
            yield return StartCoroutine(DestoryBullet());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision.gameObject == target)
        {
            if (target != null)
            {
                target = null;
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision.gameObject == target)
        {
            if (target != null)
            {
                target = null;
            }
        }
    }

    public override void flyToTarget()
    {
        if (target != null && target.activeInHierarchy)
        {
            direction = (target.transform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            //如果没有目标，则子弹沿着原来的方向移动
            transform.position += direction * moveSpeed * Time.deltaTime;
            target = null; //清除目标
        }

        //一直自转，一秒500度
        transform.Rotate(0, 0, 500f * Time.deltaTime);

        //检测距离，过远销毁
        float currentDistance = Vector3.Distance(transform.position, initialPosition);
        if (currentDistance >= maxDistance)
        {
            Destroy(gameObject); //销毁
        }
    }
}
