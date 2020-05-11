using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowsSwitcher : MonoBehaviour {

    public CanvasGroup[] windows;
    List<Vector3> windowsStartPos = new List<Vector3>();
    public Canvas canvas;
    [SerializeField]
    private int activeWindow = -1;
    [SerializeField]
    public int windowMoveDir = 1;

    private void Awake()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windowsStartPos.Add(windows[i].transform.position);
        }
    }

    // Update is called once per frame
    void LateUpdate () {
        
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].blocksRaycasts = i==activeWindow;
            Vector3 targetPos = windowsStartPos[i] + (new Vector3(windowMoveDir,0,0) * (canvas.pixelRect.width) * (activeWindow == i ? 0 : 1));
            //windows[i].transform.position =Vector3.MoveTowards( windows[i].transform.position, targetPos, canvas.pixelRect.width/15.0f);
            windows[i].transform.position =Vector3.Lerp( windows[i].transform.position, targetPos, Time.deltaTime*15.0f);
        }
	}

    public void SwitchWindow(int window)
    {
        if (window == activeWindow) return;

        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].alpha = (i == window || i == activeWindow) ? 1 : 0;
        }

        int temp = activeWindow;
        activeWindow = window;
        windows[activeWindow].transform.position = windowsStartPos[activeWindow] + (new Vector3(temp > activeWindow ? -1 : 1, 0, 0) * canvas.pixelRect.width);
        windowMoveDir = temp > activeWindow ? 1 : -1;

    }

    public void ChangeActiveWindow(int window)
    {
        activeWindow = window;
    }

    public void Next()
    {
        if (activeWindow < windows.Length - 1)
            SwitchWindow(activeWindow++);
        else SwitchWindow(0);
    }

    public void CloseAll()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].alpha = 0;
        }
        activeWindow = -1;
    }
}
