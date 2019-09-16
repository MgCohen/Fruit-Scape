using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tester : MonoBehaviour
{
    public GameEvent stopper;

    private void OnEnable()
    {
        stopper.Raise();
    }

    private void OnDisable()
    {
        stopper.CloseAll();
    }
}
