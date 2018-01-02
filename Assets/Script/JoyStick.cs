using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour,IDragHandler,IEndDragHandler
{
    public Image bgImg;
    Vector2 inputVector;
    bool setOn = true;

    float minA = 0f;
    float maxA = 0.2f;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform,eventData.position,eventData.pressEventCamera,out position))
        {
            //print(position);
            position.x = position.x / bgImg.rectTransform.sizeDelta.x*2;
            position.y = position.y / bgImg.rectTransform.sizeDelta.y*2;
            inputVector = position.normalized ;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        inputVector.x = 0f;
        inputVector.y = 0f;
    }
    
    public  Vector2 getJoyStickVector()
    {
        return (inputVector);
    }
    public void showMe()
    {
        //bgImg.color
        setOn = true;
    }
    public void hideMe()
    {
        print("setOff");
        setOn = false;
    }
    private void Update()
    {
        tweenColor();

    }

    void tweenColor()
    {
        float targetA;

        print(setOn);
        if (setOn)
        {
            targetA = Mathf.Clamp(bgImg.color.a + 0.03f, minA, maxA);
                
        }
        else
        {
            targetA = Mathf.Clamp(bgImg.color.a - 0.03f, minA, maxA);
            print(targetA);
        }

        bgImg.color = new Color(1, 1, 1, targetA);
    }
        
}
