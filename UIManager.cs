using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UM
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private float degreeInTime = 3.0f;
        public Color color1;
        public Color color2;
        public float fadeSpeed = 0.02f;

        public GameObject player1Turn;
        public GameObject player2Turn;

        private void Start()
        {
            //SetTextAndColor(player1Turn, color1);
            /*color[0] = player1Turn.GetComponent<TextMeshProUGUI>().color;
            color[0].a = 0;
            player1Turn.GetComponent<Text>().color = color[0];*/
        }

        private void Update()
        {
            //FadeIn(player1Turn, color1);
        }

        public void SetTextAndColor(GameObject text, Color color)
        {
            color = text.GetComponent<TextMeshProUGUI>().color;
            color.a = 0;
            text.GetComponent<TextMeshProUGUI>().color = color;
        }

        public void FadeIn(GameObject text, Color color)
        {
            if (color.a <= 1)
            {
                color.a += fadeSpeed;
                text.GetComponent<TextMeshProUGUI>().color = color;
            }
            else
            {
                return;
            }
        }
    }
}

