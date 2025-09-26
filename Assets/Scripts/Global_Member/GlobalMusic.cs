using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalMusic
{
    //点击按钮音效
    public static AudioSource _Button = null;

    //更换页面音效
    public static AudioSource _Page = null;

    //弹窗音效
    public static AudioSource _Window = null;


    //技能选择音效
    public static AudioSource _Skill = null;

    //BGM播放器
    public static AudioSource _BGM = null;

    //[Header("声音预制件")]
    public static GameObject _Hit;
    public static GameObject _Boom;
    public static GameObject _Miss;
    public static GameObject _Money;
    public static GameObject _Wave;
    public static GameObject _Accept;
    public static GameObject _Remove;
    public static GameObject _Fail;

    public static void PlayOnce(GameObject g)
    {
        Object.Instantiate(g);
    }
}
