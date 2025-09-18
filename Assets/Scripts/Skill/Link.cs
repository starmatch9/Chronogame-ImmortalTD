using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Link : MonoBehaviour
{
    Vector3 originalPosition;

    bool following = false;

    Coroutine coldCoroutine = null;

    //是否可以点击，即鼠标在塔的上方
    bool canClick = false;

    //被操作的塔对象
    GameObject currentTower = null;

    [Header("UI冷却阴影")]
    public GameObject UI_Shade = null;

    [Header("神秘光电预制件")]
    public GameObject theLight = null;

    [Header("冷却时间(秒)")]
    public float time = 1f;

    [Header("——相生增益倍数——")]
    [Header("子弹伤害")]
    [Range(1f, 20f)]public float a1 = 1f;
    [Header("攻击范围")]
    [Range(1f, 20f)] public float r1 = 1f;
    [Header("子弹间隔时间")]
    [Range(1f, 20f)] public float i1 = 1f;

    [Header("——主克增益倍数——")]
    [Header("子弹伤害")]
    [Range(1f, 20f)] public float a2 = 1f;
    [Header("攻击范围")]
    [Range(1f, 20f)] public float r2 = 1f;
    [Header("子弹间隔时间")]
    [Range(1f, 20f)] public float i2 = 1f;

    //[Header("——被克增益倍数——")]
    //[Header("子弹伤害")]
    //[Range(1f, 20f)] public float a3 = 1f;
    //[Header("攻击范围")]
    //[Range(1f, 20f)] public float r3 = 1f;
    //[Header("子弹间隔时间")]
    //[Range(1f, 20f)] public float i3 = 1f;

    //这是准备连接的两座塔
    GameObject towerA = null;
    GameObject towerB = null;

    void Start()
    {
        originalPosition = transform.position;

        GlobalLink.a1 = a1;
        GlobalLink.a2 = a2;
        GlobalLink.r1 = r1;
        GlobalLink.r2 = r2;
        GlobalLink.i1 = i1;
        GlobalLink.i2 = i2;

        GlobalLink.lightPref = theLight;
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

        if (following && canClick && Input.GetMouseButton(0))
        {
            Prune();

        }
        else if (following && !canClick && Input.GetMouseButton(0))
        {
            //请单击塔以进行连接
            GlobalData.JumpTip("请选中目标塔以进行连接");
        }
    }

    public void Prune()
    {
        if(currentTower == null)
        {
            return;
        }

        if(towerA == null && towerB == null)
        {
            towerA = currentTower;
            currentTower = null;
        }else if(towerA != null && towerB == null)
        {
            //不可选同一个塔
            if(currentTower == towerA) {
                GlobalData.JumpTip("不可连接同一个塔");
                return;
            }
            towerB = currentTower;
            currentTower = null;

            //不可选已经连接的塔
            if (GlobalLink.linkedTowers.ContainsKey(towerA.GetComponent<Tower>()) || GlobalLink.linkedTowers.ContainsKey(towerB.GetComponent<Tower>()))
            {
                GlobalData.JumpTip("不可选择已经连接的塔");
                return;
            }

            LinkTower(towerA, towerB);
        }
    }

    public void LinkTower(GameObject A, GameObject B)
    {
        Tower tA = A.GetComponent<Tower>();
        Tower tB = B.GetComponent<Tower>();

        GlobalData.ElementAttribute eA = tA.attribute;
        GlobalData.ElementAttribute eB = tB.attribute;

        //金生水、金克木
        if(eA == GlobalData.ElementAttribute.JIN && eB == GlobalData.ElementAttribute.SHUI)
        {
            GlobalData.JumpTip("“金生水”");
            Xiang_Sheng(tA, tB);

            StopFollow();
            cold();
        }
        else if (eB == GlobalData.ElementAttribute.JIN && eA == GlobalData.ElementAttribute.SHUI)
        {
            GlobalData.JumpTip("“金生水”");
            Xiang_Sheng(tB, tA);

            StopFollow();
            cold();
        }
        else if (eA == GlobalData.ElementAttribute.JIN && eB == GlobalData.ElementAttribute.MU)
        {
            GlobalData.JumpTip("“金克木”");
            Xiang_Ke(tA, tB);

            StopFollow();
            cold();
        }
        else if (eB == GlobalData.ElementAttribute.JIN && eA == GlobalData.ElementAttribute.MU)
        {
            GlobalData.JumpTip("“金克木”");
            Xiang_Ke(tB, tA);

            StopFollow();
            cold();
        }
        //水生木、水克火
        else if (eA == GlobalData.ElementAttribute.SHUI && eB == GlobalData.ElementAttribute.MU)
        {
            GlobalData.JumpTip("“水生木”");
            Xiang_Sheng(tA, tB);

            StopFollow();
            cold();
        }
        else if (eB == GlobalData.ElementAttribute.SHUI && eA == GlobalData.ElementAttribute.MU)
        {
            GlobalData.JumpTip("“水生木”");
            Xiang_Sheng(tB, tA);

            StopFollow();
            cold();
        }
        else if (eA == GlobalData.ElementAttribute.SHUI && eB == GlobalData.ElementAttribute.HUO)
        {
            GlobalData.JumpTip("“水克火”");
            Xiang_Ke(tA, tB);

            StopFollow();
            cold();
        }
        else if (eB == GlobalData.ElementAttribute.SHUI && eA == GlobalData.ElementAttribute.HUO)
        {
            GlobalData.JumpTip("“水克火”");
            Xiang_Ke(tB, tA);

            StopFollow();
            cold();
        }
        //木生火、木克土
        else if (eA == GlobalData.ElementAttribute.MU && eB == GlobalData.ElementAttribute.HUO)
        {
            GlobalData.JumpTip("“木生火”");
            Xiang_Sheng(tA, tB);

            StopFollow();
            cold();
        }
        else if (eB == GlobalData.ElementAttribute.MU && eA == GlobalData.ElementAttribute.HUO)
        {
            GlobalData.JumpTip("“木生火”");
            Xiang_Sheng(tB, tA);

            StopFollow();
            cold();
        }
        else if (eA == GlobalData.ElementAttribute.MU && eB == GlobalData.ElementAttribute.TU)
        {
            GlobalData.JumpTip("“木克土”");
            Xiang_Ke(tA, tB);

            StopFollow();
            cold();
        }
        else if (eB == GlobalData.ElementAttribute.MU && eA == GlobalData.ElementAttribute.TU)
        {
            GlobalData.JumpTip("“木克土”");
            Xiang_Ke(tB, tA);

            StopFollow();
            cold();
        }
        //火生土、火克金
        else if (eA == GlobalData.ElementAttribute.HUO && eB == GlobalData.ElementAttribute.TU)
        {
            GlobalData.JumpTip("“火生土”");
            Xiang_Sheng(tA, tB);

            StopFollow();
            cold();
        }
        else if (eB == GlobalData.ElementAttribute.HUO && eA == GlobalData.ElementAttribute.TU)
        {
            GlobalData.JumpTip("“火生土”");
            Xiang_Sheng(tB, tA);

            StopFollow();
            cold();
        }
        else if (eA == GlobalData.ElementAttribute.HUO && eB == GlobalData.ElementAttribute.JIN)
        {
            GlobalData.JumpTip("“火克金”");
            Xiang_Ke(tA, tB);

            StopFollow();
            cold();
        }
        else if (eB == GlobalData.ElementAttribute.HUO && eA == GlobalData.ElementAttribute.JIN)
        {
            GlobalData.JumpTip("“火克金”");
            Xiang_Ke(tB, tA);

            StopFollow();
            cold();
        }
        //土生金、土克水
        else if (eA == GlobalData.ElementAttribute.TU && eB == GlobalData.ElementAttribute.JIN)
        {
            GlobalData.JumpTip("“土生金”");
            Xiang_Sheng(tA, tB);

            StopFollow();
            cold();
        }
        else if (eB == GlobalData.ElementAttribute.TU && eA == GlobalData.ElementAttribute.JIN)
        {
            GlobalData.JumpTip("“土生金”");
            Xiang_Sheng(tB, tA);

            StopFollow();
            cold();
        }
        else if (eA == GlobalData.ElementAttribute.TU && eB == GlobalData.ElementAttribute.SHUI)
        {
            GlobalData.JumpTip("“土克水”");
            Xiang_Ke(tA, tB);

            StopFollow();
            cold();
        }
        else if (eB == GlobalData.ElementAttribute.TU && eA == GlobalData.ElementAttribute.SHUI)
        {
            GlobalData.JumpTip("“土克水”");
            Xiang_Ke(tB, tA);

            StopFollow();
            cold();
        }
        else
        {
            GlobalData.JumpTip("请选择正确的相生相克关系");

            StopFollow();
        }
    }


    public void Xiang_Sheng(Tower A, Tower B)
    {
        //A生B

        GlobalLink.ResetBuff();

        TowerLinkMap ALM = new TowerLinkMap();
        ALM.relation = 1;
        ALM.tower = A;
        ALM.originalAttackRange = A.attackRange;
        ALM.originalActionTime = A.actionTime;
        ALM.originalBulletAttack = A.bulletAttack;
        GlobalLink.linkedTowers[A] = ALM;
        GlobalLink.linksTower_1.Add(ALM);


        TowerLinkMap BLM = new TowerLinkMap();
        ALM.relation = 1;
        BLM.tower = B;
        BLM.originalAttackRange = B.attackRange;
        BLM.originalActionTime = B.actionTime;
        BLM.originalBulletAttack = B.bulletAttack;
        GlobalLink.linkedTowers[B] = BLM;
        GlobalLink.linksTower_1.Add(BLM);

        GameObject mysteriousLight = Instantiate(theLight);
        TheLight l = mysteriousLight.GetComponent<TheLight>();
        l.A = A;
        l.B = B;

        TheLink link = new TheLink();
        link.A = A;
        link.B = B;
        link.light = l;
        GlobalLink.theLinks.Add(link);
        link.light.StartMove();

        GlobalLink.FlashLink();
    }

    public void Xiang_Ke(Tower A, Tower B)
    {
        //A克B

        GlobalLink.ResetBuff();

        TowerLinkMap ALM = new TowerLinkMap();
        ALM.relation = 2;
        ALM.tower = A;
        ALM.originalAttackRange = A.attackRange;
        ALM.originalActionTime = A.actionTime;
        ALM.originalBulletAttack = A.bulletAttack;
        GlobalLink.linkedTowers[A] = ALM;
        GlobalLink.linksTower_2.Add(ALM);


        TowerLinkMap BLM = new TowerLinkMap();
        BLM.relation = 3;
        BLM.tower = B;
        BLM.originalAttackRange = B.attackRange;
        BLM.originalActionTime = B.actionTime;
        BLM.originalBulletAttack = B.bulletAttack;
        GlobalLink.linkedTowers[B] = BLM;
        GlobalLink.linksTower_3.Add(BLM);

        GameObject mysteriousLight = Instantiate(theLight);
        TheLight l = mysteriousLight.GetComponent<TheLight>();
        l.A = A;
        l.B = B;

        TheLink link = new TheLink();
        link.A = A;
        link.B = B;
        link.light = l;
        GlobalLink.theLinks.Add(link);
        link.light.StartMove();

        GlobalLink.FlashLink();
    }


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
        while (timer < time)
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
        currentTower = null;
        towerA = null;
        towerB = null;

        following = false;
        GlobalData.towerClick = true;
        transform.position = originalPosition;
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

    //检测塔
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {

            SpriteRenderer renderer = collision.gameObject.GetComponent<SpriteRenderer>();
            //变成半透明
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.7f);

            canClick = true;

            currentTower = collision.transform.parent.gameObject;

            //Debug.Log("我要进来了！！");
        }
    }

    //离开塔
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            SpriteRenderer renderer = collision.gameObject.GetComponent<SpriteRenderer>();
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);

            canClick = false;

            currentTower = null;
        }
    }
}
