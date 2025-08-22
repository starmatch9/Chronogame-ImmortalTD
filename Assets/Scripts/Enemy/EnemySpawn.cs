using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static GlobalData;

public class EnemySpawn : MonoBehaviour
{
    float timer = 0f; //计时器

    int spawnedEnemyCount = 0; //已生成的敌人数量

    public GameObject enemyPrefab; //敌人预制体

    public int enemyNumber = 20; //最大敌人数量

    int poolSize = 10;   //对象池的容量

    public Tilemap roadTilemap;

    private Queue<GameObject> enemyPool; //敌人对象池

    [Range(0f, 5f)]public float spawnInterval = 1f; //生成间隔时间

    bool isSpawning = false;

    void Start()
    {
        // 初始化对象
        enemyPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            Enemy enemyScript = enemy.GetComponent<Enemy>();

            //这一步很重要
            enemy.GetComponent<Move>().roadTilemap = roadTilemap; //设置路径
            enemy.SetActive(false); //初始时不激活
            enemyScript.SetEnemySpawn(this); //设置生成器
            //依次加入对应数量的敌人
            enemyPool.Enqueue(enemy);
        }
    }

    //开关
    public void Switch()
    {
        isSpawning = true;
    }


    //这种计时方法可以后续改进
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    //注意：此布尔变量即敌人生成的开关
        //    isSpawning = true;
        //}

        if (isSpawning)
        {
            timer += Time.deltaTime;
            //目前每秒生成一个敌人
            if (timer <= spawnInterval) 
            {
                return;
            }
            if (spawnedEnemyCount < enemyNumber)
            {
                SpawnEnemy();
                spawnedEnemyCount++;
            }
            else
            {
                //重置状态
                isSpawning = false;
                spawnedEnemyCount = 0;
            }
            timer = 0f; //重置计时器
        }
    }
    //获取对象
    public GameObject GetEnemy()
    {
        if(enemyPool.Count <= 0)
        {
            /*目前每次扩容5个*/
            ExpandPool(5);
        }
        //取敌人
        return enemyPool.Dequeue();
    }

    //对象池扩容
    void ExpandPool(int additionalSize)
    {
        for (int i = 0; i < additionalSize; i++)
        {
            //要和初始化时一模一样
            GameObject enemy = Instantiate(enemyPrefab);
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemy.GetComponent<Move>().roadTilemap = roadTilemap; //设置路径
            enemy.SetActive(false); //初始时不激活
            enemyScript.SetEnemySpawn(this); //设置生成器
            enemyPool.Enqueue(enemy);
        }
    }

    //回收对象
    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false); //禁用敌人
        enemyPool.Enqueue(enemy); //重新加入队列
    }

    //生成敌人，同时是使用对象池的上下文
    void SpawnEnemy()
    {
        //从对象池获取一个敌人
        GameObject oneEnemy = GetEnemy(); 
        //设置生成位置
        oneEnemy.transform.position = transform.position; 
        //激活敌人
        oneEnemy.SetActive(true);
        //激活的同时调用想过方法
        oneEnemy.GetComponent<Enemy>().GameObjectSpawn();

        Enemy enemyScript = oneEnemy.GetComponent<Enemy>();

        if (!globalEnemies.Contains(enemyScript))
        {
            //添加到全局敌人列表
            globalEnemies.Add(enemyScript);
        }
    }
}
