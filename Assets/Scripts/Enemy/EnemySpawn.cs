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

    public int enemyNumber = 10; //����������

    public int poolSize = 5;

    public Tilemap roadTilemap;

    private List<GameObject> enemyPool; //���˶����

    void Start()
    {
        // ��ʼ�������
        enemyPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);

            //��һ������Ҫ
            enemy.GetComponent<Move>().roadTilemap = roadTilemap; //����·��

            enemy.SetActive(false); //��ʼʱ������
            enemyPool.Add(enemy);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer <= 2f) //ÿ������һ������
        {
            return;
        }

        if (spawnedEnemyCount < enemyNumber)
        {
            SpawnEnemy();

            spawnedEnemyCount++;
        }
        timer = 0f; //���ü�ʱ��
    }

    void SpawnEnemy()
    {
        // ����һ��δ����ĵ���
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = transform.position; //��������λ��

                enemy.SetActive(true); //�������
                globalEnemies.Add(enemy.GetComponent<Enemy>()); //��ӵ�ȫ�ֵ����б�
                return; //ֻ����һ������
            }
        }
    }


}
