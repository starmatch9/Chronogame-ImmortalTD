using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuTower : Tower
{
    //  * - 炉塔 - *

    //吸收敌人，直接给敌人扣血极大即可

    [Header("炼丹图标")]
    public GameObject icon;

    [Header("炼丹时长（即秒杀间隔）")]
    [Range(0, 20)] public float furnaceDuration = 2f;

    [Header("每次生成元素力数量")]
    [Range(1, 1000)] public int elementAmount = 100;

    [Header("元素力图标游戏对象")]
    public GameObject elementPower = null;

    [Header("子弹预制件")]
    public GameObject bulletPref = null;

    //元素力系统（元素力管理器脚本）
    //public ElementManager elementManager;

    //后续可以写一个函数，元素力量的吸收与怪血量正相关

    float timer0 = 0f; //计时器
    public override void Update()
    {
        //timer记录子弹间隔
        timer += Time.deltaTime;
        if (timer >= actionTime)
        {
            ExecuteAction();
            timer = 0f; //重置计时器
        }
        //timer记录炼丹间隔
        timer0 += Time.deltaTime;
        if (timer0 >= furnaceDuration)
        {
            if (FindClosestToFinishEnemy() == null)
            {
                return;
            }
            GlobalEnemyGroupFunction.mono.StartCoroutine(furnaceLife());
            timer0 = 0f; //重置计时器
        }
    }

    public override void TowerAction()
    {
        if (FindClosestToFinishEnemy() == null)
        {
            return;
        }
        //射击
        GameObject target = FindClosestToFinishEnemy().gameObject;
        Shoot(target);
        //StartCoroutine(furnaceLife());
    }

    //发射子弹，及生成子弹实例
    void Shoot(GameObject enemy)
    {
        // 偏移 ：子弹在塔上方1.5米的位置发射
        Vector3 offset = new Vector3(0, 1f, 0);

        //实例化子弹
        GameObject bullet = Instantiate(bulletPref, transform.position + offset, Quaternion.identity);

        //需要锚定子弹的目标，获取子弹的行为脚本
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        //设置攻击伤害
        bulletScript.baseAttack = bulletAttack;

        bulletScript.SetTarget(enemy);
    }

    //炼丹炉的生命周期
    IEnumerator furnaceLife()
    {
        GlobalMusic.PlayOnce(GlobalMusic._Miss);

        //把敌人吸过来
        GameObject enemy = FindClosestToFinishEnemy().gameObject;
        if (enemy == null)
        {
            yield break;
        }

        float absorbDuration = 0.8f;
        float absorbTimer = 0f; //吸收速度
        while (absorbTimer < absorbDuration)
        {
            if (enemy == null)
            {
                yield break;
            }

            enemy.transform.position = Vector2.Lerp(enemy.transform.position, transform.position, absorbTimer / absorbDuration);

            absorbTimer += Time.deltaTime;
            yield return null;
        }
        //停顿一下
        float stopTimer = 0f;
        while(stopTimer < 0.2f)
        {
            if (enemy == null)
            {
                yield break;
            }
            stopTimer += Time.deltaTime;
            yield return null;
        }

        //yield return new WaitForSeconds(0.2f);

        //敌人收到极大伤害
        enemy.GetComponent<Enemy>().AcceptAttack(999999f, GlobalData.AttackAttribute.None, GlobalData.ElementAttribute.NONE);

        //激活图标
        icon.SetActive(true);

        //等待一段时间
        yield return new WaitForSeconds(furnaceDuration - 0.2f - absorbDuration);

        //元素力增加的协程动画
        GlobalEnemyGroupFunction.mono.StartCoroutine(AddElementPower());

        //生成元素力
        GlobalElementPowerFunction.AddCount(elementAmount);

        //禁用图标
        icon.SetActive(false);
    }

    //元素力标志
    public IEnumerator AddElementPower()
    {
        GlobalMusic.PlayOnce(GlobalMusic._Money);

        SpriteRenderer renderer = elementPower.GetComponent<SpriteRenderer>();
        Vector3 originalposition = elementPower.transform.position;

        //设置动画时间为0.3秒
        float duration = 1f;

        float timer = 0f;

        elementPower.SetActive(true);
        while (timer < duration)
        {
            if (elementPower != null)
            {
                //颜色改变
                float alpha = Mathf.Lerp(1f, 0f, timer / duration);
                Color color = renderer.color;
                color.a = alpha;
                renderer.color = color;

                //向上移动
                float distance = Mathf.Lerp(0f, 0.5f, timer / duration);
                float newY = originalposition.y + distance;
                elementPower.transform.position = new Vector3(originalposition.x, newY, originalposition.z);
            }
            timer += Time.deltaTime;
            yield return null;
        }
        elementPower.SetActive(false);
        elementPower.transform.position = originalposition;

    }

}
