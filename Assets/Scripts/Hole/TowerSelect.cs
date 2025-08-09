using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelect : MonoBehaviour
{
    [Header("������������-ľ-ˮ-��-��")]
    public GameObject towerJin;
    public GameObject towerMu;
    public GameObject towerShui;
    public GameObject towerHuo;
    public GameObject towerTu;

    private void Awake()
    {
        //��������λ��
        Adjust();
    }

    //һ�����÷������ڵ�ǰλ�÷�����
    void PlaceTower(GameObject tower)
    {
        //��ʼ����Ϸ����
        Instantiate(tower, transform.position, Quaternion.identity);

        //�ҵ������壬Ȼ���Ƚ�����
        Transform child = transform.Find("FiveOptionsCanva");
        child.gameObject.SetActive(false);
        //Ȼ���ڽ��ÿ�
        gameObject.SetActive(false);
    }

    //���ѡ�����
    public void JinSelect()
    {
        PlaceTower(towerJin);
    }

    //���ѡ��ľ��
    public void MuSelect()
    {
        PlaceTower(towerMu);
    }

    //���ѡ��ˮ��
    public void ShuiSelect()
    {
        PlaceTower(towerShui);
    }
    
    //���ѡ�����
    public void HuoSelect()
    {
        PlaceTower(towerHuo);
    }

    //���ѡ������
    public void TuSelect()
    {
        PlaceTower(towerTu);
    }


    public void Adjust()
    {
        //���е�Ĺ�ʽ
        float widthStart = -7.5f;
        float widthEnd = 7.5f;
        float heightStart = -3.5f;
        float heightEnd = 4.5f;
        //���������λ��
        for (float x = widthStart; x <= widthEnd; x += 1.0f)
        {
            for (float y = heightStart; y <= heightEnd; y += 1.0f)
            {
                if (Mathf.Abs(transform.position.x - x) <= 0.5f && Mathf.Abs(transform.position.y - y) <= 0.5f)
                {
                    transform.position = new Vector3(x, y, 0f);
                    break;
                }
            }
        }
    }
}
