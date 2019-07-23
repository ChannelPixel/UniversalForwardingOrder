using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scr_TxtGravityDebug : MonoBehaviour
{
    TextMesh _textMesh;

    public void OnEnable()
    {
        _textMesh = gameObject.GetComponent<TextMesh>();
        if (_textMesh.text == "Off")
        {
            _textMesh.text = "On";
            _textMesh.color = new Color(0f, 255f, 0f, 0.33f);
        }
        else
        {
            _textMesh.text = "Off";
            _textMesh.color = new Color(255f, 0f, 0f, 0.33f);
        }
    }

    public void UpdateGravityStatus()
    {
        if(_textMesh.text == "Off")
        {
            _textMesh.text = "On";
            _textMesh.color = new Color(0f,255f,0f, 0.33f);
        }
        else
        {
            _textMesh.text = "Off";
            _textMesh.color = new Color(255f, 0f, 0f, 0.33f);
        }
    }
}
