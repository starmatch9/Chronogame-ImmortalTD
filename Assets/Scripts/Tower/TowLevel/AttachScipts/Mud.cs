using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud : MonoBehaviour
{
    //�ٶȼ�Ϊԭ���İٷ�֮����
    float slowFactor = 0.3f;

    public void SetSlowFactor(float slowFactor)
    {
        this.slowFactor = slowFactor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ֻҪ�ǵ��˾ͼ���
        if (collision.CompareTag("Enemy"))
        {
            Move move = collision.GetComponent<Move>();
            if (move != null)
            {
                //��ֹ�ظ����٣��������ٶ�
                move.ResetSpeed();
                move.ChangeSpeed(slowFactor);

            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //ֻҪ�ǵ��ˣ����һ���Ƿ���Ҫ���ּ���״̬
        if (collision.CompareTag("Enemy"))
        {
            Move move = collision.GetComponent<Move>();
            //����ָ���ԭ�٣���������
            if(move.GetSpeed() == move.speed)
            {
                move.ResetSpeed();
                move.ChangeSpeed(slowFactor);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //ֻҪ�ǵ��˾ͻص�ԭ���ٶ�
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
