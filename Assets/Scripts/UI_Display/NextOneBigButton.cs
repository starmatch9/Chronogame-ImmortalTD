using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextOneBigButton : MonoBehaviour
{
    //激活前调用
    private void Awake()
    {
        //或许游戏对象
        GlobalEnemyGroupFunction.nextOneBigButton = gameObject;
    }

    //按钮调用
    public void ReadyGo()
    {
        GlobalEnemyGroupFunction.StartOneEnemyGroup();
    }

}
