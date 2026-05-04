using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Necesario para modificar los textos

public class MathCaptcha : MonoBehaviour
{
    [SerializeField] private TMP_Text problemText;
    [SerializeField] private TMP_Text[] buttonTexts;
    private int correctAnswer;

    void Start()
    {
        GenerateProblem();
    }

    private void GenerateProblem()
    {
        int num1 = Random.Range(1, 20);
        int num2 = Random.Range(1, 20);
        correctAnswer = num1 + num2;

        problemText.text = $"{num1} + {num2} = ?";

        int correctButtonIndex = Random.Range(0, buttonTexts.Length);

        for (int i = 0; i < buttonTexts.Length; i++)
        {
            if (i == correctButtonIndex)
            {
                buttonTexts[i].text = correctAnswer.ToString();
            }
            else
            {
                int fakeAnswer = correctAnswer + Random.Range(-5, 5);
                if (fakeAnswer == correctAnswer) fakeAnswer += 2;
                buttonTexts[i].text = fakeAnswer.ToString();
            }
        }
    }

    public void OnAnswerClicked(int buttonIndex)
    {
        if (buttonTexts[buttonIndex].text == correctAnswer.ToString())
        {
            GetComponent<CaptchaWindow>().ResolveCaptcha();
        }
        else
        {
            GetComponent<CaptchaWindow>().TriggerError();
            GenerateProblem();
        }
    }
}
