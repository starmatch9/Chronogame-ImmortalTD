using UnityEngine;

//���������ӿڣ�ʹ��װ����ģʽ��ʵ�ֲ�ͬ���˵���Ϊ,ʹ�ó������
//�鷽��������
//���󷽷�������д
public abstract class Enemy : MonoBehaviour
{
    HealthBar healthBar; //Ѫ��

    //Ѫ��ֵ
    public float health = 100;

    //�ƶ��ٶ�
    public float speed = 2f;

    private void Awake()
    {
        //��ȡѪ�����
        healthBar = GetComponentInChildren<HealthBar>();
    }

    //��ȡѪ��ֵ
    public float GetHealth() { 
        return health; 
    }

    //�۳�Ѫ��ֵ
    public void MinusHealth(int attack) {
        health -= attack;

        healthBar.SetHealth(health / 100f); //����Ѫ����ʾ

        if (health <= 0)
        {
            gameObject.SetActive(false); //��������
        }
    }

    //�жϵ����ݿ�������������д
    //�Ƿ�����Ҫ����
    public virtual bool NoMoreShotsNeeded() {
        return health <= 0; //���Ѫ��С�ڵ���0��������Ҫ����
    }    

    //��ȡ������Ϸ����
    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
