﻿using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffectOnce : MonoBehaviour
{
    public TextMeshProUGUI textContent;
    public float delayBetweenCharacters = 0.125f;
    private string story;

    void Awake()
    {
        story = textContent.text;
        textContent.text = "";

        StartCoroutine("PlayText");
    }

    IEnumerator PlayText()
    {
        foreach (char c in story)
        {
            textContent.text += c;
            yield return new WaitForSeconds(delayBetweenCharacters);
        }
    }

}
