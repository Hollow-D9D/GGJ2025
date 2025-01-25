using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalData : MonoBehaviour
{
    public enum TimeState
    {
        TIME_STATE_DAY = -1,
        TIME_STATE_NIGHT = 1
    }

    public static Color DayColor = Color.cyan;
    
    public static Color NightColor = Color.yellow;
    
    public static int CurrentEnemyCount;
    public static TimeState CurrentTimeState = TimeState.TIME_STATE_DAY;

    public static UnityEvent OnCycleChanged = new UnityEvent();
}
