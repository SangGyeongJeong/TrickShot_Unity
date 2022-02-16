using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject direction;
    public Renderer render;
    public float power; // 던지는 힘
    public GameObject target; // 게임 클리어를 위한 타겟 오브젝트
    public GameObject targetPoint; // 타겟의 위치를 알려주는 오브젝트
    public GameObject MainUI;
    public GameObject MenuUI;
    public GameObject ModeUI;
    public GameObject LevelUI;
    public float necessaryTime; // 게임 클리어를 위해 필요한 시간
    public bool isShoot; // Shoot()함수를 게임에서 한 번만 사용하기 위해 필요한 변수
    public bool changeRotate = false;
    public bool directionRotate = true;
    public float rotateSpeed = 1.0f;
    public AudioSource source;
    public AudioSource successSource;
    public AudioSource FailSource;

    Rigidbody rigid;
    BoxCollider targetCollider; // 타겟의 콜라이더
    Text text;

    Touch touch1, touch2;
    float xmove, ymove;

    bool targetStay = false;
    bool otherStay = false;
    bool gameEnd = false;
    float time;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        targetCollider = target.GetComponent<BoxCollider>();

        rigid.isKinematic = true;
        isShoot = false;

        if (PlayerPrefs.HasKey("Sensitive"))
            rotateSpeed = PlayerPrefs.GetFloat("Sensitive");

    }

    void Update()
    {
        RotatePlayer();
        RotateDirection();
    }

    void RotatePlayer() // 물체 회전
    {
        if (changeRotate)
        {
            if (Input.touchCount == 1)
            {
                touch1 = Input.GetTouch(0);
                if (touch1.phase == TouchPhase.Moved)
                {
                    ymove = touch1.deltaPosition.y * 0.1f;
                    xmove = touch1.deltaPosition.x * 0.1f;
                    transform.Rotate(0, -xmove * rotateSpeed * 10 * Time.deltaTime, 0, Space.World);
                    transform.Rotate(0, 0, ymove * rotateSpeed * 10 * Time.deltaTime, Space.World);
                }
            }
        }
    }

    void RotateDirection() // 발사방향 회전
    {
        if (directionRotate)
        {
            if (Input.touchCount == 2)
            {
                touch1 = Input.GetTouch(0);
                touch2 = Input.GetTouch(1);

                render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, 0.3f); // 발사방향 설정 중일때 물체 투명하게 변경

                if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
                {
                    xmove = ((touch1.deltaPosition.x + touch2.deltaPosition.x) * 0.5f) * 0.1f;
                    ymove = ((touch1.deltaPosition.y + touch2.deltaPosition.y) * 0.5f) * 0.1f;

                    direction.transform.Rotate(0f, xmove * rotateSpeed * 10 * Time.deltaTime, 0f, Space.Self);
                    direction.transform.Rotate(-ymove * rotateSpeed * 10 * Time.deltaTime, 0f, 0f);
                }

            }
            else
            {
                render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, 1f); // 발사방향 설정이 끝나면 물체 투명도 복구
            }
        }
    }

    void FixedUpdate()
    {
        SuccessRound();
    }

    void SuccessRound()
    {
        if (isShoot)
        {
            if (targetStay && !gameEnd)
            {
                time += Time.fixedDeltaTime;

                if (time > necessaryTime)
                {
                    gameEnd = true;
                    GameManager.instance.success = true;
                    PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);

                    successSource.Play();

                    StartCoroutine(SuccessMessage());
                }
            }
            else if (otherStay && !gameEnd)
            {
                time += Time.fixedDeltaTime;

                if (time > necessaryTime - 1f)
                {
                    gameEnd = true;
                    GameManager.instance.success = false;

                    FailSource.Play();
                    Invoke("DelayLoadScene", 0.25f);
                }
            }
        }
    }
    
    void DelayLoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SuccessMessage()
    {
        string[] select = SceneManager.GetActiveScene().name.Split(' ');
        MainUI.SetActive(false);
        MenuUI.SetActive(true);
        ModeUI.SetActive(false);
        LevelUI.SetActive(true);
        MainMenuEvent.instance.ClearImageSet(select[0]);

        GameObject target = LevelUI.transform.GetChild(int.Parse(select[1])).gameObject.transform.GetChild(0).gameObject;
        Image targetIamge = target.GetComponent<Image>();
        yield return new WaitForSeconds(0.3f);
        target.SetActive(true);
        targetIamge.rectTransform.sizeDelta = new Vector2(300f, 300f);
        yield return new WaitForSeconds(0.05f);
        targetIamge.rectTransform.sizeDelta = new Vector2(287.5f, 287.5f);
        yield return new WaitForSeconds(0.05f);
        targetIamge.rectTransform.sizeDelta = new Vector2(275f, 275f);
        yield return new WaitForSeconds(0.05f);
        targetIamge.rectTransform.sizeDelta = new Vector2(262.5f, 262.5f);
        yield return new WaitForSeconds(0.05f);
        targetIamge.rectTransform.sizeDelta = new Vector2(250f, 250f);
        yield return new WaitForSeconds(0.05f);
        targetIamge.rectTransform.sizeDelta = new Vector2(237.5f, 237.5f);
        yield return new WaitForSeconds(0.05f);
        targetIamge.rectTransform.sizeDelta = new Vector2(225f, 225f);
        yield return new WaitForSeconds(0.05f);
        targetIamge.rectTransform.sizeDelta = new Vector2(212.5f, 212.5f);
        yield return new WaitForSeconds(0.05f);
        targetIamge.rectTransform.sizeDelta = new Vector2(200f, 200f);
        yield return new WaitForSeconds(0.05f);
        targetIamge.rectTransform.sizeDelta = new Vector2(187.5f, 187.5f);
        yield return new WaitForSeconds(0.05f);
        targetIamge.rectTransform.sizeDelta = new Vector2(175f, 175f);
        yield return new WaitForSeconds(0.05f);
        targetIamge.rectTransform.sizeDelta = new Vector2(162.5f, 162.5f);
        yield return new WaitForSeconds(0.05f);
        targetIamge.rectTransform.sizeDelta = new Vector2(150f, 150f);
        yield return null;
    }

    public void Shoot()
    {
        targetPoint.SetActive(false);
        direction.SetActive(false);

        rigid.isKinematic = false;
        isShoot = true;
        rigid.AddTorque(direction.transform.right, ForceMode.Impulse);
        rigid.AddForce(direction.transform.forward * power, ForceMode.Impulse); // AddForce: 월드 좌표를 기준으로 힘이 가해짐, AddRelativeForce: 로컬 좌표를 기준으로 힘이 가해짐
    }

    void OnCollisionEnter(Collision other)
    {
        source.Play();
    }

    void OnCollisionStay(Collision other)
    {
        if (targetCollider == other.collider)
        {
            targetStay = true;
        }
        else
        {
            otherStay = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (targetCollider == other.collider)
        {
            time = 0;
            targetStay = false;
        }
        else
        {
            time = 0;
            otherStay = false;
        }
    }

}
