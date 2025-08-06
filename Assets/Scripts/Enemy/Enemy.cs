using UnityEngine;

//考虑制作接口，使用装饰器模式来实现不同敌人的行为(如特效抗性),使用抽象基类
//虚方法可重新
//抽象方法必须重写
public abstract class Enemy : MonoBehaviour
{
    HealthBar healthBar; //血条

    float maxHealth = 100f; //最大血量

    bool isDead = false; //是否死亡

    //血量值
    public float health = 100;

    //移动速度
    public float speed = 2f;

    private void Awake()
    {
        //获取血条组件
        healthBar = GetComponentInChildren<HealthBar>();
    }

    private void OnEnable()
    {
        //在启用时重置血量
        health = maxHealth; //重置血量
        isDead = false; //重置死亡状态
        healthBar.SetHealth(health / maxHealth); //更新血条显示
    }

    private void OnDisable()
    {
        //在禁用时重置血量
        isDead = true; //重置死亡状态
    }

    //获取血量值
    public float GetHealth() { 
        return health; 
    }

    //扣除血量值
    public void MinusHealth(int attack) {
        health -= attack;

        healthBar.SetHealth(health / maxHealth); //更新血条显示

        if (health <= 0)
        {
            gameObject.SetActive(false); //敌人死亡
        }
    }

    //判断的依据可以在子类中重写
    //是否不再需要攻击
    public virtual bool NoMoreShotsNeeded() {
        return isDead; //如果血量小于等于0，则不再需要攻击
    }    

    //获取敌人游戏对象（……好像有点多此一举了）
    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
