using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ZuanTower : Tower
{
    // * ��ͷ�� *

    [TextArea]
    public string Tips = "ע�⣺���з�Χָ�˶��켣�İ뾶����Ϊ���ָ��ͷ����תһ�ܵ�ʱ�䡣";

    GameObject drill;

    //��תƫ�ƽǶ�
    float rotationOffset = 180f;

    [Header("����˺�")]
    [Range(0, 100)] public float damage = 30f;

    Drill drillScript;

    private void Start()
    {
        drill = transform.Find("Drill").gameObject;
        drillScript = drill.GetComponent<Drill>();
        drillScript.SetAttack(damage);
        drill.SetActive(false);
    }

    public override void TowerAction()
    {
        if (!drill.activeInHierarchy)
        {
            drill.SetActive(true);
        }
        StartCoroutine(FlyDrill(actionTime));
    }

    //����תһ��
    IEnumerator FlyDrill(float circleTime)
    {
        float timer = 0f;
        float angle = 0f;

        while (timer < circleTime)
        {
            timer += Time.deltaTime;
            angle = -2 * Mathf.PI * (timer / circleTime); //���ű�ʾ˳ʱ��

            //����Բ���ϵĵ�
            float x = transform.position.x + attackRange * Mathf.Cos(angle);
            float y = transform.position.y + attackRange * Mathf.Sin(angle);

            //��������λ��
            drill.transform.position = new Vector3(x, y, drill.transform.position.z);

            //������ת�Ƕ�
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            float angleArrow = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            drill.transform.rotation = Quaternion.Euler(0, 0, angleArrow + rotationOffset);

            yield return null; // ÿ֡����
        }

        timer = 0f; // ���ü�ʱ��
    }


}
