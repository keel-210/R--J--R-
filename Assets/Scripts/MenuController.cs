using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuController : MonoBehaviour
{
    [SerializeField] List<GameObject> StartObjs, PauseObjs, RestartObjs, ClearObjs;
    float StartTimer = 0;
    bool StartFlg;
    bool Cancel, IsPausing, CancelCash, CancelPalse;
    void Start()
    {
        PauseObjs.ForEach(go => go.SetActive(false));
        RestartObjs.ForEach(go => go.SetActive(false));
        ClearObjs.ForEach(go => go.SetActive(false));
    }
    void Update()
    {
        Cancel = Input.GetAxisRaw("Cancel") > 0;
        if (!Cancel)
        {
            CancelCash = Cancel;
        }
        CancelPalse = Cancel ^ CancelCash;
        if (CancelPalse && !IsPausing)
        {
            MenuOpen();
        }
        else if (CancelPalse && IsPausing)
        {
            MenuClose();
        }
        if (Cancel)
        {
            CancelCash = Cancel;
        }
    }
    void MenuOpen()
    {
        if (Time.timeScale == 1)
        {
            IsPausing = true;
            Time.timeScale = 0;
            PauseObjs.ForEach(go => go.SetActive(true));
        }
    }
    public void MenuClose()
    {
        IsPausing = false;
        Time.timeScale = 1;
        PauseObjs.ForEach(go => go.SetActive(false));
    }
    public void Restart()
    {
        RestartObjs.ForEach(go => go.SetActive(true));
    }
    public void Clear()
    {
        Time.timeScale = 0.1f;
        StartCoroutine(DelayMethod(0.3f, () =>
         {
             ClearObjs.ForEach(go => go.SetActive(true));
             Time.timeScale = 1;
         }));
    }
    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

}
