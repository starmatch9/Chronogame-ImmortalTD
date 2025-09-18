using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheLight : MonoBehaviour
{
    //神秘光点
    public Tower A;
    public Tower B;

    //移动速度
    public float speed = 10f;

    //两点间距离
    float journeyLength;

    Vector3 pointA;
    Vector3 pointB;

    float startTime;

    bool canMove = false;

    public void StartMove()
    {
        pointA = A.transform.position;
        pointB = B.transform.position;  

        journeyLength = Vector3.Distance(pointA, pointB);
        startTime = Time.time;

        canMove = true;
    }

    public void StopMove()
    {
        canMove = false;
    }

    public void Die()
    {
        StopMove();
        Destroy(gameObject);
    }

    void Update()
    {
        if (!canMove)
        {
            return;
        }

        //PingPong使其0到1之间来回变化
        float distance = (Time.time - startTime) * speed;
        float fraction = Mathf.PingPong(distance, journeyLength) / journeyLength;

        // 在两个点之间插值
        transform.position = Vector3.Lerp(pointA, pointB, fraction);
    }


}
