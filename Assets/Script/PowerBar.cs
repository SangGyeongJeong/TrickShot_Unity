using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PowerBar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public MainCamera mainCam;
    public Player player;

    bool curChangeRotate;
    bool curDirectionRotate;
    bool curCamMove;

    public void OnPointerDown(PointerEventData eventData)
    {
        // 현재 상태 반환
        curCamMove = mainCam.stopMove;
        curChangeRotate = player.changeRotate;
        curDirectionRotate = player.directionRotate;

        // 카메라 무빙, 회전, 발사방향 변경 불가 상태로 변경
        mainCam.stopMove = true;

        if(curChangeRotate)
            player.changeRotate = false;
        
        if(curDirectionRotate)
            player.directionRotate = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 원래 상태로 변경
        mainCam.stopMove = curCamMove;
        player.changeRotate = curChangeRotate;
        player.directionRotate = curDirectionRotate;
    }
}
