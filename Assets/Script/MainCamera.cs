using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject player;
    public float xmove = 0; // x축 누적 이동량
    public float ymove = 0; // y축 누적 이동량
    public float distance = 3;
    public float cameraMoveSpeed = 1f;
    public float rayHitChange = 1f;
    public bool stopMove = false; // 카메라 움직임 멈춤

    Vector3 reverseDistance;
    Touch touch;

    RaycastHit rayHit;
    Ray[] ray;

    void Start()
    {
        ray = new Ray[4];

        transform.rotation = Quaternion.Euler(xmove, ymove, 0); // 이동량에 따라 카메라의 바라보는 방향을 조정

        if (PlayerPrefs.HasKey("Sensitive"))
            cameraMoveSpeed = PlayerPrefs.GetFloat("Sensitive");
    }

    void FixedUpdate()
    {
        reverseDistance = new Vector3(0.0f, 0.0f, distance); // 카메라가 바라보는 앞방향은 z축입니다. 이동량에 따른 z축 방향의 벡터를 구함
        transform.position = player.transform.position - transform.rotation * reverseDistance;

        HitRayToObject();
    }

    void Update()
    {
        if (!stopMove)
        {
            CameraMove();
        }
    }

    void CameraMove()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                xmove += touch.deltaPosition.y * cameraMoveSpeed * 0.5f * Time.deltaTime; // 마우스의 상하 이동량을 ymove에 누적
                xmove = Mathf.Clamp(xmove, 16.5f, 90f);

                ymove += touch.deltaPosition.x * cameraMoveSpeed * 0.5f * Time.deltaTime; // 마우스의 좌우 이동량을 xmove에 누적

                transform.rotation = Quaternion.Euler(xmove, ymove, 0); // 이동량에 따라 카메라의 바라보는 방향을 조정

                transform.position = player.transform.position - transform.rotation * reverseDistance;
            }

        }
    }

    void HitRayToObject()
    {
        ray[0].direction = transform.up;
        ray[1].direction = -transform.up;
        ray[2].direction = transform.right;
        ray[3].direction = -transform.right;

        for (int i = 0; i < ray.Length; i++)
        {
            ray[i].origin = transform.position;

            if (Physics.Raycast(ray[i], out rayHit, 0.5f))
            {
                stopMove = true;

                if (i == 0)
                {
                    xmove -= rayHitChange;
                }
                else if (i == 1)
                {
                    xmove += rayHitChange;
                }
                else if (i == 2)
                {
                    ymove += rayHitChange;
                }
                else if (i == 3)
                {
                    ymove -= rayHitChange;
                }

                transform.rotation = Quaternion.Euler(xmove, ymove, 0);
                stopMove = false;
            }
        }
    }
}
