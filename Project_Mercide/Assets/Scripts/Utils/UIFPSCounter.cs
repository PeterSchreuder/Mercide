using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFPSCounter : MonoBehaviour
{
    private Text textComponent;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<Text>();
        StartCoroutine(fpsUpdater());
    }

    private IEnumerator fpsUpdater()
    {
        yield return new WaitForSeconds(1f);
        textComponent.text = "FPS: " + ((int)(1f / Time.unscaledDeltaTime)).ToString();

        StartCoroutine(fpsUpdater());
    }
}
