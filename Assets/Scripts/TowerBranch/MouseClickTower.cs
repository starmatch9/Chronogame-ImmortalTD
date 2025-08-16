using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickTower : MonoBehaviour
{
    [Header("��������")]
    //ԭ�������ű���
    Vector3 originalScale;
    //��һ�����ű���
    public float hoverScale = 1.2f;

    [Header("����չ��")]
    public GameObject optionCanva;

    Vector3 originalCanvaScale;

    //�ж��û��Ƿ�չ��ѡ�����
    bool isSelecting = false;

    Coroutine OpenCloseOptionCavan = null;

    void Awake()
    {
        originalScale = transform.localScale;
        originalCanvaScale = optionCanva.transform.localScale;
    }

    // �����ͣ
    public void OnMouseEnter()
    {
        if (isSelecting)
        {
            return;
        }
        transform.localScale = originalScale * hoverScale;
    }

    // ����뿪
    public void OnMouseExit()
    {
        if (isSelecting)
        {
            return;
        }
        transform.localScale = originalScale;
    }

    //��갴��
    public void OnMouseDown()
    {
        if (isSelecting)
        {
            return;
        }
        //����ԭ���Ĵ�С
        transform.localScale = originalScale;

        isSelecting = true;
        //����չ������
        if (OpenCloseOptionCavan != null)
        {
            StopCoroutine(OpenCloseOptionCavan);
            OpenCloseOptionCavan = null;
        }
        OpenCloseOptionCavan = StartCoroutine(optionCancaOpen());
    }

    public void MouseReset()
    {
        //���ô�С
        transform.localScale = originalScale;
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
