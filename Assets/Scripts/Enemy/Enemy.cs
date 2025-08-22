using UnityEngine;

//考虑制作接口，使用装饰器模式来实现不同敌人的行为(如特效抗性),使用抽象基类
//虚方法可重新
//抽象方法必须重写

/*每个子类必须实现OnTriggerEnter方法*/

public abstract class Enemy : MonoBehaviour
{
    EnemySpawn enemySpawn; //敌人生成器

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

    /*注意：这里检测的是子弹！！！*/
    //这个方法检测子弹碰撞，调用MinusHealth方法
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //还得对号入座，瞄准的哪个敌人只能打哪个敌人，而触发不了其他敌人的碰撞器
        if (collision.CompareTag("Bullet") && collision.GetComponent<Bullet>().target == gameObject)
        {
            //获取子弹的数据脚本
            Bullet bullet = collision.GetComponent<Bullet>();

            //测试
            AcceptAttack(GetAttack(bullet));

            //销毁子弹前调用“死”
            //包括死前的逻辑以及销毁本身
            collision.GetComponent<Bullet>().Die();

        }
    }

    //注意：获取子弹的攻击力（子类必须实现）！！！！！其必须传入子弹脚本作为参数！！虽然现在没有写
    //
    //这个只用于实现不同敌人对不同子弹的抗性！！注意，只是子弹！！
    //
    //子类中，方法的逻辑可能随着敌人抗性、子弹类型等不同而不同！！！！！
    public abstract float GetAttack(Bullet bullet);

    //
    //重要：承受攻击！！！！可能需要子类重写！！！！让MinusHealth方法不对外暴露！！！
    //
    //用此方法区分与子弹的统一管理，这个方法用于连接其他类，造成荆棘等特殊伤害
    //
    public virtual void AcceptAttack(float attack)
    {
        MinusHealth(attack);
    }

    //游戏对象生成时需要调整的功能
    public void GameObjectSpawn()
    {
        isDead = false;
    }

    //重置敌人状态（在对象池中用的到）
    public void GameObjectReset()
    {
        GetComponent<Move>().survivalTime = 0f; //重置存活时间
        GetComponent<Move>().ResetSpeed(); //重置速度因子为1
        health = maxHealth; //重置血量
        healthBar.SetHealth(health / maxHealth); //更新血条显示

        //重置冻结状态
        if (Freeze.enemyHitCount.ContainsKey(this))    //记住：ContainsKey用判断字典中是否包含某个键
        {
            Freeze.enemyHitCount.Remove(this); //移除敌人冻结状态
        }

        //重置状态时，进行对象回收
        enemySpawn.ReturnEnemy(gameObject);
    }

    //获取血量值
    public float GetHealth() { 
        return health; 
    }

    //扣除血量值
    void MinusHealth(float attack) {
        health -= attack;

        healthBar.SetHealth(health / maxHealth); //更新血条显示

        if (health <= 0)
        {
            isDead = true;
            gameObject.SetActive(false); //敌人死亡

            //封印术，调用所有死前行为（敌人到达终点时也要使用）
            SealFunction();

            GameObjectReset(); //重置敌人状态
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

    //设置敌人生成器
    public void SetEnemySpawn(EnemySpawn spawn)
    {
        enemySpawn = spawn;
    }

    //敌人死后或到达终点时调用一次的函数(seal的意思是封印，参考团藏死前释放里四象封印术)
    public void SealFunction()
    {
        //基本上都是全局方法了
        GlobalEnemyGroupFunction.CheckEnd();
        
    }
}
