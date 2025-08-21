using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchSelect : MonoBehaviour
{
    [Header("������֧")]
    public GameObject branch_1;
    public GameObject branch_2;

    //��TowerSelect����
    void PlaceTower(GameObject tower)
    {
        //���ۼ�
        int price = GlobalElementPowerFunction.towerSale[tower];
        if (!GlobalElementPowerFunction.CanMinus(price))
        {
            //�����Ԫ��������������������
            return;
        }
        GlobalElementPowerFunction.MinusCount(price);

        //��ʼ����Ϸ����
        GameObject newTower = Instantiate(tower, transform.parent.position, Quaternion.identity);
        Transform originalHole = transform.parent.gameObject.GetComponent<TowerInitial>().hole;
        // --------------------�������ǵøĳ�Tower���������Ļ���--------------------
        newTower.GetComponent<Tower>().SetHole(originalHole); //���ö�Ӧ�Ŀ�

        //�ҵ������壬Ȼ���Ƚ�����
        Transform child = transform.Find("TwoOptionsCanva");
        child.gameObject.SetActive(false);
        //Ȼ���ڽ��ñ���
        transform.parent.gameObject.SetActive(false);
        Destroy(transform.parent.gameObject); //����
    }

    //ѡ���֧1
    public void Branch1Select()
    {
        PlaceTower(branch_1);
    }

    //ѡ���֧2
    public void Branch2Select()
    {
        PlaceTower(branch_2);
    }
}
