//ScoreManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    static public ScoreManager instance;
    public TextMeshProUGUI scoreText;
    public FeverTimeUI feverTimeUI;

    public int[] scoreTable = { 5, 10, 30, 50, 100, 150, 300, 500, 1000, 3000 };
    public int feverNeed;
    public bool isFeverTime;
    public float feverTimeDuration;
    private int score;
    private int feverScore;

    // 피버타임 이벤트
    public GameItem item_box;
    public delegate void FeverTimeEvent();
    public static event FeverTimeEvent onFeverTimeStart;
    public static event FeverTimeEvent onFeverTimeEnd;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        Debug.Log("게임시작");
        SetFevertimeDuration();
        GameOverZone.OnGameOver += DisableFeverTime;
    }

    void Update()
    {
        this.scoreText.text = this.score.ToString();

        if (feverScore >= feverNeed)
        {
            feverScore = 0;
            feverNeed += 5000;
            EnableFeverTime();
        }
    }

    public void AddScore(int value)
    {
        this.score += value;
    }

    public void AddScoreByLevel(int level)
    {
        if (level < scoreTable.Length)
        {
            this.score += scoreTable[level];
            this.feverScore += scoreTable[level];
        }
    }

    public int GetScore()
    {
        return this.score;
    }

    // ==========
    // 피버타임 관련
    public void SetFevertimeDuration()
    {
        if (DataManager.instance.HasItem(item_box.KEY)) //Box 아이템 효과로 피버타임 지속시간 증가
        {
            int level = DataManager.instance.GetItemLevel(item_box.KEY);
            float bonusSecond = item_box.effectValues[level];
            feverTimeDuration += bonusSecond;
        }
    }

    public void EnableFeverTime()
    {
        isFeverTime = true;
        onFeverTimeStart?.Invoke();

        StartCoroutine(FeverTimer(feverTimeDuration));
    }

    IEnumerator FeverTimer(float time)
    {
        yield return new WaitForSeconds(time);
        isFeverTime = false;
        onFeverTimeEnd?.Invoke();
    }

    void DisableFeverTime()
    {
        GameOverZone.OnGameOver -= DisableFeverTime;
        this.gameObject.SetActive(false);
    }

    public void ResetFeverEvent()
    {
        onFeverTimeStart = null;
        onFeverTimeEnd = null;
    }

}
