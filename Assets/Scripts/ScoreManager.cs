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
    private int score;
    private int feverScore;

    // 피버타임 이벤트
    public delegate void FeverTimeEvent();
    public event FeverTimeEvent onFeverTimeStart;
    public event FeverTimeEvent onFeverTimeEnd;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        isFeverTime = false;
        
    }

    private void Update()
    {
        this.scoreText.text = this.score.ToString();

        if (feverScore >= feverNeed)
        {
            feverScore = 0;
            EnableFeverTime();
        }
    }

    public void AddScoreByLevel(int level)
    {
        if (level < scoreTable.Length)
        {
            this.score += scoreTable[level];
            this.feverScore += scoreTable[level];
        }
    }

    public void EnableFeverTime()
    {
        isFeverTime = true;
        onFeverTimeStart?.Invoke();
        feverTimeUI.FeverAnimation();
        StartCoroutine(FeverTimer(10.0f));
    }

    IEnumerator FeverTimer(float time)
    {
        yield return new WaitForSeconds(time);
        isFeverTime = false;
        onFeverTimeEnd?.Invoke();
    }

}
