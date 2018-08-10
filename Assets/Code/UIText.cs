using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (TMPro.TMP_Text))]
public class UIText : MonoBehaviour {

    TMPro.TMP_Text myText;
    Coroutine myRoutine;
    string targetString;

    private void Awake()
    {
        myText = GetComponent<TMPro.TMP_Text>();
    }
    public void Text(string text)
    {
        if (myText == null) return;
        if (text == string.Empty)
        {
            myText.text = text;
            targetString = text;
            if (myRoutine != null) StopCoroutine(myRoutine);
            return;
        }
        if (targetString == text) return;
        if (myRoutine != null) StopCoroutine(myRoutine);
        targetString = text;
        myRoutine = StartCoroutine(WriteText(text));
    }
    string RandomString()
    {
        if(Random.Range(0,100) > 50) return Utilities.RandomCharacter.ToString().ToUpper();
        return Utilities.RandomCharacter.ToString().ToLower();
    }
    IEnumerator WriteText(string text)
    {
        float timePerChar = 0.5f/text.Length;
        if(text != string.Empty)
        {
            myText.text = "<color=yellow>" + RandomString() + "</color>";
            for (int i = 1; i < text.Length + 1; i++)
            {
                if (myText.text.Length > i - 1)
                {
                    if (i != 1) myText.text = myText.text.Remove(i - 1, 23);
                    else myText.text = string.Empty;
                }
                myText.text += text[i - 1];
                if (i != text.Length)
                {
                    myText.text += "<color=yellow>" + RandomString() + "</color>";
                }
                yield return new WaitForSeconds(timePerChar);
            }
        }
        myRoutine = null;
    }
}
