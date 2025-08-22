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
        if (collision.CompareTag("Bullet") && collision.GetComponent<Bullet>().target == gameObject)
        {
            //��ȡ�ӵ������ݽű�
            Bullet bullet = collision.GetComponent<Bullet>();

            //����
            AcceptAttack(GetAttack(bullet));

            //�����ӵ�ǰ���á�����
            //������ǰ���߼��Լ����ٱ���
            collision.GetComponent<Bullet>().Die();

        }
    }

    //ע�⣺��ȡ�ӵ��Ĺ��������������ʵ�֣���������������봫���ӵ��ű���Ϊ����������Ȼ����û��д
    //
    //���ֻ����ʵ�ֲ�ͬ���˶Բ�ͬ�ӵ��Ŀ��ԣ���ע�⣬ֻ���ӵ�����
    //
    //�����У��������߼��������ŵ��˿��ԡ��ӵ����͵Ȳ�ͬ����ͬ����������
    public abstract float GetAttack(Bullet bullet);

    //
    //��Ҫ�����ܹ�����������������Ҫ������д����������MinusHealth���������Ⱪ¶������
    //
    //�ô˷����������ӵ���ͳһ������������������������࣬��ɾ����������˺�
    //
    public virtual void AcceptAttack(float attack)
    {
        MinusHealth(attack);
    }

    //��Ϸ��������ʱ��Ҫ�����Ĺ���
    public void GameObjectSpawn()
    {
        isDead = false;
    }

    //���õ���״̬���ڶ�������õĵ���
    public void GameObjectReset()
    {
        GetComponent<Move>().survivalTime = 0f; //���ô��ʱ��
        GetComponent<Move>().ResetSpeed(); //�����ٶ�����Ϊ1
        health = maxHealth; //����Ѫ��
        healthBar.SetHealth(health / maxHealth); //����Ѫ����ʾ

        //���ö���״̬
        if (Freeze.enemyHitCount.ContainsKey(this))    //��ס��ContainsKey���ж��ֵ����Ƿ����ĳ����
        {
            Freeze.enemyHitCount.Remove(this); //�Ƴ����˶���״̬
        }

        //����״̬ʱ�����ж������
        enemySpawn.ReturnEnemy(gameObject);
    }

    //��ȡѪ��ֵ
    public float GetHealth() { 
        return health; 
    }

    //�۳�Ѫ��ֵ
    void MinusHealth(float attack) {
        health -= attack;

        healthBar.SetHealth(health / maxHealth); //����Ѫ����ʾ

        if (health <= 0)
        {
            isDead = true;
            gameObject.SetActive(false); //��������

            //��ӡ��������������ǰ��Ϊ�����˵����յ�ʱҲҪʹ�ã�
            SealFunction();

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

    //��������򵽴��յ�ʱ����һ�εĺ���(seal����˼�Ƿ�ӡ���ο��Ų���ǰ�ͷ��������ӡ��)
    public void SealFunction()
    {
        //�����϶���ȫ�ַ�����
        GlobalEnemyGroupFunction.CheckEnd();
        
    }
}
