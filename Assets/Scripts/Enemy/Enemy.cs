using UnityEngine;

//考虑制作接口，使用装饰器模式来实现不同敌人的行为(如特效抗性),使用抽象基类
//虚方法可重新
//抽象方法必须重写

/*每个子类必须实现OnTriggerEnter方法*/

public abstract class Enemy : MonoBehaviour
{
    EnemySpawn enemySpawn = null; //敌人生成器

    HealthBar healthBar; //血条

    [Header("血量")]
    public float maxHealth = 100f; //最大血量

    [Header("受伤害倍率（用于增伤减伤）")]
    float hurtRate = 1f;  //收到伤害的倍率，用来增伤

    [Header("元素属性")]  //默认为无属性
    public GlobalData.ElementAttribute elementAttribute = GlobalData.ElementAttribute.NONE;

    [Header("物理防御减伤百分比")]
    [Range(0, 1f)]public float physicsDefense = 0f;

    [Header("魔法防御减伤百分比")]
    [Range(0, 1f)]public float magicDefense = 0f;

    [Header("是否无敌")]
    public bool unbeatable = false;

    [Header("掉落元素力数量")]
    public int dropElementPower = 50;

    //血量值，用于计算与显示的值
    float health = 100f;

    bool isDead = false; //是否死亡

    public virtual void Awake()
    {
        //获取血条组件
        healthBar = GetComponentInChildren<HealthBar>();
        health = maxHealth;
    }

    //设置伤害倍率
    public void SetHurtRate(float defenseFactor)
    {
        hurtRate = defenseFactor;
    }
    //重置伤害倍率
    public void ResetHurtRate()
    {
        hurtRate = 1f;
    }

    /*注意：这里检测的是子弹！！！*/
    //这个方法检测子弹碰撞，调用MinusHealth方法
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //还得对号入座，瞄准的哪个敌人只能打哪个敌人，而触发不了其他敌人的碰撞器
        if (collision.CompareTag("Bullet") && (collision.GetComponent<Bullet>().target == gameObject || collision.GetComponent<Bullet>().penetrate ))
        {
            //获取子弹的数据脚本
            Bullet bullet = collision.GetComponent<Bullet>();

            //测试
            AcceptAttack(GetAttack(bullet), bullet.attackAttribute, bullet.elementAttribute);

            //销毁子弹前调用“死”
            //包括死前的逻辑以及销毁本身
            collision.GetComponent<Bullet>().Die(this);

        }
    }

    public float GetAttack(Bullet bullet)
    {
        return bullet.GetBaseAttack();
    }

    //重要：承受攻击！！！！可能需要子类重写！！！！让MinusHealth方法不对外暴露！！！
    //传入攻击属性和元素属性
    public void AcceptAttack(float attack, GlobalData.AttackAttribute attackAttribute, GlobalData.ElementAttribute elementAttribute)
    {
        //受到伤害前可能进行的行为
        ActionBeforeAttack();

        //根据元素属性条用特殊机制
        ElementFunction(elementAttribute);

        //敌人处于此状态时，无法受到伤害（无敌状态），行为机制不受影响
        if (unbeatable)
        {
            return;
        }

        //根据魔法或物理属性计算伤害
        float value = 0;
        if(attackAttribute == GlobalData.AttackAttribute.Magic)
        {
            //魔攻
            value = attack - attack * magicDefense;
        }
        else if (attackAttribute == GlobalData.AttackAttribute.Physics)
        {
            //物攻
            value = attack - attack * physicsDefense;
        }
        else
        {
            //真伤
            value = attack;
        }

        //元素克制可能引发的增伤减伤行为
        value = ElementExtraHurt(elementAttribute, value);

        //在属性伤害计算完成后乘以倍率（目前只有冰冻子弹用到了）
        value *= hurtRate;

        MinusHealth(value);
    }

    /*下面是几个可能需要重写的函数*/

    //受到伤害前进行的行为
    public virtual void ActionBeforeAttack()
    {

    }

    //实现元素机制的行为
    public virtual void ElementFunction(GlobalData.ElementAttribute elementAttribute)
    {

    }

    //元素克制可能引发的增伤减伤行为
    public virtual float ElementExtraHurt(GlobalData.ElementAttribute elementAttribute, float attack)
    {
        return attack;
    }

    //一些由子类衍生出的需要重置的条目,也是敌人死亡时的动作
    public virtual void OtherReset()
    {

    }

    //一些由子类衍生出的生成时需要执行的其他行为
    public virtual void OtherSpawn()
    {

    }

    //游戏暂停时需要停止的行为
    public virtual void StopAction() 
    { 
    
    }
    //游戏继续时需要停止的行为
    public virtual void ContinueAction()
    {

    }

    //敌人死亡时调用的行为
    public virtual void Die()
    {

    }

    //判断的依据可以在子类中重写
    //是否不再需要攻击
    public virtual bool NoMoreShotsNeeded()
    {
        return isDead; //如果血量小于等于0，则不再需要攻击
    }

    //游戏对象生成时需要调整的功能
    public void GameObjectSpawn()
    {
        isDead = false;

        OtherSpawn();
    }

    //重置敌人状态（在对象池中用的到）
    public void GameObjectReset()
    {
        GetComponent<Move>().survivalTime = 0f; //重置存活时间
        GetComponent<Move>().SetSpeedFactor(1f); ; //重置速度因子为1
        health = maxHealth; //重置血量
        healthBar.SetHealth(health / maxHealth); //更新血条显示

        //重置冻结状态
        if (Freeze.enemyHitCount.ContainsKey(this))    //记住：ContainsKey用判断字典中是否包含某个键
        {
            Freeze.enemyHitCount.Remove(this); //移除敌人冻结状态
        }
        //重置状态时，进行对象回收
        if(enemySpawn != null)
        {
            enemySpawn.ReturnEnemy(gameObject);
        }

        OtherReset();
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

            Die();

            //封印术，调用所有死前行为（敌人到达终点时也要使用）
            SealFunction();

            GameObjectReset(); //重置敌人状态
        }
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
        if(dropElementPower != 0)
        {
            GlobalElementPowerFunction.mono.StartCoroutine(GlobalElementPowerFunction.AddElementPowerAnim());
            //生成元素力
            GlobalElementPowerFunction.AddCount(dropElementPower);
        }

        //基本上都是全局方法了
        GlobalEnemyGroupFunction.CheckEnd();
    }
}
