using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextOneBigButton : MonoBehaviour
{
    //����ǰ����
    private void Awake()
    {
        //������Ϸ����
        GlobalEnemyGroupFunction.nextOneBigButton = gameObject;
    }

    //��ť����
    public void ReadyGo()
    {
        GlobalEnemyGroupFunction.StartOneEnemyGroup();
    }

}
