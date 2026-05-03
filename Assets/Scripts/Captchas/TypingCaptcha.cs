using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingCaptcha : MonoBehaviour
{
    [SerializeField] private TMP_Text targetWordText;
    [SerializeField] private TMP_InputField inputField;

    private string targetWord;
    void Start()
    {
        int wordLength = 4;
        targetWord = GenerateRandomString(wordLength);
        targetWordText.text = "Type: " + targetWord;

        inputField.onValueChanged.AddListener(CheckInput);
    }

    private void CheckInput(string currentText)
    {
        if (currentText.ToUpper() == targetWord)
        {
            GetComponent<CaptchaWindow>().ResolveCaptcha();
        }
    }

    private string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string result = "";

        for (int i = 0; i < length; i++)
        {
            result += chars[Random.Range(0, chars.Length)];
        }
        return result;
    }
}

