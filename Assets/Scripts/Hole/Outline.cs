using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    //��ͣʱ�����ű���
    float scaleFactor = 1.2f;

    //������ɫ
    Color outlineColor = Color.red;

    void Awake()
    {
        CreateOutline();
    }

    //ͨ��ͬ�������ŵķ�ʽ��������
    void CreateOutline()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //��ȡ������
        GameObject parent = transform.parent.gameObject;
        //��ȡ�������SpriteRenderer���
        SpriteRenderer parentSpriteRenderer = parent.GetComponent<SpriteRenderer>();
        //�������ľ����븸����һ��
        spriteRenderer.sprite = parentSpriteRenderer.sprite;
        //��������λ���븸����һ��       (�����漰local��Եĸ���)
        transform.localPosition = Vector3.zero;
        //�������Ĵ�СΪ������ı�������
        transform.localScale = Vector3.one * scaleFactor;
        //������ͼ��һ��
        spriteRenderer.sortingLayerName = parentSpriteRenderer.sortingLayerName;
        //ȷ����������������������
        spriteRenderer.sortingOrder = parentSpriteRenderer.sortingOrder - 1;
        //����������ɫ
        spriteRenderer.color = outlineColor;
    }
}
