using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem
{
    //private static AudioSource source;
    //private static AudioSource Source
    //{
    //    get
    //    {
    //        if (!source)
    //        {
    //            SetSource();
    //        }
    //        return source;
    //    }
    //}



    //public static void SetSource()
    //{
    //    var obj = new GameObject("Audio");
    //    var s = obj.AddComponent<AudioSource>();
    //    source = s;
    //}

    public static void Play(AudioClip clip, GameObject obj, float volume = 0.25f)
    {
        var source = obj.GetComponent<AudioSource>();
        if (!source)
        {
            source = obj.AddComponent<AudioSource>();
        }
        source.volume = volume;
        source.clip = clip;
        source.Play();
    }


    public static void Play(AudioClip clip, float volume = 0.25f)
    {
        var obj = new GameObject("Audio");
        var s = obj.AddComponent<AudioSource>();
        s.volume = volume;
        s.clip = clip;
        s.Play();
        ActionDelayer.DelayAction(obj.Kill, clip.length + 1f);
    }

}
