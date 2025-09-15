using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ginseng : Enemy
{
    //————人参精（本体）————

    //特殊机制：召唤小人参精

    [Header("小人参精预制件")]
    public GameObject littleGinsengPrefab;

    [Header("本体生成的时间间隔(秒)")]
    public float duration = 4f;

    [Header("每次生成小人参精的数量")]
    public int number = 4;

    [Header("两两小人参精生成时间间隔")]
    public float durationLittle = 0.2f;

    float timer = 0;
    bool isStop = false;
    public void Update()
    {
        if (isStop)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer < duration)
        {
            return;
        }
        SpawnLittle();
        timer = 0;
    }

    //生成小人参精（不采用对象池）
    void SpawnLittle()
    {
        //当前的生成方式——在本体后方依次生成，移速较快可迅速移动到本体前面
        StartCoroutine(StartSpawnInOrder());
    }

    public IEnumerator StartSpawnInOrder()
    {
        for (int i = 0; i < number; i++) {

            while (isStop)
            {
                yield return null;
            }

            while (InCorner(GetComponent<Move>().roadTilemap))
            {
                yield return null;
            }

            GameObject littleGinseng = Instantiate(littleGinsengPrefab);
            littleGinseng.transform.position = transform.position;

            //让小人参精的方向与本体的移动方向一致
            Move move = littleGinseng.GetComponent<Move>();
            move.StopMove();
            move.survivalTime = GetComponent<Move>().survivalTime;
            move.roadTilemap = GetComponent<Move>().roadTilemap;
            move.direction = GetComponent<Move>().direction;

            Enemy enemyScript = littleGinseng.GetComponent<Enemy>();
            //调用小人参精的生成方法
            enemyScript.GameObjectSpawn();
            if (!GlobalData.globalEnemies.Contains(enemyScript))
            {
                //添加到全局敌人列表
                GlobalData.globalEnemies.Add(enemyScript);
            }

            move.ContinueMove();

            yield return new WaitForSeconds(durationLittle);

        }
    }

    //是否在拐角
    bool InCorner(Tilemap t)
    {
        Vector3 currentPosition = transform.position;//当前物体位置
        Vector3 upPosition = currentPosition + new Vector3(0, 1, 0); //上方位置
        Vector3 downPosition = currentPosition + new Vector3(0, -1, 0); //下方位置
        Vector3 leftPosition = currentPosition + new Vector3(-1, 0, 0); //左方位置
        Vector3 rightPosition = currentPosition + new Vector3(1, 0, 0); //右方位置

        if(IsPositionOnTile(upPosition, t) && IsPositionOnTile(leftPosition, t))
        {
            return true;
        }
        if (IsPositionOnTile(upPosition, t) && IsPositionOnTile(rightPosition, t))
        {
            return true;
        }
        if (IsPositionOnTile(downPosition, t) && IsPositionOnTile(rightPosition, t))
        {
            return true;
        }
        if (IsPositionOnTile(downPosition, t) && IsPositionOnTile(leftPosition, t))
        {
            return true;
        }

        return false;
    }

    bool IsPositionOnTile(Vector3 worldPosition, Tilemap roadTilemap)
    {
        //将世界坐标转换为网格坐标
        Vector3Int cellPosition = roadTilemap.WorldToCell(worldPosition);
        //检查该网格位置是否有瓦片
        return roadTilemap.HasTile(cellPosition);
    }

    public override void StopAction()
    {
        isStop = true;
    }

    public override void ContinueAction()
    {
        isStop = false;
    }
}
