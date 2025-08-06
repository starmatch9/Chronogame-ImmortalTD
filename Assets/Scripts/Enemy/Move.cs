using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Move : MonoBehaviour
{
    //移动方向枚举
    enum arrow
    {
        UP, DOWN, LEFT, RIGHT, NONE
    }

    [Range(0, 20)]public float speed = 2f; //移动速度

    //记录当前来的方向，注意！！！！是来的方向！！！当角色转向时不可以向来的方向移动
    arrow direction = arrow.NONE;

    bool isMoving = false; //是否正在移动

    public Tilemap roadTilemap;

    Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        //调整物体至中央
        Adjust();
    }

    void Update()
    {
        RunAlongRoad();
    }

    void OnEnable()
    {
        Adjust();
        isMoving = false; //重置移动状态
        direction = arrow.NONE; //重置方向
    }
    public void RunAlongRoad()
    {
        if (!isMoving)
        {
            MoveObject();
            isMoving = true;
        }
        else
        {
            //按照行驶的方向检测有路没有
            if (direction == arrow.UP)
            {
                Vector3 explorePosition = transform.position + new Vector3(0, -0.5f, 0); //下方位置
                if (!IsPositionOnTile(explorePosition))
                {
                    isMoving = false;
                }
            }
            else if (direction == arrow.DOWN)
            {
                Vector3 explorePosition = transform.position + new Vector3(0, 0.5f, 0); //上方位置
                if (!IsPositionOnTile(explorePosition))
                {
                    isMoving = false;
                }
            }
            else if (direction == arrow.LEFT)
            {
                Vector3 explorePosition = transform.position + new Vector3(0.5f, 0, 0); //右方位置
                if (!IsPositionOnTile(explorePosition))
                {
                    isMoving = false;
                }
            }
            else if (direction == arrow.RIGHT)
            {
                Vector3 explorePosition = transform.position + new Vector3(-0.5f, 0, 0); //左方位置
                if (!IsPositionOnTile(explorePosition))
                {
                    isMoving = false;
                }
            }
        }
    }

    //移动角色逻辑
    public void MoveObject()
    {
        //获取四个面的位置，即上下左右
        //核心思路：前探1格，判断是否有路
        Vector3 currentPosition = transform.position;//当前物体位置
        Vector3 upPosition = currentPosition + new Vector3(0, 1, 0); //上方位置
        Vector3 downPosition = currentPosition + new Vector3(0, -1, 0); //下方位置
        Vector3 leftPosition = currentPosition + new Vector3(-1, 0, 0); //左方位置
        Vector3 rightPosition = currentPosition + new Vector3(1, 0, 0); //右方位置
        if (IsPositionOnTile(upPosition) && direction != arrow.UP)
        {
            body.velocity = new Vector2(0, speed); //向上移动
            direction = arrow.DOWN;
        }
        else if (IsPositionOnTile(downPosition) && direction != arrow.DOWN)
        {
            body.velocity = new Vector2(0, -speed); //向下移动
            direction = arrow.UP;
        }
        else if (IsPositionOnTile(leftPosition) && direction != arrow.LEFT)
        {
            body.velocity = new Vector2(-speed, 0); //向左移动
            direction = arrow.RIGHT;
        }
        else if (IsPositionOnTile(rightPosition) && direction != arrow.RIGHT)
        {
            body.velocity = new Vector2(speed, 0); //向右移动
            direction = arrow.LEFT;
        }
        else
        {
            //如果四个方向都没有路，则不移动
            body.velocity = Vector2.zero;
        }
    }

    //判断是否可以移动到指定位置
    bool IsPositionOnTile(Vector3 worldPosition)
    {
        //将世界坐标转换为网格坐标
        Vector3Int cellPosition = roadTilemap.WorldToCell(worldPosition);
        //检查该网格位置是否有瓦片
        return roadTilemap.HasTile(cellPosition);
    }

    /*初始化用方法区*/
    //调整
    public void Adjust()
    {
        //调整的点要包含外面那一圈！！

        //所有点的公式
        float widthStart = -8.5f;
        float widthEnd = 8.5f;
        float heightStart = -4.5f;
        float heightEnd = 5.5f;
        //调整物体的位置
        for (float x = widthStart; x <= widthEnd; x += 1.0f)
        {
            for (float y = heightStart; y <= heightEnd; y += 1.0f)
            {
                if (Mathf.Abs(transform.position.x - x) <= 0.5f && Mathf.Abs(transform.position.y - y) <= 0.5f)
                {
                    transform.position = new Vector3(x, y, 0f);
                    break;
                }
            }
        }
    }
}
