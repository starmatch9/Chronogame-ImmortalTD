using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//子弹的行为基类
public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public GameObject target;

    [Header("是否穿透伤害")]
    public bool penetrate = false;

    [Header("基础伤害（已名存实亡，请前往塔预制件修改）")]
    [Range(0f, 100f)]public float baseAttack = 10f;

    [Header("移动速度")]
    [Range(0f, 20f)]public float moveSpeed = 5f;

    [Header("子弹攻击属性")]
    public GlobalData.AttackAttribute attackAttribute = GlobalData.AttackAttribute.None;

    [Header("子弹元素属性")]
    public GlobalData.ElementAttribute elementAttribute = GlobalData.ElementAttribute.NONE;

    protected Vector3 direction = new Vector3(0, 1, 0);

    //防止子弹飞出地图后不销毁
    protected float maxDistance = 100f;
    protected Vector3 initialPosition;

    //原来贴图的朝向
    protected float rotationOffset = 180f;

    protected bool isStop = false; //是否停止移动

    public virtual void Start()
    {
        //记录初始位置
        initialPosition = transform.position;
    }
    public virtual void Update()
    {
        if (isStop)
        {
            return;
        }
        flyToTarget();
    }

    public virtual void flyToTarget()
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
            //transform.position += direction * moveSpeed * Time.deltaTime;
            //target = null; //清除目标
            Die(null);
        }

        //计算旋转角度
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);

        //检测距离，过远销毁
        float currentDistance = Vector3.Distance(transform.position, initialPosition);
        if (currentDistance >= maxDistance)
        {
            Destroy(gameObject); //销毁
        }
    }

    //基础伤害
    public virtual float GetBaseAttack()
    {
        return baseAttack;
    }


    public virtual void SetTarget(GameObject enemy)
    {
        target = enemy;
    }

    public void StopMove()
    {
        isStop = true;
    }

    public void ResumeMove()
    {
        isStop = false;
    }


    //由敌人调用该方法
    //注意，此时目标敌人已经收到伤害
    public virtual void Die(Enemy enemy)
    {
        //子弹死亡时，也就是销毁前需要调用的逻辑
        StartCoroutine(DieAction());
    }

    //子类重写时，在逻辑后添加Base.DieAction()，以确保调用基类的死亡逻辑

    //死亡的有关行为通过协程实现，方便添加延时操作
    public virtual IEnumerator DieAction()
    {

        yield return StartCoroutine(DestoryBullet());
    }

    public IEnumerator DestoryBullet()
    {
        yield return new WaitForSeconds(0f);
        Destroy(gameObject);
    }

}
