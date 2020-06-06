using System;
using UnityEngine;

public enum UIScreenTypes { Begin, Mid, Pause, Win, Lose };



public class UIScreenManager : MonoBehaviour
{
    private Action<EventParam> openScreenListener;

    // The screen list
    [SerializeField]
    private UIScreen[] uiScreens;
    public UIScreen[] UIScreens { get => uiScreens; set => uiScreens = value; }

    // What screen needs to be activated
    private UIScreenTypes screenStateCurrent;
    public UIScreenTypes ScreenStateCurrent
    {
        get => screenStateCurrent;

        set
        {
            CloseScreen(ScreenStateCurrent);

            screenStateCurrent = value;

            OpenScreen(value);

            switch (ScreenStateCurrent)
            {
                case UIScreenTypes.Begin:// At the beginning of the game (When restarted)

                    break;

                case UIScreenTypes.Mid:

                    break;

                case UIScreenTypes.Pause:

                    break;

                case UIScreenTypes.Win:

                    break;

                case UIScreenTypes.Lose:

                    break;
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        // Setup
        ToggleAllScreens(false);

        ScreenStateCurrent = UIScreenTypes.Begin;

        openScreenListener = new Action<EventParam>(OpenScreen);
    }

    void OnEnable()
    {
        //Register With Action variable
        EventManager.StartListening("UIScreen:Open", OpenScreen);
        EventManager.StartListening("UIScreen:Close", CloseScreen);
    }

    void OnDisable()
    {
        //Un-Register With Action variable
        EventManager.StopListening("UIScreen:Open", OpenScreen);
        EventManager.StopListening("UIScreen:Close", CloseScreen);
    }

    //- Open/Close screen functions -

    public void ToggleAllScreens(bool _open)
    {
        for (int i = 0; i < UIScreens.Length; i++)
        {
            UIScreens[i].gameObject.SetActive(_open);
        }
    }

    public void ToggleScreen(UIScreenTypes _screen, bool _open)
    {
        for (int i = 0; i < UIScreens.Length; i++)
        {
            if (UIScreens[i].ScreenType == _screen)
            {
                UIScreens[i].gameObject.SetActive(_open);

                break;
            }
        }
    }

    public void OpenScreen(UIScreenTypes _screen)
    {
        ToggleScreen(_screen, true);
    }

    public void OpenScreen(EventParam _screen)
    {
        ToggleScreen(_screen.UIScreenType, true);
    }

    public void CloseScreen(UIScreenTypes _screen)
    {
        ToggleScreen(_screen, false);
    }

    public void CloseScreen(EventParam _screen)
    {
        ToggleScreen(_screen.UIScreenType, false);
    }
}
