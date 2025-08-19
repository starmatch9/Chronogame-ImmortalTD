using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud : MonoBehaviour
{
    //速度减为原来的百分之多少
    float slowFactor = 0.3f;

    public void SetSlowFactor(float slowFactor)
    {
        this.slowFactor = slowFactor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //只要是敌人就减速
        if (collision.CompareTag("Enemy"))
        {
            Move move = collision.GetComponent<Move>();
            if (move != null)
            {
                //防止重复减速，先重置速度
                move.ResetSpeed();
                move.ChangeSpeed(slowFactor);

            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //只要是敌人，检测一下是否需要保持减速状态
        if (collision.CompareTag("Enemy"))
        {
            Move move = collision.GetComponent<Move>();
            //如果恢复了原速，继续减速
            if(move.GetSpeed() == move.speed)
            {
                move.ResetSpeed();
                move.ChangeSpeed(slowFactor);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //只要是敌人就回到原来速度
        if (collision.CompareTag("Enemy"))
        {
            Move move = collision.GetComponent<Move>();
            if (move != null)
            {
                move.ResetSpeed();
            }
        }
    }
}
