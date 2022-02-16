using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuEvent : MonoBehaviour
{
    public GameObject MainUI;
    public GameObject ModeUI;
    public GameObject SettingUI;
    public GameObject LevelUI;
    public string selectMode;
    public GameObject[] arrTitleObject;
    Rigidbody2D[] arrTitleRigid;
    public GameObject[] arrLevelObject;
    BoxCollider2D[] arrLevelCollider;
    public GameObject[] arrClearMarkObject;
    public AudioSource clickSource;
    Vector3 mainUITargetPoint;
    Vector3 mainUIStartPoint;
    Vector3 mainUIVelocity;
    bool settingClick = false;
    bool levelClick = false;
    int selectLevel;

    public static MainMenuEvent instance;

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

        mainUITargetPoint = new Vector3(540, 845, 0);
        mainUIStartPoint = MainUI.transform.position;
        mainUIVelocity = Vector3.zero;
        LevelUI.SetActive(false);
        SettingUI.SetActive(false);
    }

    void Update()
    {
        if (MainUI.activeInHierarchy)
            MainUI.transform.position = Vector3.SmoothDamp(MainUI.transform.position, mainUITargetPoint, ref mainUIVelocity, 0.3f);
        else
            MainUI.transform.position = Vector3.SmoothDamp(MainUI.transform.position, mainUIStartPoint, ref mainUIVelocity, 0.3f);
    }

    public void OnClickModeButton(string mode)
    {
        selectMode = mode;
        if (selectMode != null)
        {
            clickSource.Play();

            ModeUI.SetActive(false);
            LevelUI.SetActive(true);

            ClearImageSet(mode);

        }
    }

    public void OnClickSettingButton()
    {
        if (!settingClick)
        {
            settingClick = true;

            clickSource.Play();

            ModeUI.SetActive(false);
            LevelUI.SetActive(false);
            SettingUI.SetActive(true);
        }
    }

    public void OnClickBackButton()
    {
        clickSource.Play();
        ModeUI.SetActive(true);
        LevelUI.SetActive(false);
        SettingUI.SetActive(false);
        settingClick = false;
    }

    public void OnClickLevelButton(int level)
    {
        if (!levelClick)
        {
            clickSource.Play();

            selectLevel = level;
            levelClick = true;
            arrTitleRigid = new Rigidbody2D[arrTitleObject.Length];
            arrLevelCollider = new BoxCollider2D[arrLevelObject.Length];
            
            for (int i = 0; i < arrTitleObject.Length; i++)
            {
                arrTitleRigid[i] = arrTitleObject[i].transform.GetComponent<Rigidbody2D>();
            }

            for (int i = 0; i < arrLevelObject.Length; i++)
            {
                arrLevelCollider[i] = arrLevelObject[i].transform.GetComponent<BoxCollider2D>();
            }

            arrLevelCollider[selectLevel - 1].isTrigger = false;

            titleEvent();
            Invoke("LoadGame", 2f);
        }
    }

    void LoadGame()
    {
        SceneManager.LoadScene(selectMode + ' ' + selectLevel, LoadSceneMode.Single);
    }

    public void ClearImageSet(string mode)
    {
        for (int i = 1; i < arrClearMarkObject.Length + 1; i++)
        {

            if (PlayerPrefs.GetInt(mode + ' ' + i) == 1)
            {
                arrClearMarkObject[i - 1].SetActive(true);
                
                if(i != arrClearMarkObject.Length)
                    arrLevelObject[i].SetActive(true);
            }

        }
    }

    public void titleEvent()
    {
        for (int i = 0; i < arrTitleRigid.Length; i++)
        {
            arrTitleRigid[i].simulated = true;
        }
    }
}
