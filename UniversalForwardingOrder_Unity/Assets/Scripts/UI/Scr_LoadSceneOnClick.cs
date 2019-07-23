using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_LoadSceneOnClick : MonoBehaviour
{
    public void LoadByIndex(int sceneIndex)
    {
        if(sceneIndex == 1)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(sceneIndex);
    }
}
