using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreen : MonoBehaviour
{
    [SerializeField]
    private UIScreenTypes screenType;
    public UIScreenTypes ScreenType { get => screenType; set => screenType = value; }
}
