using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public Player player;
    public MainCamera cam;

    public GameObject ShootButton;
    public GameObject ChangeButton;
    public GameObject SettingButton;
    public GameObject ExplainButton;
    public Image PowerGaugeBG;
    public Image PowerGauge;
    public GameObject MainUI;
    public GameObject SettingUI;
    public GameObject ExplainUI;
    public Sprite ChangeImage;
    public Sprite CameraImage;
    public Slider BgSlider;
    public Slider SFXSlider;
    public Slider SensitiveSlider;
    public AudioSource ClickSource;

    public bool settingClick = false;
    public bool explainClick = false;
    bool shootClick = false;
    bool changeClick = false;
    bool curCamStopState;
    bool curRotateState;

    public static ButtonEvent instance;


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

        if(PowerGauge != null && SettingUI != null)
        {
            PowerGauge.fillAmount = player.power / 10;
            SettingUI.SetActive(false);
        }

        if (PlayerPrefs.HasKey("Sensitive"))
            SensitiveSlider.value = PlayerPrefs.GetFloat("Sensitive");
        if (PlayerPrefs.HasKey("BGM"))
            BgSlider.value = PlayerPrefs.GetFloat("BGM");
        if (PlayerPrefs.HasKey("SFX"))
            SFXSlider.value = PlayerPrefs.GetFloat("SFX");

    }
    void Update()
    {
        if(player != null)
            Shoot();
    }

    void Shoot()
    {
        if (!player.isShoot)
        {
            if (shootClick) // 버튼이 눌려지는 동안 파워 증가
            {
                if (player.power < 10)
                    player.power += 3f * Time.deltaTime;
                else
                    player.power = 10;

                PowerGauge.fillAmount = player.power / 10;
            }
        }
    }

    public void PowerGaugeValue(float val)
    {
        player.power = val;
    }

    public void PointDown()
    {
        ClickSource.Play();

        shootClick = true;
        cam.stopMove = true;
        player.changeRotate = false;
        player.directionRotate = false;
    }

    public void PointUp() // 버튼이 떼지면 isShoot이 false인 경우 Shoot()함수 실행
    {
        shootClick = false;

        if (!player.isShoot)
        {
            player.Shoot();
            cam.stopMove = false;

            ShootButton.SetActive(false);
            ChangeButton.SetActive(false);

            Destroy(PowerGauge);
            Destroy(PowerGaugeBG);
        }
    }

    public void OnClickChangeButton()
    {
        if (!changeClick)
        {
            ClickSource.Play();

            ChangeButton.transform.GetChild(0).GetComponent<Image>().sprite = CameraImage;

            changeClick = true;
            cam.stopMove = true;
            player.changeRotate = true;
        }
        else
        {
            ClickSource.Play();

            ChangeButton.transform.GetChild(0).GetComponent<Image>().sprite = ChangeImage;

            changeClick = false;
            cam.stopMove = false;
            player.changeRotate = false;
        }

    }

    public void OnClickSettingButton()
    {
        if (!settingClick)
        {
            ClickSource.Play();

            curCamStopState = cam.stopMove;
            curRotateState = player.changeRotate;

            player.changeRotate = false;
            cam.stopMove = true;
            settingClick = true;

            MainUI.SetActive(false);
            SettingUI.SetActive(true);
        }
    }

    public void OnClickExplainButton()
    {
        if(!explainClick)
        {
            ClickSource.Play();

            curCamStopState = cam.stopMove;
            curRotateState = player.changeRotate;

            player.changeRotate = false;
            cam.stopMove = true;
            explainClick = true;

            MainUI.SetActive(false);
            ExplainUI.SetActive(true);
        }
    }

    public void OnClickBackButton()
    {
        ClickSource.Play();

        cam.stopMove = curCamStopState;
        player.changeRotate = curRotateState;

        MainUI.SetActive(true);

        if(settingClick)
        {
            settingClick = false;

            SettingUI.SetActive(false);
        }
        else if(explainClick)
        {
            explainClick = false;

            ExplainUI.SetActive(false);
        }

        PlayerPrefs.Save();
    }

    public void Sensitive(float val)
    {
        player.rotateSpeed = val;
        cam.cameraMoveSpeed = val;
        PlayerPrefs.SetFloat("Sensitive", val);
    }

}
