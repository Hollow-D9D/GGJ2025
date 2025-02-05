using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GlobalData : MonoBehaviour
{
    public enum TimeState
    {
        TIME_STATE_DAY = -1,
        TIME_STATE_NIGHT = 1
    }
    
    public static int CurrentEnemyCount;

    public static string GetScene()
    {
        return SceneManager.GetActiveScene().name == "Level1" ? "Level2" : "Level1";
    }

    public static int currentLevel = 0;

    public static Vector3 GetSecondLevelCoords()
    {
        return new Vector3(0, 0, 0); //tochange
    }
}
