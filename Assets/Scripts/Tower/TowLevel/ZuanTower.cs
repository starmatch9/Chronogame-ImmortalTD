using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ZuanTower : Tower
{
    // * 钻头塔 *

    [TextArea]
    public string Tips = "注意：索敌范围指运动轨迹的半径，行为间隔指钻头绕塔转一周的时间。";

    GameObject drill;

    //旋转偏移角度
    float rotationOffset = 180f;

    [Header("造成伤害")]
    [Range(0, 100)] public float damage = 30f;

    Drill drillScript;

    private void Start()
    {
        drill = transform.Find("Drill").gameObject;
        drillScript = drill.GetComponent<Drill>();
        drillScript.SetAttack(damage);
        drill.SetActive(false);
    }

    public override void TowerAction()
    {
        if (!drill.activeInHierarchy)
        {
            drill.SetActive(true);
        }
        StartCoroutine(FlyDrill(actionTime));
    }

    //绕塔转一周
    IEnumerator FlyDrill(float circleTime)
    {
        float timer = 0f;
        float angle = 0f;

        while (timer < circleTime)
        {
            timer += Time.deltaTime;
            angle = -2 * Mathf.PI * (timer / circleTime); //负号表示顺时针

            //计算圆周上的点
            float x = transform.position.x + attackRange * Mathf.Cos(angle);
            float y = transform.position.y + attackRange * Mathf.Sin(angle);

            //更新物体位置
            drill.transform.position = new Vector3(x, y, drill.transform.position.z);

            //计算旋转角度
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            float angleArrow = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            drill.transform.rotation = Quaternion.Euler(0, 0, angleArrow + rotationOffset);

            yield return null; // 每帧更新
        }

        timer = 0f; // 重置计时器
    }


}
