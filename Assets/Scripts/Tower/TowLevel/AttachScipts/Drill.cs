using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    float attack = 30f; //��ͷ�ƶ��ٶ�

    public void SetAttack(float attack)
    {
        this.attack = attack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ֻҪ�ǵ��˾Ϳ���
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().AcceptAttack(attack);
        }
    }
}
