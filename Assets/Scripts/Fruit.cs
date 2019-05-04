using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fruit : Entity
{

    public AudioClip pickSound;
    public AudioClip spawnSound;

    public void PickUp()
    {
        SoundSystem.Play(pickSound, 0.6f);
        Manager.instance.Score();
        currentCell.Contains = null;
        //try outzoom
        //play animation
        Destroy(gameObject);
    }

    public void Stomp()
    {
        Manager.instance.Score(false);
        currentCell.Contains = null;
        Destroy(gameObject);
    }

    public void SpawnEffect()
    {
        var scale = transform.localScale;
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        transform.DOScale(scale, 0.1f);
        SoundSystem.Play(spawnSound, 0.25f);
    }
}
