using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLevelUp : MonoBehaviour
{
    //���������Ԥ�Ƽ�
    [Header("һ����֧")]
    public GameObject branch;

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
        Transform originalHole = transform.parent.gameObject.GetComponent<Tower>().hole;
        // --------------------�������ǵøĳ�Tower���������Ļ���--------------------
        newTower.GetComponent<Tower>().SetHole(originalHole); //���ö�Ӧ�Ŀ�

        GlobalData.towers.Add(newTower.GetComponent<Tower>());

        //�ҵ������壬Ȼ���Ƚ�����
        Transform child = transform.Find("OptionsCanva");
        child.gameObject.SetActive(false);
        //����ǰ���û���
        MouseClickTower mouseClickTower = GetComponent<MouseClickTower>();
        if (mouseClickTower.tower != null)
        {
            mouseClickTower.tower.EraseAttackArea();
        }
        //Ȼ���ڽ��ñ���
        if (GlobalData.towersInitial.Contains(transform.parent.gameObject.GetComponent<Tower>()))
        {
            GlobalData.towersInitial.Remove(transform.parent.gameObject.GetComponent<Tower>());
        }
        transform.parent.gameObject.SetActive(false);

        Destroy(transform.parent.gameObject); //����
    }

    //ѡ���֧
    public void BranchSelect()
    {
        PlaceTower(branch);
    }
}
