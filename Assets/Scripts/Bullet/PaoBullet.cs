using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaoBullet : Bullet
{
    [Header("爆炸冲击波伤害")]
    [Range(0f, 100f)] public float explosionAttack = 30f;

    [Header("爆炸半径")]
    [Range(0f, 10f)] public float explosionRange = 2f;

    [Header("子弹本体贴图")]
    public Renderer bulletRenderer;

    [Header("冲击波贴图对象")]
    public Transform dash;

    [Header("爆炸的攻击属性")]
    public GlobalData.AttackAttribute PattackAttribute = GlobalData.AttackAttribute.None;

    [Header("爆炸的元素属性")]
    public GlobalData.ElementAttribute PelementAttribute = GlobalData.ElementAttribute.NONE;

    public override IEnumerator DieAction()
    {
        //贴图禁用
        bulletRenderer.enabled = false;

        //碰撞器禁用
        GetComponent<Collider2D>().enabled = false;

        //停止移动
        StopMove();

        Explode(); //爆炸

        yield return StartCoroutine(Dash());

        yield return base.DieAction(); //调用基类的死亡逻辑
    }

    IEnumerator Dash()
    {
        GlobalMusic.PlayOnce(GlobalMusic._Boom);

        //0.25秒内将dash缩放为爆炸半径的大小
        float timer = 0f;
        Vector3 targetScale = Vector3.one * explosionRange * explosionRange;//应该是平方，需要测试

        //时长
        float timeLength = 0.2f;

        while (timer < timeLength)
        {
            dash.localScale = Vector3.Lerp(Vector3.zero, targetScale, timer / timeLength);
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f); //等待0.1秒，模拟爆炸延迟
    }


    void Explode()
    {
        //复制一份
        List<Enemy> temp = new List<Enemy>(GlobalData.globalEnemies);
        foreach (Enemy enemy in temp)
        {
            if (enemy != null) {
                //跳过不需要攻击的敌人(一定不能忘了这段)
                if (enemy.NoMoreShotsNeeded())
                {
                    continue;
                }

                //计算与每个敌人的距离
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                //如果在爆炸范围内
                if (distance <= explosionRange)
                {
                    //对敌人造成伤害
                    enemy.AcceptAttack(explosionAttack, PattackAttribute, PelementAttribute);
                }

            }
        }
    }



    void OnDrawGizmos()
    {
        // 设置Gizmo颜色
        Gizmos.color = Color.red;
        // 绘制无填充圆圈
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
