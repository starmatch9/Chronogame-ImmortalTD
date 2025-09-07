using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ZuanTower : Tower
{
    // * 钻头塔 *

    [Header("造成伤害")]
    [Range(0, 100)] public float damage = 30f;

    [Header("钻头预制件")]
    public GameObject drill;

    [Header("最多穿透几个敌人")]
    [Range(0, 20)] public int maxPenetrate = 0;

    public override void TowerAction()
    {
        Shoot();
    }

    //向上下左右四个方向发射子弹
    void Shoot()
    {
        //实例化子弹
        GameObject bullet0 = Instantiate(drill, transform.position, Quaternion.identity);
        bullet0.GetComponent<Drill>().SetDir(0);
        bullet0.GetComponent<Drill>().SetPenetrateCount(maxPenetrate);
        GameObject bullet1 = Instantiate(drill, transform.position, Quaternion.identity);
        bullet1.GetComponent<Drill>().SetDir(1);
        bullet1.GetComponent<Drill>().SetPenetrateCount(maxPenetrate);
        GameObject bullet2 = Instantiate(drill, transform.position, Quaternion.identity);
        bullet2.GetComponent<Drill>().SetDir(2);
        bullet2.GetComponent<Drill>().SetPenetrateCount(maxPenetrate);
        GameObject bullet3 = Instantiate(drill, transform.position, Quaternion.identity);
        bullet3.GetComponent<Drill>().SetDir(3);
        bullet3.GetComponent<Drill>().SetPenetrateCount(maxPenetrate);
    }

    //绕塔转一周
    //IEnumerator FlyDrill(float circleTime)
    //{
    //    float timer = 0f;
    //    float angle = 0f;

    //    while (timer < circleTime)
    //    {
    //        timer += Time.deltaTime;
    //        angle = -2 * Mathf.PI * (timer / circleTime); //负号表示顺时针

    //        //计算圆周上的点
    //        float x = transform.position.x + attackRange * Mathf.Cos(angle);
    //        float y = transform.position.y + attackRange * Mathf.Sin(angle);

    //        //更新物体位置
    //        drill.transform.position = new Vector3(x, y, drill.transform.position.z);

    //        //计算旋转角度
    //        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    //        float angleArrow = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //        drill.transform.rotation = Quaternion.Euler(0, 0, angleArrow + rotationOffset);

    //        yield return null; // 每帧更新
    //    }

    //    timer = 0f; // 重置计时器
    //}
}
