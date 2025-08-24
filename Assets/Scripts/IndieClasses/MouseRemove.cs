using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRemove : MonoBehaviour
{
    Vector3 originalPosition;

    bool following = false;



    //��ʼ�������
    public void StartFollow()
    {
        following = true;
    }

    //ֹͣ�������
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
        //�һ�ȡ��
        if(following && Input.GetMouseButton(1))
        {
            StopFollow();
        }

        if (following)
        {
            FollowMousePosition();
        }
    }

    //���淽������
    private void FollowMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        //����z����Ϊ���嵽����ľ��룬���򿴲���
        mousePosition.z = 4.5f;
        //ת��Ϊ��������
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = originalPosition.z;
        //��������λ��
        transform.position = worldPosition;
    }

    //�����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tower")) { 
            
            SpriteRenderer renderer = collision.gameObject.GetComponent<SpriteRenderer>();
            //��ɰ�͸��
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.5f);

            Debug.Log("��Ҫ�����ˣ���");
        }
    }

    //�뿪��
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            SpriteRenderer renderer = collision.gameObject.GetComponent<SpriteRenderer>();
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);


        }
    }
}
