using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PauseGame : MonoBehaviour
{
    bool inputEsc;
    bool lastInputEsc;

    public GameObject canvas;

    private void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        inputEsc = Input.GetKeyDown(KeyCode.Escape);


        if(inputEsc)
        {
            if (canvas != null)
            {
                if (canvas.activeInHierarchy == false)
                {
                    canvas.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    canvas.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }
    }
}
