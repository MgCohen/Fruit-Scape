using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Question : MonoBehaviour
{

    
    public bool trueOrFalse;

    public TextMeshProUGUI mainText;

    public GameEvent stopEvent;

    public AudioClip wrongSound;
    public AudioClip correctSound;

    public GameObject correct;
    public GameObject wrong;

    public int answers = 0;
    public int correctAnswers = 0;

    public void GotWrong()
    {
        SoundSystem.Play(wrongSound, 0.5f);
        wrong.SetActive(true);
        answers += 1;
        transform.DOShakePosition(0.2f, 10f);
        if (answers <= 5)
        {
            ActionDelayer.DelayAction(() => { stopEvent.Close(); transform.parent.parent.gameObject.SetActive(false); wrong.SetActive(false); Manager.instance.player.EnableInput = true; }, 1f);
        }
        else
        {
            ActionDelayer.DelayAction(() => { stopEvent.Close(); transform.parent.parent.gameObject.SetActive(false); correct.SetActive(false); Manager.instance.End(true); }, 1f);
        }
    }

    public void GotRight()
    {
        SoundSystem.Play(correctSound, 0.5f);
        correct.SetActive(true);
        transform.DOShakeScale(0.2f);
        answers += 1;
        correctAnswers += 1;
        Manager.instance.Points += 10;
        if (answers < 4)
        {
            ActionDelayer.DelayAction(() => { stopEvent.Close(); transform.parent.parent.gameObject.SetActive(false); correct.SetActive(false); Manager.instance.player.EnableInput = true; }, 1f);
        }
        else
        {
            ActionDelayer.DelayAction(() => { stopEvent.Close(); transform.parent.parent.gameObject.SetActive(false); correct.SetActive(false); Manager.instance.End(true); }, 1f);
        }
    }

    public void SetQuest(questionBase basic)
    {
        //titleText.text = title;
        mainText.text = basic.text;
        trueOrFalse = basic.trueOrFalse;
    }

    public void Answer(bool answered)
    {
        if((trueOrFalse && answered) || (!trueOrFalse && !answered))
        {
            GotRight();
        }
        else
        {
            GotWrong();
        }
    }
}
