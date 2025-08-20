using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QiangTower : Tower
{
    //  * ǽ�� * 


    [Header("ǽ�ĳ���ʱ��")]
    [Range(0, 10)]public float wallDuration = 5f;

    [Header("ǽ�ĵ����˺�")]
    [Range(0, 100)]public float wallDamage = 40f;

    [Header("ǽ��Ԥ�Ƽ�")]  //ǽ��Ԥ�Ƽ�Ҫ��y������ƫ��0.2
    public GameObject wallPrefab;

    private void Start()
    {
        TowerAction(); //�ڿ�ʼʱִ��һ��������Ϊ
    }

    public override void TowerAction()
    {

        //����ǽ
        Enemy enemy = FindClosestToFinishEnemy();
        if (enemy == null)
        {
            return; //û�е�����ִ��
        }

        //ȷ��λ��
        Vector3 spownPosition = new Vector3(enemy.GetGameObject().transform.position.x, enemy.GetGameObject().transform.position.y - 0.2f, enemy.GetGameObject().transform.position.z);

        GameObject wall = Instantiate(wallPrefab, spownPosition, Quaternion.identity);

        //��ʼǽ����������
        Wall w = wall.GetComponent<Wall>();
        StartCoroutine(w.WallLife(wallDuration, wallDamage));
    }

}
