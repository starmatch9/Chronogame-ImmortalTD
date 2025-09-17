using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickTower : MonoBehaviour
{
    [Header("当前塔")]
    public Tower tower = null;

    [Header("Selected物体")]
    public GameObject selected = null;

    [Header("关于展开")]
    public GameObject optionCanva = null;

    Vector3 originalCanvaScale;

    //判断用户是否展开选项界面
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

    // 鼠标悬停
    public void OnMouseEnter()
    {
        if (!GlobalData.towerClick)
        {
            return;
        }

        if (isSelecting)
        {
            return;
        }
        //transform.localScale = originalScale * hoverScale;
        selected.SetActive(true);


        //点击后执行的方法（如显示攻击范围）
        if (tower != null)
        {
            tower.DrawAttackArea();
        }
    }

    // 鼠标离开
    public void OnMouseExit()
    {
        
        

        if (isSelecting)
        {
            return;
        }
        //transform.localScale = originalScale;
        selected.SetActive(false);


        //点击后执行的方法重置（如显示攻击范围）
        if (tower != null)
        {
            tower.EraseAttackArea();
        }
    }

    //鼠标按下
    public void OnMouseDown()
    {
        if (!GlobalData.towerClick)
        {
            return;
        }

        if (optionCanva == null)
        {
            return;
        }

        if (isSelecting)
        {
            return;
        }
        isSelecting = true;

        //点击后执行的方法（如显示攻击范围）
        //tower.DrawAttackArea();

        //光翼展开！！
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

        //重置选项界面
        isSelecting = false;
        //重置选中状态
        selected.SetActive(false);

        //点击后执行的方法重置（如显示攻击范围）
        if (tower != null)
        {
            tower.EraseAttackArea();
        }

        //收
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
        //这里调节时常（有两个地方，别漏了）
        while (timer < 0.3f)
        {
            //从零到原始缩放比例，插值
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
            //从原始缩放比例到零，插值
            optionCanva.transform.localScale = Vector3.Lerp(originalCanvaScale, Vector3.zero, timer / 0.3f);
            timer += Time.deltaTime;
            yield return null;
        }
        optionCanva.SetActive(false);
        optionCanva.transform.localScale = originalCanvaScale;
    }
}
