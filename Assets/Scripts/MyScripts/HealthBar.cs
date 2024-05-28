using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;


    // Start is called before the first frame update
    public void UpdateHealthBar (float currentValue, float maxValue)
        {
            slider.value = currentValue / maxValue;
            Debug.Log(slider.value);
        }
        

    // Update is called once per frame
    void Update()
    {
       // UpdateHealthBar();
    }
}
