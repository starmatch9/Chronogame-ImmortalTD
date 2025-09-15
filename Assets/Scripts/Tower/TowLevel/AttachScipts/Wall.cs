using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    //最大敌人数
    int maxEnemy = 0;

    //每次生成一堵墙，所以可以在脚本里维护挡住的敌人列表
    List<Enemy> blockedEnemies = new List<Enemy>();

    [HideInInspector]
    public GlobalData.AttackAttribute attackAttribute = GlobalData.AttackAttribute.None;

    [HideInInspector]
    public GlobalData.ElementAttribute elementAttribute = GlobalData.ElementAttribute.NONE;

    public void SetMaxEnemy(int m)
    {
        maxEnemy = m;
    }

    //进入时，敌人停下
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Move move = collision.GetComponent<Move>();
            Enemy enemy = collision.GetComponent<Enemy>();

            if (enemy != null) {
                //无法移动
                move.StopMove();
                //将敌人添加到列表中
                if (!blockedEnemies.Contains(enemy))
                {
                    blockedEnemies.Add(enemy);
                }
            }
        }
    }

    void FallDown(float damage)
    {
        //防止列表本体变化复制一份
        List<Enemy> enemiesCopy = new List<Enemy>(blockedEnemies);

        foreach (Enemy enemy in enemiesCopy)
        {
            //跳过不需要攻击的敌人(一定不能忘了这段)
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            enemy.AcceptAttack(damage, attackAttribute, elementAttribute);
        }
    }

    public IEnumerator WallLife(float wallDuration, float wallDamage)
    {

        //等待墙的持续时间
        //yield return new WaitForSeconds(wallDuration);

        //计时器
        float timer = 0;
        while(timer <= wallDuration)
        {
            //数量过大退出
            if (blockedEnemies.Count >= maxEnemy)
            {
                break;
            }
            yield return null;
            timer += Time.deltaTime;
        }


        //墙倒塌协程（倒塌时间在这里改）
        yield return StartCoroutine(FallDownRotate(0.3f));

        //墙倒塌
        FallDown(wallDamage);

        //销毁前敌人重新移动
        List<Enemy> enemiesCopy = new List<Enemy>(blockedEnemies);
        foreach (Enemy enemy in enemiesCopy)
        {
            Move move = enemy.gameObject.GetComponent<Move>();
            if (move != null)
            {
                move.ContinueMove(); //恢复敌人移动
            }
        }

        //销毁墙
        Destroy(gameObject);
    }

    IEnumerator FallDownRotate(float fallTime)
    {
        float timer = 0f;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 90f);

        while (timer < fallTime)
        {
            float t = timer / fallTime;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            timer += Time.deltaTime;
            yield return null; //等待下一帧
        }
    }
}
