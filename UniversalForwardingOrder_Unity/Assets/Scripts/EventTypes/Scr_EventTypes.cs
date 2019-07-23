using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scr_EventTypes
{
    [System.Serializable]

    public class InputEvent : UnityEvent<Scr_IInput>
    {

    }
}
