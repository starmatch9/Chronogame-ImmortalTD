using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Drill : Bullet
{
    //就是在子弹死亡前可以穿透多个敌人
    int maxCount = 0;

    //穿透计数器
    int counter = 0;

    enum Dir
    {
        Up, Down, Left, Right
    }

    Dir currentDir = Dir.Up;

    Dictionary<Dir, Vector3> dir = new Dictionary<Dir, Vector3>();

    void Awake()
    {
        target = null; //清除目标
        dir.Add (Dir.Up, new Vector3 (0, 1, 0).normalized);
        dir.Add(Dir.Down, new Vector3 (0, -1, 0).normalized);
        dir.Add(Dir.Left, new Vector3(1, 0, 0).normalized);
        dir.Add(Dir.Right, new Vector3(-1, 0, 0).normalized);  
    }

    //0123就是上下左右
    public void SetDir(int i)
    {
        if (i == 0)
        {
            currentDir = Dir.Up;
        }
        else if (i == 1)
        {
            currentDir = Dir.Down;
            transform.Rotate(0, 0, 180);
        }
        else if (i == 2)
        {
            currentDir = Dir.Left;
            transform.Rotate(0, 0, 270);
        }else
        {
            currentDir = Dir.Right;
            transform.Rotate(0, 0, 90);
        }
    }

    public void SetPenetrateCount(int Penetrate)
    {
        maxCount = Penetrate;
    }

    public override IEnumerator DieAction()
    {
        if (counter < maxCount)
        {
            counter++;
            //立刻推出协程
            yield break;
        }
        else
        {
            yield return StartCoroutine(DestoryBullet());
        }
    }

    public override void flyToTarget()
    {
        //上下左右发射
        direction = dir[currentDir];
        transform.position += direction * moveSpeed * Time.deltaTime;

        //检测距离，过远销毁
        float currentDistance = Vector3.Distance(transform.position, initialPosition);
        if (currentDistance >= maxDistance)
        {
            Destroy(gameObject); //销毁
        }
    }

}
