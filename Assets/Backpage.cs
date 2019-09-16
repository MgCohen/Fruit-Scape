using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Backpage : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void BackPage();

    public void Back()
    {
        BackPage();

    }
}
