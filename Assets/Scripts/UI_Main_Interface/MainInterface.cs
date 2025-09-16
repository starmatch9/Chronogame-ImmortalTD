using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainInterface : MonoBehaviour
{
    //�ؿ��б�
    [Header("�ؿ��б�")]
    public List<LockCheck> levels = new List<LockCheck>();

    //�˳���Ϸ
    public void Exit_Game()
    {
        Application.Quit();
    }

    //�ؿ�ѡ��ť(ͨ����������Ѱ��)
    public void Select_Level(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void CheckLocks()
    {
        foreach (var lockCheck in levels)
        {
            lockCheck.check();

        }
    }


}
