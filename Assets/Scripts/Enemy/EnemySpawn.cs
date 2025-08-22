using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static GlobalData;

public class EnemySpawn : MonoBehaviour
{
    float timer = 0f; //��ʱ��

    int spawnedEnemyCount = 0; //�����ɵĵ�������

    public GameObject enemyPrefab; //����Ԥ����

    public int enemyNumber = 20; //����������

    int poolSize = 10;   //����ص�����

    public Tilemap roadTilemap;

    private Queue<GameObject> enemyPool; //���˶����

    [Range(0f, 5f)]public float spawnInterval = 1f; //���ɼ��ʱ��

    bool isSpawning = false;

    void Start()
    {
        // ��ʼ������
        enemyPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            Enemy enemyScript = enemy.GetComponent<Enemy>();

            //��һ������Ҫ
            enemy.GetComponent<Move>().roadTilemap = roadTilemap; //����·��
            enemy.SetActive(false); //��ʼʱ������
            enemyScript.SetEnemySpawn(this); //����������
            //���μ����Ӧ�����ĵ���
            enemyPool.Enqueue(enemy);
        }
    }

    //����
    public void Switch()
    {
        isSpawning = true;
    }


    //���ּ�ʱ�������Ժ����Ľ�
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    //ע�⣺�˲����������������ɵĿ���
        //    isSpawning = true;
        //}

        if (isSpawning)
        {
            timer += Time.deltaTime;
            //Ŀǰÿ������һ������
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
                //����״̬
                isSpawning = false;
                spawnedEnemyCount = 0;
            }
            timer = 0f; //���ü�ʱ��
        }
    }
    //��ȡ����
    public GameObject GetEnemy()
    {
        if(enemyPool.Count <= 0)
        {
            /*Ŀǰÿ������5��*/
            ExpandPool(5);
        }
        //ȡ����
        return enemyPool.Dequeue();
    }

    //���������
    void ExpandPool(int additionalSize)
    {
        for (int i = 0; i < additionalSize; i++)
        {
            //Ҫ�ͳ�ʼ��ʱһģһ��
            GameObject enemy = Instantiate(enemyPrefab);
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemy.GetComponent<Move>().roadTilemap = roadTilemap; //����·��
            enemy.SetActive(false); //��ʼʱ������
            enemyScript.SetEnemySpawn(this); //����������
            enemyPool.Enqueue(enemy);
        }
    }

    //���ն���
    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false); //���õ���
        enemyPool.Enqueue(enemy); //���¼������
    }

    //���ɵ��ˣ�ͬʱ��ʹ�ö���ص�������
    void SpawnEnemy()
    {
        //�Ӷ���ػ�ȡһ������
        GameObject oneEnemy = GetEnemy(); 
        //��������λ��
        oneEnemy.transform.position = transform.position; 
        //�������
        oneEnemy.SetActive(true);
        //�����ͬʱ�����������
        oneEnemy.GetComponent<Enemy>().GameObjectSpawn();

        Enemy enemyScript = oneEnemy.GetComponent<Enemy>();

        if (!globalEnemies.Contains(enemyScript))
        {
            //��ӵ�ȫ�ֵ����б�
            globalEnemies.Add(enemyScript);
        }
    }
}
