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
        if (collision.CompareTag("Bullet") && collision.GetComponent<Action>().target == gameObject)
        {
            //获取子弹的数据脚本
            //测试
            MinusHealth(GetAttack());
            Destroy(collision.gameObject); //销毁子弹
        }
    }

    //注意：获取子弹的攻击力（子类必须实现）！！！！！其必须传入子弹脚本作为参数！！虽然现在没有写
    //子类中，方法的逻辑可能随着敌人抗性、子弹类型等不同而不同！！！！！
    public abstract float GetAttack();

    //游戏对象生成时需要调整的功能
    public void GameObjectSpawn()
    {
        isDead = false;
    }

    //重置敌人状态（在对象池中用的到）
    public void GameObjectReset()
    {
        GetComponent<Move>().survivalTime = 0f; //重置存活时间
        health = maxHealth; //重置血量
        healthBar.SetHealth(health / maxHealth); //更新血条显示
        //重置状态时，进行对象回收
        enemySpawn.ReturnEnemy(gameObject);
    }

    //获取血量值
    public float GetHealth() { 
        return health; 
    }

    //扣除血量值
    public void MinusHealth(float attack) {
        health -= attack;

        healthBar.SetHealth(health / maxHealth); //更新血条显示

        if (health <= 0)
        {
            isDead = true;
            gameObject.SetActive(false); //敌人死亡

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
}
