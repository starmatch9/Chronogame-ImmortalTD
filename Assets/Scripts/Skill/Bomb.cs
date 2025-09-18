using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Bomb : MonoBehaviour
{
    Vector3 originalPosition;

    bool following = false;

    Coroutine coldCoroutine = null;

    [Header("UI冷却阴影")]
    public GameObject UI_Shade = null;

    [Header("冷却时间(秒)")]
    public float time = 1f;

    [Header("爆炸冲击波伤害")]
    [Range(0f, 100f)] public float explosionAttack = 30f;

    [Header("爆炸半径")]
    [Range(0f, 10f)] public float explosionRange = 2f;

    [Header("冲击波贴图对象")]
    public Transform dash;

    [Header("爆炸的攻击属性")]
    public GlobalData.AttackAttribute PattackAttribute = GlobalData.AttackAttribute.None;

    [Header("爆炸的元素属性")]
    public GlobalData.ElementAttribute PelementAttribute = GlobalData.ElementAttribute.NONE;


    //开始冷却函数
    public void cold()
    {
        coldCoroutine = StartCoroutine(fill());
    }

    IEnumerator fill()
    {
        UI_Shade.SetActive(true);
        Image image = UI_Shade.GetComponent<Image>();
        image.fillAmount = 1f;

        float timer = 0;
        while(timer < time)
        {
            image.fillAmount = Mathf.Lerp(1f, 0f, timer / time);

            timer += Time.deltaTime;
            yield return null;
        }

        UI_Shade.SetActive(false);
    }

    //开始跟随鼠标
    public void StartFollow()
    {
        GlobalData.JumpTip("右击取消技能释放");

        following = true;
        GlobalData.towerClick = false;
    }

    //停止跟随鼠标
    public void StopFollow()
    {
        following = false;
        GlobalData.towerClick = true;
        transform.position = originalPosition;
    }

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        //右击取消
        if (following && Input.GetMouseButton(1))
        {
            StopFollow();
        }

        if (following)
        {
            FollowMousePosition();
        }

        if (following && Input.GetMouseButton(0))
        {
            //执行功能
            Explosion();

            
        }
    }


    //跟随方法主题
    private void FollowMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        //设置z坐标为物体到相机的距离，否则看不见
        mousePosition.z = 4.5f;
        //转换为世界坐标
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = originalPosition.z;
        //更新物体位置
        transform.position = worldPosition;
    }

    void Explosion()
    {
        //检测路面、爆炸、重新计时
        if (InRoad())
        {
            following = false;
            //Debug.Log("爆炸爆炸");
            Explode();
            StartCoroutine(Dash());

            cold();

            return;
        }
        else
        {
            GlobalData.JumpTip("爆炸技能只能放置在路面上");
            StopFollow();
            return;
        }

    }

    //复制一份炮子弹（
    IEnumerator Dash()
    {
        GetComponent<SpriteRenderer>().enabled = false;

        //0.25秒内将dash缩放为爆炸半径的大小
        float timer = 0f;
        Vector3 targetScale = Vector3.one * explosionRange * explosionRange;//应该是平方，需要测试

        //时长
        float timeLength = 0.2f;

        while (timer < timeLength)
        {
            dash.localScale = Vector3.Lerp(Vector3.zero, targetScale, timer / timeLength);
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f); //等待0.1秒，模拟爆炸延迟

        dash.localScale = Vector3.zero;
        GetComponent<SpriteRenderer>().enabled = true;
        StopFollow();
    }


    void Explode()
    {
        //复制一份
        List<Enemy> temp = new List<Enemy>(GlobalData.globalEnemies);
        foreach (Enemy enemy in temp)
        {
            if (enemy != null)
            {
                //跳过不需要攻击的敌人(一定不能忘了这段)
                if (enemy.NoMoreShotsNeeded())
                {
                    continue;
                }

                //计算与每个敌人的距离
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                //如果在爆炸范围内
                if (distance <= explosionRange)
                {
                    //对敌人造成伤害
                    enemy.AcceptAttack(explosionAttack, PattackAttribute, PelementAttribute);
                }

            }
        }
    }

    bool InRoad()
    {
        //获取鼠标在屏幕上的位置
        Vector3 mouseScreenPosition = Input.mousePosition;

        //将屏幕坐标转换为世界坐标
        //注意：需要给z坐标一个值，通常使用相机到XY平面的距离
        mouseScreenPosition.z = Camera.main.nearClipPlane;
        Vector3 point = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        //列表不为空时，判断是否在路面上
        if (GlobalData.globalRoads.Count != 0)
        {
            foreach (Tilemap tilemap in GlobalData.globalRoads)
            {
                //如果在路面上
                if (tilemap.HasTile(tilemap.WorldToCell(point)))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
