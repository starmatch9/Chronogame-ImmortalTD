using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClick : MonoBehaviour
{
    [Header("��������")]
    //��������������
    public GameObject outline;
    //ԭ�������ű���
    Vector3 originalScale;
    //��һ�����ű���
    public float hoverScale = 1.2f;

    [Header("����չ��")]
    public GameObject optionCanva;//��ʱ������������1.428571��Ϊ�˷��ö�ʧ��д������

    Vector3 originalCanvaScale;

    //�ж��û��Ƿ�չ��ѡ�����
    bool isSelecting = false;

    Coroutine OpenCloseOptionCavan = null;

    void Awake()
    {
        originalScale = transform.localScale;
        outline.SetActive(false);
        originalCanvaScale = optionCanva.transform.localScale;
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
        if (isSelecting)
        {
            return;
        }
        transform.localScale = originalScale;
        outline.SetActive(false);
    }

    //��갴��
    public void OnMouseDown()
    {
        if (isSelecting)
        {
            return;
        }

        //transform.localScale = originalScale * hoverScale * clickScale;
        transform.localScale = originalScale * hoverScale;
        outline.SetActive(true);

        isSelecting = true;

        //����չ������
        if (OpenCloseOptionCavan != null)
        {
            StopCoroutine(OpenCloseOptionCavan);
            OpenCloseOptionCavan = null;
        }
        OpenCloseOptionCavan = StartCoroutine(optionCancaOpen());

    }

    //���̧��
    public void OnMouseUp()
    {
        //if (isOnHole)
        //{
        //    transform.localScale = originalScale * hoverScale;
        //}
    }

    //
    //������ν������������ط�����״̬�ļ�¼��
    //������壬�ڵ�������������¼����������״̬��������Ҫ��˼·��������������������������������������
    //
    public void MouseReset()
    {
        //���ÿӴ�С����������
        transform.localScale = originalScale;
        outline.SetActive(false);
        //����ѡ�����
        isSelecting = false;

        //��
        if (OpenCloseOptionCavan != null)
        {
            StopCoroutine(OpenCloseOptionCavan);
            OpenCloseOptionCavan = null;
        }
        OpenCloseOptionCavan = StartCoroutine(optionCancaClose());
    }

    IEnumerator optionCancaOpen()
    {
        optionCanva.transform.localScale = Vector3.zero;
        optionCanva.SetActive(true);
        float timer = 0;
        //�������ʱ�����������ط�����©�ˣ�
        while (timer < 0.3f)
        {
            //���㵽ԭʼ���ű�������ֵ
            optionCanva.transform.localScale = Vector3.Lerp(Vector3.zero, originalCanvaScale, timer / 0.3f);
            timer += Time.deltaTime;
            yield return null;
        }
        optionCanva.transform.localScale = originalCanvaScale;
    }

    IEnumerator optionCancaClose()
    {
        float timer = 0;
        while (timer < 0.3f)
        {
            //��ԭʼ���ű������㣬��ֵ
            optionCanva.transform.localScale = Vector3.Lerp(originalCanvaScale, Vector3.zero, timer / 0.3f);
            timer += Time.deltaTime;
            yield return null;
        }
        optionCanva.SetActive(false);
        optionCanva.transform.localScale = originalCanvaScale;
    }
}
