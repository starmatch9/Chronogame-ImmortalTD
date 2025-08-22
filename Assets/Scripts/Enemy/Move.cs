using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Move : MonoBehaviour
{
    //�ƶ�����ö��
    enum arrow
    {
        UP, DOWN, LEFT, RIGHT, NONE
    }

    [Range(0, 20)]public float speed = 2f; //�ƶ��ٶ�

    //��¼��ǰ���ķ���ע�⣡�����������ķ��򣡣�������ɫת��ʱ�����������ķ����ƶ�
    arrow direction = arrow.NONE;

    bool isMoving = false; //�Ƿ������ƶ�

    public Tilemap roadTilemap;

    public float survivalTime = 0f;

    bool isStopMove = false;

    //��¼��ǰ�ƶ��ٶ�
    Vector2 currentVelocity;

    /* ����������һ�� */

    //���ٻ�����ƶ�(����Ϊ�ٷֱ�)
    public void ChangeSpeed(float factor)
    {
        isStopMove = true;
        SetSpeedFactor(factor);
        body.velocity = body.velocity * speedFactor;
        isStopMove = false;
    }
    //�����ٶȣ������ٶ����ӵ�Ӱ�죩
    public void ResetSpeed()
    {
        isStopMove = true;
        if(GetSpeed() != speed)
        {
            body.velocity = body.velocity / speedFactor; //�����ٶ����ӵ�Ӱ��
        }
        SetSpeedFactor(1f); //�����ٶ�����Ϊ1
        isStopMove = false;
    }

    float speedFactor = 1f; //�ٶ����ӣ�Ĭ��Ϊ1

    void SetSpeedFactor(float factor)
    {
        speedFactor = factor;
    }

    //���ڻ�ȡ�ٶȣ�������и���
    public float GetSpeed()
    {
        return speed * speedFactor;
    }


    //��ͣ�ƶ�
    public void StopMove()
    {
        currentVelocity = body.velocity;
        body.velocity = Vector2.zero; //ֹͣ�ƶ�

        isStopMove = true;
    }
    //�����ƶ�
    public void ContinueMove()
    {
        body.velocity = currentVelocity;
        currentVelocity = Vector2.zero; //��յ�ǰ�ٶ�
        isStopMove = false;
    }

    Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        //��������������
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
        isMoving = false; //�����ƶ�״̬
        direction = arrow.NONE; //���÷���
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
            //ʱ���������Ҫ�����ٶ�����
            survivalTime += Time.deltaTime * speedFactor; //���Ӵ��ʱ��

            //������ʻ�ķ�������·û��
            if (direction == arrow.UP)
            {
                Vector3 explorePosition = transform.position + new Vector3(0, -0.5f, 0); //�·�λ��
                if (!IsPositionOnTile(explorePosition))
                {
                    isMoving = false;
                }
            }
            else if (direction == arrow.DOWN)
            {
                Vector3 explorePosition = transform.position + new Vector3(0, 0.5f, 0); //�Ϸ�λ��
                if (!IsPositionOnTile(explorePosition))
                {
                    isMoving = false;
                }
            }
            else if (direction == arrow.LEFT)
            {
                Vector3 explorePosition = transform.position + new Vector3(0.5f, 0, 0); //�ҷ�λ��
                if (!IsPositionOnTile(explorePosition))
                {
                    isMoving = false;
                }
            }
            else if (direction == arrow.RIGHT)
            {
                Vector3 explorePosition = transform.position + new Vector3(-0.5f, 0, 0); //��λ��
                if (!IsPositionOnTile(explorePosition))
                {
                    isMoving = false;
                }
            }
        }
    }

    //�ƶ���ɫ�߼�
    public void MoveObject()
    {
        //��ȡ�ĸ����λ�ã�����������
        //����˼·��ǰ̽1���ж��Ƿ���·
        Vector3 currentPosition = transform.position;//��ǰ����λ��
        Vector3 upPosition = currentPosition + new Vector3(0, 1, 0); //�Ϸ�λ��
        Vector3 downPosition = currentPosition + new Vector3(0, -1, 0); //�·�λ��
        Vector3 leftPosition = currentPosition + new Vector3(-1, 0, 0); //��λ��
        Vector3 rightPosition = currentPosition + new Vector3(1, 0, 0); //�ҷ�λ��
        if (IsPositionOnTile(upPosition) && direction != arrow.UP)
        {
            body.velocity = new Vector2(0, GetSpeed()); //�����ƶ�
            direction = arrow.DOWN;
        }
        else if (IsPositionOnTile(downPosition) && direction != arrow.DOWN)
        {
            body.velocity = new Vector2(0, -GetSpeed()); //�����ƶ�
            direction = arrow.UP;
        }
        else if (IsPositionOnTile(leftPosition) && direction != arrow.LEFT)
        {
            body.velocity = new Vector2(-GetSpeed(), 0); //�����ƶ�
            direction = arrow.RIGHT;
        }
        else if (IsPositionOnTile(rightPosition) && direction != arrow.RIGHT)
        {
            body.velocity = new Vector2(GetSpeed(), 0); //�����ƶ�
            direction = arrow.LEFT;
        }
        else
        {
            //����ĸ�����û��·�����ƶ�
            body.velocity = Vector2.zero;
        }
    }

    //�ж��Ƿ�����ƶ���ָ��λ��
    bool IsPositionOnTile(Vector3 worldPosition)
    {
        //����������ת��Ϊ��������
        Vector3Int cellPosition = roadTilemap.WorldToCell(worldPosition);
        //��������λ���Ƿ�����Ƭ
        return roadTilemap.HasTile(cellPosition);
    }

    /*��ʼ���÷�����*/
    //����
    public void Adjust()
    {
        //�����ĵ�Ҫ����������һȦ����

        //���е�Ĺ�ʽ
        float widthStart = -8.5f;
        float widthEnd = 8.5f;
        float heightStart = -4.5f;
        float heightEnd = 5.5f;
        //���������λ��
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
