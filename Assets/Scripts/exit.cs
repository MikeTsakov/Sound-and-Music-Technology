﻿/*
The MIT License (MIT)

Copyright (c) 2018 Victor van Andel, Chun He
Copyright (c) 2018 Twan Veldhuis, Ivar Troost

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class exit : MonoBehaviour {
    
    private string soundModeString = Log.CurrentMode.ToString();

    // Upon entering the exit scene, the log is sent to the webserver
    public void Start () {
        StartCoroutine(SendLog());
    }

    IEnumerator SendLog()
    {
        string url = "http://www.students.science.uu.nl/~5718171/receiveLog.php";

        WWWForm logForm = new WWWForm();
        logForm.AddField("Title", Log.logTitle);
        logForm.AddField("Content", Log.logContent);
        logForm.AddField("Variation", soundModeString);

        using (UnityWebRequest www = UnityWebRequest.Post(url, logForm))
        {
            yield return www.Send();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}