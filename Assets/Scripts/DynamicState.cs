using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum pattern
{
    Horizontal,
    Vertical
}

/// <summary>
/// ¶¯Ì¬¸¡¶¯,ÆøÅÝ
/// </summary>
public class DynamicState : MonoBehaviour
{

    private float targetTime = 2f;
    private float speed = 5f;
    private float startTime = 0;

    private RectTransform rect;

    private float distance = 3.5f;

    public pattern currentPattern;

    public float XYvalue;

    public bool isMoving = false;
    public bool isStartTime = false;

    private void Awake()
    {
        rect = this.GetComponent<RectTransform>();
        if(currentPattern == pattern.Horizontal)
        {
            XYvalue = rect.anchoredPosition.x;
        }
        else if(currentPattern == pattern.Vertical)
        {
            XYvalue = rect.anchoredPosition.y;
        }
    }

    void Update()
    {
        if(currentPattern == pattern.Horizontal)
        {
            startTime += Time.deltaTime;
            if (startTime >= targetTime && !isStartTime)
            {
                isStartTime = true;
                isMoving = !isMoving;
            }
            if (isMoving)
            {
                rect.anchoredPosition = new Vector2(Mathf.Lerp(rect.anchoredPosition.x, XYvalue + distance, Time.deltaTime * speed), rect.anchoredPosition.y);
                if(Mathf.Abs(rect.anchoredPosition.x - (XYvalue + distance)) < 0.01f)
                {
                    isStartTime = false;
                    startTime = 0;
                }
            }
            else 
            {
                rect.anchoredPosition = new Vector2(Mathf.Lerp(rect.anchoredPosition.x, XYvalue - distance, Time.deltaTime * speed), rect.anchoredPosition.y);
                if (Mathf.Abs(rect.anchoredPosition.x - (XYvalue - distance)) < 0.01f)
                {
                    isStartTime = false;
                    startTime = 0;
                }
            }
        }
        else if(currentPattern == pattern.Vertical)
        {

        }
    }
}
