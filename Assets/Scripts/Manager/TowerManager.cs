using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
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
    }
}
