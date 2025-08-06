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

    Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        //��������������
        Adjust();
    }

    void Update()
    {
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
            body.velocity = new Vector2(0, speed); //�����ƶ�
            direction = arrow.DOWN;
        }
        else if (IsPositionOnTile(downPosition) && direction != arrow.DOWN)
        {
            body.velocity = new Vector2(0, -speed); //�����ƶ�
            direction = arrow.UP;
        }
        else if (IsPositionOnTile(leftPosition) && direction != arrow.LEFT)
        {
            body.velocity = new Vector2(-speed, 0); //�����ƶ�
            direction = arrow.RIGHT;
        }
        else if (IsPositionOnTile(rightPosition) && direction != arrow.RIGHT)
        {
            body.velocity = new Vector2(speed, 0); //�����ƶ�
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
