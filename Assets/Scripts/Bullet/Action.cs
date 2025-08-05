using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//此为子弹的行为脚本只用来写行为逻辑，不可涉及任何数值逻辑
public class Action : MonoBehaviour
{
    GameObject target;
    float moveSpeed = 5f;
    Vector3 direction;
    void Update()
    {
        if (target != null)
        {
            // 计算移动方向,归一化方向向量方便于控制速度
            direction = (target.transform.position - transform.position).normalized;
            // 每帧移动
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            //如果没有目标，则子弹沿着原来的方向移动
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
    public void SetTarget(GameObject enemy)
    {
        target = enemy;
    }
}
