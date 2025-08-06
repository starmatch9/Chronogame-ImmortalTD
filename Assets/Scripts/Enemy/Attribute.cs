using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//就当作是一个敌人类的模板
//虽然一开始用了属性这个单词，实际上用“普通敌人”比较合适
public class Attribute : Enemy
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            //获取子弹的数据脚本
            //测试
            MinusHealth(30);
            Destroy(collision.gameObject); //销毁子弹
        }
    }
}
