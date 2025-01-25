using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    public void ChangeColor()
    {
        Debug.Log("Hop hey lalaley");
        _spriteRenderer.color = GlobalData.CurrentTimeState == GlobalData.TimeState.TIME_STATE_DAY
            ? GlobalData.DayColor
            : GlobalData.NightColor;
    }

    private void OnDestroy()
    {
        GlobalData.OnCycleChanged.RemoveListener(ChangeColor);
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        GlobalData.OnCycleChanged.AddListener(ChangeColor);
    }
    
    
}
