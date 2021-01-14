using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Win_1p : MonoBehaviour
{
    [SerializeField] private float degreeInTime = 3.0f;
    public float fadeSpeed = 0.005f;
    private Color textColor;

    public GameObject clearText1;

    void Start()
    {
        textColor = clearText1.GetComponent<TextMeshProUGUI>().color;
        textColor.a = 0;
        clearText1.GetComponent<TextMeshProUGUI>().color = textColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (clearText1.activeSelf == true) 
        {
            FadeIn();
            Invoke("ChangeScene", 4.0f);
        }
    }

    void FadeIn()
    {
        if (textColor.a <= 1)
        {
            textColor.a += fadeSpeed;
            clearText1.GetComponent<TextMeshProUGUI>().color = textColor;
        }   
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
