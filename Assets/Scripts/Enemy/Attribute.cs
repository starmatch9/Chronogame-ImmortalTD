using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//就当作是一个敌人类的模板
//虽然一开始用了属性这个单词，实际上用“普通敌人”比较合适
public class Attribute : Enemy
{
    public override float GetAttack()
    {
        return 30f;
    }

}
