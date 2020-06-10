using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    

    public void InputLeft()
    {
        EventManager.TriggerEvent("InputManager:Input", new EventParam { Float = -1f });
    }

    public void InputRight()
    {
        EventManager.TriggerEvent("InputManager:Input", new EventParam { Float = 1f });
    }

    public void InputJump()
    {
        EventManager.TriggerEvent("InputManager:Input", new EventParam { Bool = true });
    }

    public void InputDuck()
    {
        EventManager.TriggerEvent("InputManager:Input", new EventParam { Int = 1 });
    }

    public void InputUnDuck()
    {
        EventManager.TriggerEvent("InputManager:Input", new EventParam { Int2 = 1 });
    }

    public void InputShoot()
    {
        EventManager.TriggerEvent("InputManager:Actions", new EventParam { Bool = true });
    }
}
