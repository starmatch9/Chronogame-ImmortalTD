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

    public int enemyNumber = 10; //最大敌人数量

    public int poolSize = 5;

    public Tilemap roadTilemap;

    private List<GameObject> enemyPool; //敌人对象池

    void Start()
    {
        // 初始化对象池
        enemyPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);

            //这一步很重要
            enemy.GetComponent<Move>().roadTilemap = roadTilemap; //设置路径

            enemy.SetActive(false); //初始时不激活
            enemyPool.Add(enemy);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer <= 2f) //每秒生成一个敌人
        {
            return;
        }

        if (spawnedEnemyCount < enemyNumber)
        {
            SpawnEnemy();

            spawnedEnemyCount++;
        }
        timer = 0f; //重置计时器
    }

    void SpawnEnemy()
    {
        // 查找一个未激活的敌人
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = transform.position; //设置生成位置

                enemy.SetActive(true); //激活敌人
                globalEnemies.Add(enemy.GetComponent<Enemy>()); //添加到全局敌人列表
                return; //只生成一个敌人
            }
        }
    }


}
