using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//子弹的！！！子弹的！！！子弹的！！！
//此为子弹的行为脚本只用来写行为逻辑，不可涉及任何数值逻辑
public class Action : MonoBehaviour
{
    [HideInInspector]
    public GameObject target;

    float moveSpeed = 5f;
    Vector3 direction;

    //防止子弹飞出地图后不销毁
    float maxDistance = 100f;
    Vector3 initialPosition;
    private void Start()
    {
        //记录初始位置
        initialPosition = transform.position;
    }
    void Update()
    {
        /*重点*/
        //
        // - activeSelf与activeInHierarchy的区别:
        //
        // - activeSelf是指对象本身的激活状态，不受父物体影响，与层级无关，当父物体被禁用时，子物体的activeSelf状态不变。
        // - activeInHierarchy是指对象在层级中的实际激活状态，受父物体影响，当父物体被禁用时，子物体的activeInHierarchy状态也会变为false。
        //
        if (target != null && target.activeInHierarchy)
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
            target = null; //清除目标
        }


        //检测距离，过远销毁
        float currentDistance = Vector3.Distance(transform.position, initialPosition);
        if (currentDistance >= maxDistance)
        {
            Destroy(gameObject); //销毁
        }
    }
    public void SetTarget(GameObject enemy)
    {
        target = enemy;
    }
}
