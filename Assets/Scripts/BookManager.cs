using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookManager : MonoBehaviour
{
    public AudioSource music;
    private bool isMusic = true;

    public GameObject title;
    public GameObject StartPanel;

    public GameObject ScrollPanel;
    private ScrollRect scrollRect;
    public HorizontalLayoutGroup content;
    private int pageCount = 0;

    public Button StartBtn;

    public Button PreviousBtn;

    public Button NextPageBtn;

    [Header("是否滚动")]
    public bool isRoll = false;

    [Header("当前页面下标")]
    public int currentIndex = 0;
    [Header("当前页面滚动的阈值")]
    public float VelocityThreshold = 6f;
    [Header("当前页面的滚动目标值")]
    public float targetValue = 0;

    private void Awake()
    {
        music.gameObject.SetActive(isMusic);
        title.SetActive(true);
        StartPanel.SetActive(true);
        ScrollPanel.SetActive(false);
        PreviousBtn.gameObject.SetActive(false);
        NextPageBtn.gameObject.SetActive(false);

        scrollRect = ScrollPanel.GetComponent<ScrollRect>();
    }

    private void Start()
    {
        pageCount = content.transform.childCount - 1;
    }

    private void FixedUpdate()
    {
        if (isRoll)
        {
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, targetValue, Time.deltaTime * VelocityThreshold);
            //print(1 * (1 / (float)8));
            //TODO 判断是否距离目标点的距离小于0.01,如果是的话,直接跳转目标值即可

            if(Mathf.Abs(scrollRect.horizontalNormalizedPosition - targetValue) < 0.001f)
            {
                scrollRect.horizontalNormalizedPosition = targetValue;
                isRoll = false;
            }
        }
    }

    /// <summary>
    /// 获取当前滑动值
    /// </summary>
    /// <param name="PS">是否为正序</param>
    /// <returns></returns>
    private float GetCurrentRollValue(bool PS = true)
    {
        NextPageBtn.gameObject.SetActive(true);
        PreviousBtn.gameObject.SetActive(true);
        if (PS)
        {
            currentIndex++;
            if (currentIndex >= pageCount)
            {
                currentIndex = pageCount;
                NextPageBtn.gameObject.SetActive(false);
            }
        }
        else
        {
            currentIndex--;
            if (currentIndex <= 0)
            {
                currentIndex = 0;
                PreviousBtn.gameObject.SetActive(false);
            }

        }

        return currentIndex * (1 / (float)pageCount);
    }

    /// <summary>
    /// 上一页
    /// </summary>
    public void PreviousBtnMethod()
    {
        isRoll = true;
        targetValue = GetCurrentRollValue(false);
        print(targetValue);
    }

    /// <summary>
    /// 下一页
    /// </summary>
    public void NextPageBtnMethod()
    {
        isRoll = true;
        targetValue = GetCurrentRollValue();
        print(targetValue);
    }

    /// <summary>
    /// 开始
    /// </summary>
    public void StartBtnMethod()
    {
        ScrollPanel.SetActive(true);
        StartPanel.SetActive(false);
        title.SetActive(false);
        NextPageBtn.gameObject.SetActive(true);
    }

    public void MusicMethod()
    {
        isMusic = !isMusic;
        music.gameObject.SetActive(isMusic);
    }
}
