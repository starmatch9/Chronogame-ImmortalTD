using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    //ÿ������һ��ǽ�����Կ����ڽű���ά����ס�ĵ����б�
    List<Enemy> blockedEnemies = new List<Enemy>();

    //����ʱ������ͣ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Move move = collision.GetComponent<Move>();
            Enemy enemy = collision.GetComponent<Enemy>();

            if (enemy != null) {
                //�޷��ƶ�
                move.StopMove();
                //��������ӵ��б���
                if (!blockedEnemies.Contains(enemy))
                {
                    blockedEnemies.Add(enemy);
                }
            }
        }
    }

    void FallDown(float damage)
    {
        //��ֹ�б���仯����һ��
        List<Enemy> enemiesCopy = new List<Enemy>(blockedEnemies);

        foreach (Enemy enemy in enemiesCopy)
        {
            //��������Ҫ�����ĵ���(һ�������������)
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            enemy.AcceptAttack(damage);
        }
    }

    public IEnumerator WallLife(float wallDuration, float wallDamage)
    {

        //�ȴ�ǽ�ĳ���ʱ��
        yield return new WaitForSeconds(wallDuration);

        //ǽ����Э�̣�����ʱ��������ģ�
        yield return StartCoroutine(FallDownRotate(0.3f));

        //ǽ����
        FallDown(wallDamage);

        //����ǰ���������ƶ�
        List<Enemy> enemiesCopy = new List<Enemy>(blockedEnemies);
        foreach (Enemy enemy in enemiesCopy)
        {
            Move move = enemy.gameObject.GetComponent<Move>();
            if (move != null)
            {
                move.ContinueMove(); //�ָ������ƶ�
            }
        }

        //����ǽ
        Destroy(gameObject);
    }

    IEnumerator FallDownRotate(float fallTime)
    {
        float timer = 0f;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 90f);

        while (timer < fallTime)
        {
            float t = timer / fallTime;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            timer += Time.deltaTime;
            yield return null; //�ȴ���һ֡
        }
    }
}
