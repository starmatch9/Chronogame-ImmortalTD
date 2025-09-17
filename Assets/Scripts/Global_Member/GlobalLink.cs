using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class GlobalLink
{
    //约定：1为相生增益，2为主克增益，3为被克失效

    //存放被连接的塔的列表
    public static Dictionary<Tower, TowerLinkMap> linkedTowers = new Dictionary<Tower, TowerLinkMap>();

    //存放相生增益的塔列表
    public static List<TowerLinkMap> linksTower_1 = new List<TowerLinkMap>();

    //存放主克增益的塔列表
    public static List<TowerLinkMap> linksTower_2 = new List<TowerLinkMap>();

    //存放被克失效的塔列表
    public static List<TowerLinkMap> linksTower_3 = new List<TowerLinkMap>();

    //存放连接的塔对儿
    public static List<TheLink> theLinks = new List<TheLink>();

    //[Header("——相生增益倍数——")]
    //[Header("子弹伤害")]
    public static float a1 = 1f;
    //[Header("攻击范围")]
    public static float r1 = 1f;
    //[Header("子弹间隔时间")]
    public static float i1 = 1f;

    //[Header("——主克增益倍数——")]
    //[Header("子弹伤害")]
    public static float a2 = 1f;
    //[Header("攻击范围")]
    public static float r2 = 1f;
    //[Header("子弹间隔时间")]
    public static float i2 = 1f;

    //刷新连接的方法，刷新后即为增幅后的塔
    public static void FlashLink()
    {
        foreach(var l in linksTower_1)
        {
            Tower tower = l.tower;

            tower.enabled = true;
            tower.actionTime = linkedTowers[tower].originalActionTime * i1;
            tower.bulletAttack = linkedTowers[tower].originalBulletAttack * a1;
            tower.attackRange = linkedTowers[tower].originalAttackRange * r1;
        }

        foreach (var l in linksTower_2)
        {
            Tower tower = l.tower;

            tower.enabled = true;
            tower.actionTime = linkedTowers[tower].originalActionTime * i2;
            tower.bulletAttack = linkedTowers[tower].originalBulletAttack * a2;
            tower.attackRange = linkedTowers[tower].originalAttackRange * r2;
        }

        foreach (var l in linksTower_3)
        {
            Tower tower = l.tower;

            tower.enabled = false;
        }

        //遍历塔对进行贴图连接显示
        foreach (var link in theLinks)
        {
            //
            //
            Debug.Log(link.A.gameObject.name);
            Debug.Log(link.B.gameObject.name);
        }
    }

    public static void ResetBuff()
    {
        foreach(Tower tower in GlobalData.towers)
        {
            if (linkedTowers.ContainsKey(tower))
            {
                tower.enabled = true;
                tower.actionTime = linkedTowers[tower].originalActionTime;
                tower.bulletAttack = linkedTowers[tower].originalBulletAttack;
                tower.attackRange = linkedTowers[tower].originalAttackRange;
            }

        }
    }

    //找到连接关系
    public static TheLink FindTheLink(Tower t)
    {
        foreach (var link in theLinks)
        {

            if(link.A == t) return link;
            if(link.B == t) return link;
        }

        return null;
    }

    //删除连接关系(摧毁塔用)
    public static void RemoveLink(Tower t)
    {
        if (!linkedTowers.ContainsKey(t))
        {
            return;
        }

        //修改前先重置buff
        ResetBuff();

        TheLink link = FindTheLink(t);

        Tower A = link.A;
        Tower B = link.B;

        int a = linkedTowers[A].relation;
        int b = linkedTowers[B].relation;

        if (a == 1)
        {
            linksTower_1.Remove(linkedTowers[A]);
        }
        else if (a == 2)
        {
            linksTower_2.Remove(linkedTowers[A]);
        }
        else if (a == 3)
        {
            linksTower_3.Remove(linkedTowers[A]);
        }

        if (b == 1)
        {
            linksTower_1.Remove(linkedTowers[B]);
        }
        else if (b == 2)
        {
            linksTower_2.Remove(linkedTowers[B]);
        }
        else if (b == 3)
        {
            linksTower_3.Remove(linkedTowers[B]);
        }

        linkedTowers.Remove(A);
        linkedTowers.Remove(B);

        theLinks.Remove(link);

        //刷新连接
        FlashLink();
    }

    //用A替换B（升级用）
    public static void Inherit(Tower A, Tower B)
    {
        //升级前全部重置buff，升级后再刷新
        ResetBuff();

        if (linkedTowers.ContainsKey(B))
        {
            //先把A添加进去
            TowerLinkMap LM = new TowerLinkMap();
            LM.relation = linkedTowers[B].relation;
            LM.tower = A;
            LM.originalAttackRange = A.attackRange;
            LM.originalActionTime = A.actionTime;
            LM.originalBulletAttack = A.bulletAttack;
            linkedTowers[A] = LM;

            int r = linkedTowers[B].relation;
            if (r == 1)
            {
                linksTower_1.Remove(linkedTowers[B]);
                linksTower_1.Add(linkedTowers[A]);

                TheLink link = FindTheLink(B);
                if(link.A == B)
                {
                    TheLink newLink = new TheLink();
                    newLink.B = link.B;
                    newLink.A = A;

                    theLinks.Add(newLink);
                }
                else if (link.B == B)
                {
                    TheLink newLink = new TheLink();
                    newLink.A = link.A;
                    newLink.B = A;

                    theLinks.Add(newLink);
                }
                theLinks.Remove(link);

            }
            else if (r == 2)
            {
                linksTower_2.Remove(linkedTowers[B]);
                linksTower_2.Add(linkedTowers[A]);

                TheLink link = FindTheLink(B);
                if (link.A == B)
                {
                    TheLink newLink = new TheLink();
                    newLink.B = link.B;
                    newLink.A = A;

                    theLinks.Add(newLink);
                }
                else if (link.B == B)
                {
                    TheLink newLink = new TheLink();
                    newLink.A = link.A;
                    newLink.B = A;

                    theLinks.Add(newLink);
                }
                theLinks.Remove(link);
            }
            else if (r == 3)
            {
                linksTower_2.Remove(linkedTowers[B]);
                linksTower_2.Add(linkedTowers[A]);

                TheLink link = FindTheLink(B);
                if (link.A == B)
                {
                    TheLink newLink = new TheLink();
                    newLink.B = link.B;
                    newLink.A = A;

                    theLinks.Add(newLink);
                }
                else if (link.B == B)
                {
                    TheLink newLink = new TheLink();
                    newLink.A = link.A;
                    newLink.B = A;

                    theLinks.Add(newLink);
                }
                theLinks.Remove(link);
            }

            linkedTowers.Remove(B);
        }
        else
        {
            return;
        }

        //刷新连接
        FlashLink();
    }


    public static void ResetAllData()
    {
        linkedTowers = new Dictionary<Tower, TowerLinkMap>();
        linksTower_1 = new List<TowerLinkMap>();
        linksTower_2 = new List<TowerLinkMap>();
        linksTower_3 = new List<TowerLinkMap>();
        theLinks = new List<TheLink>();
    }
}
