using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuTower : Tower
{
    //  * 土塔 * 

    [Header("墙的最多阻挡敌人数量")]
    [Range(0, 10)] public int maxNumber = 5;

    [Header("墙的最大持续时间")]
    [Range(0, 10)] public float wallDuration = 5f;

    [Header("墙的倒塌伤害")]
    [Range(0, 100)] public float wallDamage = 40f;

    [Header("墙的预制件")]  //墙的预制件要在y轴向下偏移0.2
    public GameObject wallPrefab;

    [Header("墙倒塌的攻击属性")]
    public GlobalData.AttackAttribute attackAttribute = GlobalData.AttackAttribute.None;

    [Header("墙倒塌的元素属性")]
    public GlobalData.ElementAttribute elementAttribute = GlobalData.ElementAttribute.NONE;


    private void Start()
    {
        TowerAction(); //在开始时执行一次塔的行为
    }

    public override void TowerAction()
    {
        //生成墙
        Enemy enemy = FindClosestToFinishEnemy();
        if (enemy == null)
        {
            return; //没有敌人则不执行
        }
        
        //确定位置
        Vector3 spownPosition = new Vector3(enemy.GetGameObject().transform.position.x, enemy.GetGameObject().transform.position.y - 0.2f, enemy.GetGameObject().transform.position.z);
        GameObject wall = Instantiate(wallPrefab, spownPosition, Quaternion.identity);

        //开始墙的生命周期
        Wall w = wall.GetComponent<Wall>();
        w.elementAttribute = elementAttribute;
        w.attackAttribute = attackAttribute;
        //最大阻挡数量
        w.SetMaxEnemy(maxNumber);
        StartCoroutine(w.WallLife(wallDuration, wallDamage));
    }

}
