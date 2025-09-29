using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBull : Enemy
{
    //--精英怪--火牛--火

    //开场燃烧协程
    Coroutine burnCoroutine;

    [Header("开场燃烧的持续时间")]
    public float burnDuration = 5f;

    [Header("燃烧时的移动速度")]
    public float burnSpeed = 20f;

    [Header("木伤次数后延迟熄灭")]
    public int wood = 4;

    [Header("延迟多长时间熄灭")]
    public int woodTime = 4;

    [Header("金伤减伤百分比")]
    public float metalHurtReduce = 0.4f;

    [Header("水伤次数后立即熄灭")]
    public int water = 4;

    //受外部影响的持续时间
    float attachedTime = 0f;

    float oriSpeed;

    int woodCount = 0;

    int waterCount = 0;

    bool stopBurning = false;

    public override void ElementFunction(GlobalData.ElementAttribute elementAttribute)
    {
        switch (elementAttribute)
        {
            //木：延迟熄灭
            case GlobalData.ElementAttribute.MU:
                woodCount++;
                
                if (woodCount >= wood)
                {
                    attachedTime += woodTime;
                }
                break;

            //水：熄灭
            case GlobalData.ElementAttribute.SHUI:
                waterCount++;

                if (waterCount >= water)
                {
                    stopBurning = true;
                }
                break;

        }
    }

    public override float ElementExtraHurt(GlobalData.ElementAttribute elementAttribute, float attack)
    {
        switch (elementAttribute)
        {
            //金：减少伤害
            case GlobalData.ElementAttribute.JIN:
                attack *= (1 - metalHurtReduce);
                break;
        }
        return attack;
    }

    public override void Awake()
    {
        oriSpeed = gameObject.GetComponent<Move>().speed;

        base.Awake();
    }
    //生成时，燃烧
    public override void OtherReset()
    {
        if (burnCoroutine != null)
        {
            StopCoroutine(burnCoroutine);
        }
        burnCoroutine = null;

        attachedTime = 0f;
        woodCount = 0;

        waterCount = 0;

        Move move = gameObject.GetComponent<Move>();
        move.speed = oriSpeed;

        stopBurning = false;
    }

    public override void OtherSpawn()
    {
        burnCoroutine = StartCoroutine(Burn());
    }
    public IEnumerator Burn()
    {
        Move move = gameObject.GetComponent<Move>();
        move.speed = burnSpeed; //燃烧时速度

        float elapsed = 0f;
        while (elapsed < burnDuration + attachedTime)
        {
            if (stopBurning)
            {
                break;
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        move.StopMove();
        move.speed = oriSpeed; //恢复原速
        move.ContinueMove();
    }
}
