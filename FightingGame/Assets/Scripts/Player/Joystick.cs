﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 지금 하려는 방식 자체에 의문을 갖는 중...
/// 조이스틱의 기준점을 이미지 크기로 잡는 게 아닌,
/// 데이터화하여 데이터만으로 연산하도록 변경하는 등
/// </summary>

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    Image imageBackgroud;
    Image imageController;

    Vector2 touchPos;

    void Awake()
    {
        imageBackgroud = GetComponent<Image>();
        imageController = transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
        // 임시
        Managers.Input.SetTouchPosition(touchPos);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        touchPos = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imageBackgroud.rectTransform, eventData.position, eventData.pressEventCamera, out touchPos))
        {
            touchPos.x = (touchPos.x / imageBackgroud.rectTransform.sizeDelta.x);
            touchPos.y = (touchPos.y / imageBackgroud.rectTransform.sizeDelta.y);

            touchPos = new Vector2(touchPos.x * 2 - 1, touchPos.y * 2 - 1);

            touchPos = (touchPos.magnitude > 1) ? touchPos.normalized : touchPos;

            imageController.rectTransform.anchoredPosition = new Vector2(
                touchPos.x * imageBackgroud.rectTransform.sizeDelta.x * 0.4f,
                touchPos.y * imageBackgroud.rectTransform.sizeDelta.y * 0.4f);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        // #0. 터치되는 이미지의 중심축(Pivot)을 기준으로 현재 터치 좌표가 중심축으로부터 얼마나 떨어져 있는지를 touchPosition에 저장
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imageBackgroud.rectTransform, eventData.position, eventData.pressEventCamera, out touchPos))
        {
            // #1. touchPosition 값의 정규화 (0 ~ 1)
            touchPos.x = (touchPos.x / imageBackgroud.rectTransform.sizeDelta.x);
            touchPos.y = (touchPos.y / imageBackgroud.rectTransform.sizeDelta.y);

            // #2. touchPosition 값의 정규화 (-n ~ n)
            touchPos = new Vector2(touchPos.x * 2 - 1, touchPos.y * 2 - 1);

            // #3. touchPosition 값의 정규화 (-1 ~ 1)
            touchPos = (touchPos.magnitude > 1) ? touchPos.normalized : touchPos;

            // #4. 컨트롤러 이미지 이동 가두기
            imageController.rectTransform.anchoredPosition = new Vector2(
                touchPos.x * imageBackgroud.rectTransform.sizeDelta.x * 0.3f,
                touchPos.y * imageBackgroud.rectTransform.sizeDelta.y * 0.3f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        imageController.rectTransform.anchoredPosition = Vector2.zero;
        touchPos = Vector2.zero;
    }

    public float Horizontal()
    {
        if (touchPos.x > 0.3f)
            touchPos.x = 1.0f;
        else if (touchPos.x < -0.3f)
            touchPos.x = -1.0f;
        else
            touchPos.x = 0.0f;

        return touchPos.x;
    }

    public float Vertical()
    {
        if (touchPos.y > 0.3f)
            touchPos.y = 1.0f;
        else if (touchPos.y < -0.3f)
            touchPos.y = -1.0f;
        else
            touchPos.y = 0.0f;

        return touchPos.y;
    }
}