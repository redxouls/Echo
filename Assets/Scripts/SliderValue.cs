using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValue : MonoBehaviour
{
    public Slider sliderUI;
    public TextMeshProUGUI textSliderValue;
    public string title;
    // Start is called before the first frame update
    void Start()
    {
        // sliderUI.value = PlayerPrefs.GetFloat(title);
    }

    // Update is called once per frame
    void Update()
    {
        string sliderMessage = title + "\n" + sliderUI.value.ToString("F1");
        textSliderValue.text = sliderMessage;
        PlayerPrefs.SetFloat(title, sliderUI.value);
    }
}
