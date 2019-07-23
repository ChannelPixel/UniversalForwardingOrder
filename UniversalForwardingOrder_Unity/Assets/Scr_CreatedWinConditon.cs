using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CreatedWinConditon : MonoBehaviour {

    public GameObject canvas;

    private void Start()
    {
        Time.timeScale = 0;
        Instantiate(canvas);
    }
}
