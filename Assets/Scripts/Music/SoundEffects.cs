using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [Header("音源")]
    public AudioSource button;
    public AudioSource page;
    public AudioSource bgm;
    public AudioSource window;
    public AudioSource skill;

    [Header("声音预制件")]
    public GameObject hit;
    public GameObject boom;
    public GameObject miss;
    public GameObject money;
    public GameObject wave;
    public GameObject accept;
    public GameObject remove;

    void Start()
    {
        GlobalMusic._Button = button;
        GlobalMusic._Page = page;
        GlobalMusic._BGM = bgm;
        GlobalMusic._Window = window;
        GlobalMusic._Skill = skill;
        //
        GlobalMusic._Hit = hit;
        GlobalMusic._Boom = boom;
        GlobalMusic._Miss = miss;
        GlobalMusic._Money = money;
        GlobalMusic._Wave = wave;
        GlobalMusic._Accept = accept;
        GlobalMusic._Remove = remove;
    }

    public void playBGM()
    {
        GlobalMusic._BGM.Play();
    }
    public void playButton()
    {
        GlobalMusic._Button.Play();

    }
    public void playPage()
    {
        GlobalMusic._Page.Play();
    }
    public void playWindow()
    {
        GlobalMusic._Window.Play();
    }
    public void playSkill()
    {
        GlobalMusic._Skill.Play();
    }

}
