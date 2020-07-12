﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class TSVParser : MonoBehaviour
{
    public string fileName;

    public List<string> wordList;

    public List<bool> isSpoken;

    public void Start()
    {
        StreamReader reader = new StreamReader("./Assets/TSV/" + fileName + ".tsv");

        string text = reader.ReadToEnd();

        Debug.Log(text);

        wordList = new List<string>(text.Split('\t'));

        clean();

    }

    void clean()
    {
        wordList.RemoveAt(0);
        wordList.RemoveAt(0);
        wordList.RemoveAt(0);

        List<string> newWordList = new List<string>();



        for(int i = 0; i < wordList.Count; i++)
        {
            switch(i % 2)
            {
                case 0:
                    if (wordList[i] != "Words")
                    {
                        newWordList.Add(wordList[i]);
                    }
                    break;
                case 1:
                    break;
            }
        }

        for (int i = 0; i < newWordList.Count; i++)
        {
            if(newWordList[i].Substring(0, 3) == "P: ")
            {
                isSpoken.Add(true);
                newWordList[i] = newWordList[i].Remove(0, 3);
            } else
            {
                isSpoken.Add(false);
            }
        }

        wordList = newWordList;
        
    }
}