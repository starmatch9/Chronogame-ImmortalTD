using UnityEngine;

//���������ӿڣ�ʹ��װ����ģʽ��ʵ�ֲ�ͬ���˵���Ϊ(����Ч����),ʹ�ó������
//�鷽��������
//���󷽷�������д

/*ÿ���������ʵ��OnTriggerEnter����*/

public abstract class Enemy : MonoBehaviour
{
    EnemySpawn enemySpawn; //����������

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

    /*ע�⣺����������ӵ�������*/
    //�����������ӵ���ײ������MinusHealth����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���öԺ���������׼���ĸ�����ֻ�ܴ��ĸ����ˣ������������������˵���ײ��
        if (collision.CompareTag("Bullet") && collision.GetComponent<Action>().target == gameObject)
        {
            //��ȡ�ӵ������ݽű�
            //����
            MinusHealth(GetAttack());
            Destroy(collision.gameObject); //�����ӵ�
        }
    }

    //ע�⣺��ȡ�ӵ��Ĺ��������������ʵ�֣���������������봫���ӵ��ű���Ϊ����������Ȼ����û��д
    //�����У��������߼��������ŵ��˿��ԡ��ӵ����͵Ȳ�ͬ����ͬ����������
    public abstract float GetAttack();

    //��Ϸ��������ʱ��Ҫ�����Ĺ���
    public void GameObjectSpawn()
    {
        isDead = false;
    }

    //���õ���״̬���ڶ�������õĵ���
    public void GameObjectReset()
    {
        GetComponent<Move>().survivalTime = 0f; //���ô��ʱ��
        health = maxHealth; //����Ѫ��
        healthBar.SetHealth(health / maxHealth); //����Ѫ����ʾ
        //����״̬ʱ�����ж������
        enemySpawn.ReturnEnemy(gameObject);
    }

    //��ȡѪ��ֵ
    public float GetHealth() { 
        return health; 
    }

    //�۳�Ѫ��ֵ
    public void MinusHealth(float attack) {
        health -= attack;

        healthBar.SetHealth(health / maxHealth); //����Ѫ����ʾ

        if (health <= 0)
        {
            isDead = true;
            gameObject.SetActive(false); //��������

            GameObjectReset(); //���õ���״̬
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

    //���õ���������
    public void SetEnemySpawn(EnemySpawn spawn)
    {
        enemySpawn = spawn;
    }
}
