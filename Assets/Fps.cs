using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float fps;
    private GUIStyle style;

    void Start()
    {
        // Create a GUIStyle for displaying the FPS counter
        style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.white;
    }

    void Update()
    {
        // Calculate FPS by dividing 1 by the time it takes to render one frame (deltaTime)
        fps = 1f / Time.deltaTime;
    }

    void OnGUI()
    {
        // Display FPS counter on screen
        GUI.Label(new Rect(10, 10, 100, 50), "FPS: " + Mathf.Round(fps), style);
    }
}
