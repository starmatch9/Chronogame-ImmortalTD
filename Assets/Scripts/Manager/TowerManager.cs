using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [Header("������Χͼ��Ԥ�Ƽ�")]
    [Header("Բ��")]
    public GameObject CircleArea;
    [Header("����")]
    public GameObject SquareArea;

    [Header("TowerԤ�Ƽ�ȫ�б�")]
    [Header("��")]
    public GameObject JinTowerPref;
    [Header("ľ")]
    public GameObject MuTowerPref;
    [Header("ˮ")]
    public GameObject ShuiTowerPref;
    [Header("��")]
    public GameObject HuoTowerPref;
    [Header("��")]
    public GameObject TuTowerPref;
    [Header("��Lv1")]
    public GameObject JinTowerPref_Lv1;
    [Header("ľLv1")]
    public GameObject MuTowerPref_Lv1;
    [Header("ˮLv1")]
    public GameObject ShuiTowerPref_Lv1;
    [Header("��Lv1")]
    public GameObject HuoTowerPref_Lv1;
    [Header("��Lv1")]
    public GameObject TuTowerPref_Lv1;
    [Header("��Lv2")]
    public GameObject JinTowerPref_Lv2;
    [Header("ľLv2")]
    public GameObject MuTowerPref_Lv2;
    [Header("ˮLv2")]
    public GameObject ShuiTowerPref_Lv2;
    [Header("��Lv2")]
    public GameObject HuoTowerPref_Lv2;
    [Header("��Lv2")]
    public GameObject TuTowerPref_Lv2;
    [Header("��")]
    public GameObject ZuanTowerPref;
    [Header("¯")]
    public GameObject LuTowerPref;
    [Header("��")]
    public GameObject JiTowerPref;
    [Header("��")]
    public GameObject ShuTowerPref;
    [Header("��")]
    public GameObject LangTowerPref;
    [Header("��")]
    public GameObject BingTowerPref;
    [Header("��")]
    public GameObject PaoTowerPref;
    [Header("��")]
    public GameObject RongTowerPref;
    [Header("ǽ")]
    public GameObject QiangTowerPref;
    [Header("��")]
    public GameObject ZhaoTowerPref;

    List<GameObject> towers = new List<GameObject>();

    //����ȫ�ֶ�Ҫ�õĶ�������ʼ����Խ��Խ��
    // Start is called before the first frame update
    void Awake()
    {
        //������Ԥ�Ƽ���ӵ��б�
        towers.Add(JinTowerPref);
        towers.Add(MuTowerPref);
        towers.Add (ShuiTowerPref);
        towers.Add(HuoTowerPref);
        towers.Add(TuTowerPref);
        towers.Add(JinTowerPref_Lv1);
        towers.Add(MuTowerPref_Lv1);
        towers.Add(ShuiTowerPref_Lv1);
        towers.Add(HuoTowerPref_Lv1);
        towers.Add(TuTowerPref_Lv1);
        towers.Add(JinTowerPref_Lv2);
        towers.Add(MuTowerPref_Lv2);
        towers.Add(ShuiTowerPref_Lv2);
        towers.Add(HuoTowerPref_Lv2);
        towers.Add(TuTowerPref_Lv2);
        towers.Add(ZuanTowerPref);
        towers.Add(LuTowerPref);
        towers.Add(JiTowerPref);
        towers.Add(ShuTowerPref);
        towers.Add(LangTowerPref);
        towers.Add(BingTowerPref);
        towers.Add(PaoTowerPref);
        towers.Add(RongTowerPref);
        towers.Add(QiangTowerPref);
        towers.Add(ZhaoTowerPref);

        //����ȫ�ֵ���
        GlobalData.globalTowerPrefabs = towers;

        GlobalTowerFunction.CircleArea = CircleArea;
        GlobalTowerFunction.SquareArea = SquareArea;

    }
}
