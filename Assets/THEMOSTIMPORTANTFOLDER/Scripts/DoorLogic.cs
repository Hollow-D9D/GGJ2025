using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLogic : Bubblable
{
    private bool visible = false;
    
    public override void Bubbled(float duration)
    {
        Debug.Log("Door");
        GetComponent<SpriteRenderer>().enabled = true;
        visible = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && visible)
        {
            if (GlobalData.currentLevel == 2)
            {
                other.transform.position = GlobalData.GetSecondLevelCoords();
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
