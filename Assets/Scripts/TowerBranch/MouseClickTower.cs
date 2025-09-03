using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickTower : MonoBehaviour
{
    [Header("��ǰ��")]
    public Tower tower = null;

    [Header("Selected����")]
    public GameObject selected = null;

    [Header("����չ��")]
    public GameObject optionCanva = null;

    Vector3 originalCanvaScale;

    //�ж��û��Ƿ�չ��ѡ�����
    bool isSelecting = false;

    Coroutine OpenCloseOptionCavan = null;

    void Awake()
    {
        if(optionCanva == null)
        {
            return;
        }
        originalCanvaScale = optionCanva.transform.localScale;
    }

    // �����ͣ
    public void OnMouseEnter()
    {
        if (isSelecting)
        {
            return;
        }
        //transform.localScale = originalScale * hoverScale;
        selected.SetActive(true);


        //�����ִ�еķ���������ʾ������Χ��
        if (tower != null)
        {
            tower.DrawAttackArea();
        }
    }

    // ����뿪
    public void OnMouseExit()
    {
        if (isSelecting)
        {
            return;
        }
        //transform.localScale = originalScale;
        selected.SetActive(false);


        //�����ִ�еķ������ã�����ʾ������Χ��
        if (tower != null)
        {
            tower.EraseAttackArea();
        }
    }

    //��갴��
    public void OnMouseDown()
    {
        if (optionCanva == null)
        {
            return;
        }

        if (isSelecting)
        {
            return;
        }
        isSelecting = true;

        //�����ִ�еķ���������ʾ������Χ��
        //tower.DrawAttackArea();

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
        if (optionCanva == null)
        {
            return;
        }

        //����ѡ�����
        isSelecting = false;
        //����ѡ��״̬
        selected.SetActive(false);

        //�����ִ�еķ������ã�����ʾ������Χ��
        if (tower != null)
        {
            tower.EraseAttackArea();
        }

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
