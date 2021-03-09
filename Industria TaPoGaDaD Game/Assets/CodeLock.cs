﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class CodeLock : MonoBehaviour
{

    int codeLength;
    int placeInCode;

    public string code = "";
    public string attemptedCode;

    public UnityEvent LockDoor;
    public TextMeshProUGUI text;

    private void Start()
    {
        codeLength = code.Length;
        resetCode();
    }

    void CheckCode()
    {
        text.faceColor = Color.green;
        if (attemptedCode == code)
        {
            LockDoor.Invoke();
        }
        else
        {
            Debug.Log("Wrong Code");
            text.faceColor = Color.red;
        }
    }


    public void SetValue(string value)
    {
        placeInCode++;

        if (placeInCode == 0)
        {
            attemptedCode += value;
            text.SetText(attemptedCode.ToString());
        }
        else if (placeInCode <= codeLength)
        {
            text.faceColor = Color.black;
            attemptedCode += value;
            text.SetText(attemptedCode.ToString());
        }

        if (placeInCode == codeLength)
        {
            CheckCode();

            attemptedCode = "";
            placeInCode = 0;
        }
    }

    public void resetCode()
    {
        attemptedCode = "";
        text.SetText("Put in code");
        text.faceColor = Color.black;
    }
}
