using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Link : MonoBehaviour
{
    Vector3 originalPosition;

    bool following = false;

    Coroutine coldCoroutine = null;

    //是否可以点击，即鼠标在塔的上方
    bool canClick = false;

    //被操作的塔对象
    GameObject currentTower = null;

    [Header("UI冷却阴影")]
    public GameObject UI_Shade = null;

    [Header("冷却时间(秒)")]
    public float time = 1f;

    //这是准备连接的两座塔
    GameObject towerA = null;
    GameObject towerB = null;



    private void Update()
    {
        //右击取消
        if (following && Input.GetMouseButton(1))
        {
            StopFollow();
        }

        if (following)
        {
            FollowMousePosition();
        }

        if (following && canClick && Input.GetMouseButton(0))
        {
            Prune();

        }
        else if (following && !canClick && Input.GetMouseButton(0))
        {
            //请单击塔以进行连接

        }
    }

    public void Prune()
    {
        if(currentTower == null)
        {
            return;
        }

        if(towerA == null && towerB == null)
        {
            towerA = currentTower;
            currentTower = null;
        }else if(towerA != null && towerB == null)
        {
            //不可选同一个塔
            if(currentTower == towerA) {
                return;
            }
            towerB = currentTower;
            currentTower = null;

            LinkTower(towerA, towerB);

            StopFollow();
            cold();
        }


    }

    public void LinkTower(GameObject A, GameObject B)
    {
        Tower tA = A.GetComponent<Tower>();
        Tower tB = B.GetComponent<Tower>();

        GlobalData.ElementAttribute eA = tA.attribute;
        GlobalData.ElementAttribute eB = tB.attribute;

        //金生水、金克木
        if(eA == GlobalData.ElementAttribute.JIN && eB == GlobalData.ElementAttribute.SHUI)
        {
            Xiang_Sheng(tA, tB);
        }
        if (eB == GlobalData.ElementAttribute.JIN && eA == GlobalData.ElementAttribute.SHUI)
        {
            Xiang_Sheng(tB, tA);
        }
        if (eA == GlobalData.ElementAttribute.JIN && eB == GlobalData.ElementAttribute.MU)
        {
            Xiang_Ke(tA, tB);
        }
        if (eB == GlobalData.ElementAttribute.JIN && eA == GlobalData.ElementAttribute.MU)
        {
            Xiang_Ke(tB, tA);
        }
        //水生木、水克火
        if (eA == GlobalData.ElementAttribute.SHUI && eB == GlobalData.ElementAttribute.MU)
        {
            Xiang_Sheng(tA, tB);
        }
        if (eB == GlobalData.ElementAttribute.SHUI && eA == GlobalData.ElementAttribute.MU)
        {
            Xiang_Sheng(tB, tA);
        }
        if (eA == GlobalData.ElementAttribute.SHUI && eB == GlobalData.ElementAttribute.HUO)
        {
            Xiang_Ke(tA, tB);
        }
        if (eB == GlobalData.ElementAttribute.SHUI && eA == GlobalData.ElementAttribute.HUO)
        {
            Xiang_Ke(tB, tA);
        }
        //木生火、木克土
        if (eA == GlobalData.ElementAttribute.MU && eB == GlobalData.ElementAttribute.HUO)
        {
            Xiang_Sheng(tA, tB);
        }
        if (eB == GlobalData.ElementAttribute.MU && eA == GlobalData.ElementAttribute.HUO)
        {
            Xiang_Sheng(tB, tA);
        }
        if (eA == GlobalData.ElementAttribute.MU && eB == GlobalData.ElementAttribute.TU)
        {
            Xiang_Ke(tA, tB);
        }
        if (eB == GlobalData.ElementAttribute.MU && eA == GlobalData.ElementAttribute.TU)
        {
            Xiang_Ke(tB, tA);
        }
        //火生土、火克金
        if (eA == GlobalData.ElementAttribute.HUO && eB == GlobalData.ElementAttribute.TU)
        {
            Xiang_Sheng(tA, tB);
        }
        if (eB == GlobalData.ElementAttribute.HUO && eA == GlobalData.ElementAttribute.TU)
        {
            Xiang_Sheng(tB, tA);
        }
        if (eA == GlobalData.ElementAttribute.HUO && eB == GlobalData.ElementAttribute.JIN)
        {
            Xiang_Ke(tA, tB);
        }
        if (eB == GlobalData.ElementAttribute.HUO && eA == GlobalData.ElementAttribute.JIN)
        {
            Xiang_Ke(tB, tA);
        }
        //土生金、土克水
        if (eA == GlobalData.ElementAttribute.TU && eB == GlobalData.ElementAttribute.JIN)
        {
            Xiang_Sheng(tA, tB);
        }
        if (eB == GlobalData.ElementAttribute.TU && eA == GlobalData.ElementAttribute.JIN)
        {
            Xiang_Sheng(tB, tA);
        }
        if (eA == GlobalData.ElementAttribute.TU && eB == GlobalData.ElementAttribute.SHUI)
        {
            Xiang_Ke(tA, tB);
        }
        if (eB == GlobalData.ElementAttribute.TU && eA == GlobalData.ElementAttribute.SHUI)
        {
            Xiang_Ke(tB, tA);
        }
    }


    public void Xiang_Sheng(Tower A, Tower B)
    {
        //A生B



    }

    public void Xiang_Ke(Tower A, Tower B)
    {
        //A克B



    }


    //开始冷却函数
    public void cold()
    {
        coldCoroutine = StartCoroutine(fill());
    }

    IEnumerator fill()
    {
        UI_Shade.SetActive(true);
        Image image = UI_Shade.GetComponent<Image>();
        image.fillAmount = 1f;

        float timer = 0;
        while (timer < time)
        {
            image.fillAmount = Mathf.Lerp(1f, 0f, timer / time);

            timer += Time.deltaTime;
            yield return null;
        }

        UI_Shade.SetActive(false);
    }

    //开始跟随鼠标
    public void StartFollow()
    {
        following = true;
        GlobalData.towerClick = false;
    }

    //停止跟随鼠标
    public void StopFollow()
    {
        currentTower = null;
        towerA = null;
        towerB = null;

        following = false;
        GlobalData.towerClick = true;
        transform.position = originalPosition;
    }

    private void Start()
    {
        originalPosition = transform.position;
    }

    //跟随方法主题
    private void FollowMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        //设置z坐标为物体到相机的距离，否则看不见
        mousePosition.z = 4.5f;
        //转换为世界坐标
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = originalPosition.z;
        //更新物体位置
        transform.position = worldPosition;
    }

    //检测塔
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {

            SpriteRenderer renderer = collision.gameObject.GetComponent<SpriteRenderer>();
            //变成半透明
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.7f);

            canClick = true;

            currentTower = collision.transform.parent.gameObject;

            //Debug.Log("我要进来了！！");
        }
    }

    //离开塔
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            SpriteRenderer renderer = collision.gameObject.GetComponent<SpriteRenderer>();
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);

            canClick = false;

            currentTower = null;
        }
    }
}
