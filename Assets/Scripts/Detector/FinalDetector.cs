using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//终点探测器
public class FinalDetector : MonoBehaviour
{


    //敌人进入，就让全局到达终点的敌人敌人加一
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            //赐死
            enemy.AcceptAttack(999999f, GlobalData.AttackAttribute.None, GlobalData.ElementAttribute.NONE);
            CheckNumber();
        }
    }


    //----------------------注意：
    //
    //这里设置的全局最大数量为5，后续这个变量放在游戏全局管理器中控制
    //
    //

    //检测到到达终点的敌人数量
    public void CheckNumber()
    {
        //数量加以
        ++GlobalData.FinalEnemyNumber;

        //检测是否失败
        GlobalData.CheckFinalEnemy();
    }
}
