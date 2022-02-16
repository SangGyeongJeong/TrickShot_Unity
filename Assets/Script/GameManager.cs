using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool success;
    int escapeCount;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        EscapeGame();
    }

    void EscapeGame()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !ButtonEvent.instance.settingClick && !ButtonEvent.instance.explainClick)
            {
                escapeCount++;
                if(escapeCount == 1)
                    ShowAndroidToastMessage("뒤로가기 버튼을 한번 더 누르시면 종료됩니다.");
                    
                if (!IsInvoking("NoDoubleClick")) // Invoke함수 호출이 있었는지 없었는지 확인하는 함수
                    Invoke("NoDoubleClick", 3.0f);
            }
        }

        if (escapeCount == 2)
        {
            CancelInvoke("NoDoubleClick");
            PlayerPrefs.Save();
            Application.Quit();
        }
    }

    void NoDoubleClick()
    {
        escapeCount = 0;
    }

    void ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
}
