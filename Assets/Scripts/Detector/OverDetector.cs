using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverDetector : MonoBehaviour
{
    //防止子弹与敌人越界
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            //赐死
            enemy.AcceptAttack(999999f, GlobalData.AttackAttribute.None, GlobalData.ElementAttribute.NONE);
        }

        if (collision.CompareTag("Bullet"))
        {
            //直接销毁
            Destroy(collision.gameObject);
        }
    }

}
