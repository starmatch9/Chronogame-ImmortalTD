using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClick : MonoBehaviour
{
    //��������������
    public GameObject outline;

    //ԭ�������ű���
    Vector3 originalScale;

    //���ű���
    public float hoverScale = 1.3f;

    void Awake()
    {
        originalScale = transform.localScale;
        outline.SetActive(false);
    }


    /*����������ⷽ����ǰ�᣺��������ײ���������������������� Physics2DRaycaster ���������*/
    /*������������ֻʹ����2D����ϵͳ�е�sprite���壡����*/

    /*����UIϵͳʹ�õĻ������
     * 
     *public void OnPointerEnter(PointerEventData eventData) {}
     *public void OnPointerExit(PointerEventData eventData) {}
     */


    // �����ͣ
    public void OnMouseEnter()
    {
        transform.localScale = originalScale * hoverScale;
        outline.SetActive(true);
    }

    // ����뿪
    public void OnMouseExit()
    {
        transform.localScale = originalScale;
        outline.SetActive(false);
    }
}
