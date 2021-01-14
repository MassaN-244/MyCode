using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Win_2p : MonoBehaviour
{
    [SerializeField] private float degreeInTime = 3.0f;
    public float fadeSpeed = 0.005f;
    private Color textColor;

    public GameObject clearText2;

    void Start()
    {
        textColor = clearText2.GetComponent<TextMeshProUGUI>().color;
        textColor.a = 0;
        clearText2.GetComponent<TextMeshProUGUI>().color = textColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (clearText2.activeSelf == true)
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
            clearText2.GetComponent<TextMeshProUGUI>().color = textColor;
        }

    }

    void ChangeScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
