﻿using UnityEngine;
using UnityEngine.Tilemaps;

public class Move : MonoBehaviour
{
    //移动方向枚举
    public enum arrow
    {
        UP, DOWN, LEFT, RIGHT, NONE
    }

    [Header("移动速度")]
    [Range(0, 20)]public float speed = 2f; //移动速度

    //记录当前来的方向，注意！！！！是来的方向！！！当角色转向时不可以向来的方向移动
    //[HideInInspector]
    public arrow direction = arrow.NONE;

    bool isMoving = false; //是否正在移动

    [HideInInspector]
    public Tilemap roadTilemap;

    //虽然变量名说是时间，实际上是时间乘以速度，即路程，改名很麻烦（
    [HideInInspector]
    public float survivalTime = 0f;

    [HideInInspector]
    public bool isStopMove = false;

    //记录当前移动速度
    Vector2 currentVelocity;

    public void SetBoolMoving(bool v)
    {
        isMoving = v;
    }

    /* 以下两个是一组 */

    //减速或加速移动(参数为百分比)
    public void ChangeSpeed(float factor)
    {
        if (isStopMove)
        {
            return;
        }

        SetSpeedFactor(factor);
        Re_Move();
    }
    //重置速度（撤销速度因子的影响）
    public void ResetSpeed()
    {
        SetSpeedFactor(1f); //重置速度因子为1
        Re_Move();
    }

    public void Re_Move()
    {
        //按照行驶的方向重新移动
        if (direction == arrow.UP)
        {
            body.velocity = new Vector2(0, -GetSpeed()); //向下移动
        }
        else if (direction == arrow.DOWN)
        {
            body.velocity = new Vector2(0, GetSpeed()); //向上移动
        }
        else if (direction == arrow.LEFT)
        {
            body.velocity = new Vector2(GetSpeed(), 0); //向右移动
        }
        else if (direction == arrow.RIGHT)
        {
            body.velocity = new Vector2(-GetSpeed(), 0); //向左移动
        }
    }

    float speedFactor = 1f; //速度因子，默认为1

    public void SetSpeedFactor(float factor)
    {
        speedFactor = factor;
    }

    //用于获取速度，方便进行更改
    public float GetSpeed()
    {
        return speed * speedFactor;
    }


    //暂停移动
    public void StopMove()
    {
        currentVelocity = body.velocity;
        body.velocity = Vector2.zero; //停止移动

        isStopMove = true;
    }
    //继续移动
    public void ContinueMove()
    {
        body.velocity = currentVelocity;
        currentVelocity = Vector2.zero; //清空当前速度

        //继续移动时,重新计算移动方向
        SetSpeedFactor(1f);
        Re_Move();

        isStopMove = false;
    }

    Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        //调整物体至中央
        Adjust();
    }

    void Update()
    {
        if (isStopMove)
        {
            return;
        }

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
            //时间的增加需要考虑速度因子
            survivalTime += Time.deltaTime * speed * speedFactor; //增加存活时间

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
            body.velocity = new Vector2(0, GetSpeed()); //向上移动
            direction = arrow.DOWN;
        }
        else if (IsPositionOnTile(downPosition) && direction != arrow.DOWN)
        {
            body.velocity = new Vector2(0, -GetSpeed()); //向下移动
            direction = arrow.UP;
        }
        else if (IsPositionOnTile(leftPosition) && direction != arrow.LEFT)
        {
            body.velocity = new Vector2(-GetSpeed(), 0); //向左移动
            direction = arrow.RIGHT;
        }
        else if (IsPositionOnTile(rightPosition) && direction != arrow.RIGHT)
        {
            body.velocity = new Vector2(GetSpeed(), 0); //向右移动
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


    //没有退路了
    public bool noBackRoad()
    {
        //从上面来的就检测上方位置，以此类推
        if (direction == arrow.UP)
        {
            Vector3 explorePosition = transform.position + new Vector3(0, 0.5f, 0);
            if (!IsPositionOnTile(explorePosition))
            {
                return true;
            }
        }
        else if (direction == arrow.DOWN)
        {
            Vector3 explorePosition = transform.position + new Vector3(0, -0.5f, 0); 
            if (!IsPositionOnTile(explorePosition))
            {
                return true;
            }
        }
        else if (direction == arrow.LEFT)
        {
            Vector3 explorePosition = transform.position + new Vector3(-0.5f, 0, 0);
            if (!IsPositionOnTile(explorePosition))
            {
                return true;
            }
        }
        else if (direction == arrow.RIGHT)
        {
            Vector3 explorePosition = transform.position + new Vector3(0.5f, 0, 0);
            if (!IsPositionOnTile(explorePosition))
            {
                return true;
            }
        }


        return false;
    }
}
