﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI text;


    private void Update()
    {
        text.text = Manager.instance.Points.ToString();
    }
}
