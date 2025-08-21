using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuTower : Tower
{
    //  * - 炉塔 - *

    //吸收敌人，直接给敌人扣血极大即可

    [Header("炼丹图标")]
    public GameObject icon;

    [Header("炼丹时长")]
    [Range(0, 20)] public float furnaceDuration = 2f;

    [Header("每次生成元素力数量")]
    [Range(1, 1000)] public int elementAmount = 100;

    //元素力系统（元素力管理器脚本）
    //public ElementManager elementManager;

    //后续可以写一个函数，元素力量的吸收与怪血量正相关

    public override void TowerAction()
    {
        if (FindClosestToFinishEnemy() == null)
        {
            return;
        }
        StartCoroutine(furnaceLife());
    }

    //炼丹炉的生命周期
    IEnumerator furnaceLife()
    {
        //把敌人吸过来
        GameObject enemy = FindClosestToFinishEnemy().gameObject;

        float absorbDuration = 0.8f;
        float absorbTimer = 0f; //吸收速度
        while (absorbTimer < absorbDuration)
        {
            enemy.transform.position = Vector2.Lerp(enemy.transform.position, transform.position, absorbTimer / absorbDuration);

            absorbTimer += Time.deltaTime;
            yield return null;
        }
        //停顿一下
        yield return new WaitForSeconds(0.2f);

        //敌人收到极大伤害
        enemy.GetComponent<Enemy>().AcceptAttack(99999f);

        //激活图标
        icon.SetActive(true);

        //等待一段时间
        yield return new WaitForSeconds(furnaceDuration - 0.2f - absorbDuration);

        //生成元素力
        GlobalElementPowerFunction.AddCount(elementAmount);

        //禁用图标
        icon.SetActive(false);

    }

}
