using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GlobalData.CurrentTimeState = (GlobalData.TimeState)((int)GlobalData.CurrentTimeState * -1);
            // Debug.Log(GlobalData.CurrentTimeState);
            
            GlobalData.OnCycleChanged.Invoke();
        }
    }
}
