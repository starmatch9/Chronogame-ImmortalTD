using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCheck : MonoBehaviour
{
    //检测是否开锁
    //后续场景的命名都采用Lv1这样
    public void check()
    {
        //获取对象名称，与场景名一一对应
        string name = gameObject.name;
        // 遍历所有子物体，找到标签是Lock的
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Lock"))
            {
                //解锁
                if (PlayerPrefs.GetInt(name, 0) == 1)
                {
                    child.gameObject.SetActive(false);
                }
                else
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
