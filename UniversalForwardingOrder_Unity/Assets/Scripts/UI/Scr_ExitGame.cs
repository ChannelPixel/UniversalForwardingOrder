using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ExitGame : MonoBehaviour
{
    public void ExitGame()
    {
        /*if(UnityEditor.EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }*/
        Application.Quit();
    }
}
