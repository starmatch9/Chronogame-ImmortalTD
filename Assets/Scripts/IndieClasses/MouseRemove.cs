using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRemove : MonoBehaviour
{
    Vector3 originalPosition;

    bool following = false;



    //开始跟随鼠标
    public void StartFollow()
    {
        following = true;
    }

    //停止跟随鼠标
    public void StopFollow()
    {
        following = false;
        transform.position = originalPosition;
    }

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        //右击取消
        if(following && Input.GetMouseButton(1))
        {
            StopFollow();
        }

        if (following)
        {
            FollowMousePosition();
        }
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
        if (collision.gameObject.CompareTag("Tower")) { 
            
            SpriteRenderer renderer = collision.gameObject.GetComponent<SpriteRenderer>();
            //变成半透明
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.5f);

            Debug.Log("我要进来了！！");
        }
    }

    //离开塔
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            SpriteRenderer renderer = collision.gameObject.GetComponent<SpriteRenderer>();
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);


        }
    }
}
