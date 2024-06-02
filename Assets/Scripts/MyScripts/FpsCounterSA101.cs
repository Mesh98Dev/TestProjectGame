using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FpsCounterST101 : MonoBehaviour
{

    public TextMeshProUGUI fpsText;
    private float deltaTime;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = string.Format("{0:0.} FPS", fps);
    }
}


