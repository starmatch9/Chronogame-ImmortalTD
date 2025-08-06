using UnityEngine;

//���������ӿڣ�ʹ��װ����ģʽ��ʵ�ֲ�ͬ���˵���Ϊ(����Ч����),ʹ�ó������
//�鷽��������
//���󷽷�������д
public abstract class Enemy : MonoBehaviour
{
    HealthBar healthBar; //Ѫ��

    float maxHealth = 100f; //���Ѫ��

    bool isDead = false; //�Ƿ�����

    //Ѫ��ֵ
    public float health = 100;

    //�ƶ��ٶ�
    public float speed = 2f;

    private void Awake()
    {
        //��ȡѪ�����
        healthBar = GetComponentInChildren<HealthBar>();
    }

    private void OnEnable()
    {
        //������ʱ����Ѫ��
        health = maxHealth; //����Ѫ��
        isDead = false; //��������״̬
        healthBar.SetHealth(health / maxHealth); //����Ѫ����ʾ
    }

    private void OnDisable()
    {
        //�ڽ���ʱ����Ѫ��
        isDead = true; //��������״̬
    }

    //��ȡѪ��ֵ
    public float GetHealth() { 
        return health; 
    }

    //�۳�Ѫ��ֵ
    public void MinusHealth(int attack) {
        health -= attack;

        healthBar.SetHealth(health / maxHealth); //����Ѫ����ʾ

        if (health <= 0)
        {
            gameObject.SetActive(false); //��������
        }
    }

    //�жϵ����ݿ�������������д
    //�Ƿ�����Ҫ����
    public virtual bool NoMoreShotsNeeded() {
        return isDead; //���Ѫ��С�ڵ���0��������Ҫ����
    }    

    //��ȡ������Ϸ���󣨡��������е���һ���ˣ�
    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
