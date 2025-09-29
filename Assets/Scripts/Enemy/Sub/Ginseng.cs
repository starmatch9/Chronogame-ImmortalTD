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

    [Header("金伤次数后失去能力")]
    public int iron = 4;

    [Header("火伤次数后开始燃烧")]
    public int fire = 3;

    [Header("燃烧持续时间")]
    public float burnDuration = 5f;

    [Header("燃烧时每多少秒造成一次伤害")]
    public float burnInterval = 1f;

    [Header("一次伤害")]
    public float burnHurt = 5f;

    //燃烧协程
    Coroutine burnCoroutine;

    float timer = 0;
    bool isStop = false;
    bool noAbility = false;
    int ironCount = 0;
    int fireCount = 0;

    public void Update()
    {
        if (noAbility)
        {
            return;
        }

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

    public override void ElementFunction(GlobalData.ElementAttribute elementAttribute)
    {
        switch (elementAttribute)
        {
            //金：失去能力
            case GlobalData.ElementAttribute.JIN:
                ironCount++;
                if (ironCount >= iron)
                {
                    noAbility = true;
                }
                break;

            //水：生成小人参精
            case GlobalData.ElementAttribute.SHUI:

                SpawnOne();

                break;

            //火：燃烧（可多次触发）
            case GlobalData.ElementAttribute.HUO:

                fireCount++;
                if (fireCount >= fire)
                {
                    if (burnCoroutine != null)
                    {
                        StopCoroutine(burnCoroutine);
                    }
                    burnCoroutine = StartCoroutine(Burn());

                    fireCount = 0;
                }

                break;

        }
    }

    public IEnumerator Burn()
    {
        float elapsed = 0f;
        while (elapsed < burnDuration)
        {
            if(elapsed % burnInterval >= 0)
            {
                //造成伤害(真伤)
                AcceptAttack(burnHurt, GlobalData.AttackAttribute.None, GlobalData.ElementAttribute.NONE);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    public override void OtherReset()
    {
        if (burnCoroutine != null)
        {
            StopCoroutine(burnCoroutine);
        }
        burnCoroutine = null;

        timer = 0;
        ironCount = 0;
        fireCount = 0;
        noAbility = false;  
    }

    //生成小人参精（不采用对象池）
    void SpawnLittle()
    {
        //当前的生成方式——在本体后方依次生成，移速较快可迅速移动到本体前面
        StartCoroutine(StartSpawnInOrder());
    }

    public void SpawnOne()
    {
        GameObject littleGinseng = Instantiate(littleGinsengPrefab);
        littleGinseng.transform.position = transform.position;

        //让小人参精的方向与本体的移动方向一致
        Move move = littleGinseng.GetComponent<Move>();
        move.StopMove();
        move.survivalTime = GetComponent<Move>().survivalTime;
        move.roadTilemap = GetComponent<Move>().roadTilemap;
        move.direction = GetComponent<Move>().direction;

        LittleGinsengExtractum enemyScript = littleGinseng.GetComponent<LittleGinsengExtractum>();
        enemyScript.attach = true;
        //调用小人参精的生成方法
        enemyScript.GameObjectSpawn();
        if (!GlobalData.globalEnemies.Contains(enemyScript))
        {
            //添加到全局敌人列表
            GlobalData.globalEnemies.Add(enemyScript);
        }

        move.ContinueMove();
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

            SpawnOne();

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
