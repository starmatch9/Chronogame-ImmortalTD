using UnityEngine;

//考虑制作接口，使用装饰器模式来实现不同敌人的行为,使用抽象基类
//虚方法可重新
//抽象方法必须重写
public abstract class Enemy : MonoBehaviour
{
    HealthBar healthBar; //血条

    //血量值
    public float health = 100;

    //移动速度
    public float speed = 2f;

    private void Awake()
    {
        //获取血条组件
        healthBar = GetComponentInChildren<HealthBar>();
    }

    //获取血量值
    public float GetHealth() { 
        return health; 
    }

    //扣除血量值
    public void MinusHealth(int attack) {
        health -= attack;

        healthBar.SetHealth(health / 100f); //更新血条显示

        if (health <= 0)
        {
            gameObject.SetActive(false); //敌人死亡
        }
    }

    //判断的依据可以在子类中重写
    //是否不再需要攻击
    public virtual bool NoMoreShotsNeeded() {
        return health <= 0; //如果血量小于等于0，则不再需要攻击
    }    

    //获取敌人游戏对象
    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
