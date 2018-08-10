using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasFader : MonoBehaviour {
    #region Properties
    CanvasGroup myGroup;
    public float fadeSpeed = 1f;
    bool isOn;
    Action OnClose;
    Action OnOpen;
    Coroutine waitRoutine;
    #endregion
    public bool IsOpen
    {
        get { return isOn; }
    }
    private void Awake()
    {
        myGroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        StartCoroutine(Tick());
    }
    public void Toggle(bool value)
    {
        isOn = value;
    }
    public void Trigger(float waitTime)
    {
        if (waitRoutine != null) StopCoroutine(waitRoutine);
        waitRoutine = StartCoroutine(WaitFade(waitTime));
    }
    IEnumerator Tick()
    {
        while(true)
        {
            if (isOn)
            {
                if (myGroup.alpha < 1f)
                {
                    myGroup.alpha += Time.deltaTime * fadeSpeed;
                    if (myGroup.alpha >= 1f && OnOpen != null) OnOpen();
                }
            }
            else
            {
                if (myGroup.alpha > 0f)
                {
                    myGroup.alpha -= Time.deltaTime * fadeSpeed;
                    if (myGroup.alpha <= 0f && OnClose != null) OnClose();
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator WaitFade(float time)
    {
        while (time > 0f)
        {
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        isOn = false;
        waitRoutine = null;
    }
}
