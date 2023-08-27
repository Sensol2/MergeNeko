using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;

public class ScoreBoard : MonoBehaviour
{

    public Image blackScreen;
    public TMP_Text resultScore;
    public TMP_Text timeText;
    public TMP_Text maxComboText;
    public TMP_Text mergedCatText;
    public TMP_Text gainedGemText;

    public GameItem item_yarn;
    public GameItem item_toyrat;
    public GameObject info_yarn;
    public GameObject info_toyrat;


    [SerializeField] private float duration = 1f; // Animation duration
    [SerializeField] private Vector3 targetPosition; // The position where the banner will move to
    [SerializeField] private float delay = 2f; // Delay before the banner starts moving back

    private Vector3 initialPosition; // Initial position of the banner

    void Start()
    {
        initialPosition = transform.position;
        GameOverZone.OnGameOver += DisplayScoreBoard;
    }

    //todo : 닫기 시 GameOverZone.OnGameOver -= DisplayScoreBoard;
    public void DisplayScoreBoard()
    {
        //점수 계산
        CalculateScore();

        //Gem 계산
        CalculateGem();

        //기타 정보
        timeText.text = "TIME: " + TimeManager.instance.GetTimerText();
        maxComboText.text = "COMBO: " + ComboManager.instance.maxCombo.ToString();
        mergedCatText.text = "MERGED CATS: " + Cat.mergedCats.ToString();

        blackScreen.DOFade(0.7f, 0.5f);
        transform.DOMove(targetPosition, duration).SetEase(Ease.OutElastic);
        GameOverZone.OnGameOver -= DisplayScoreBoard;
    }

    private void CalculateScore()
    {
        GameItem item = item_yarn;
        float score = ScoreManager.instance.GetScore();
        if (DataManager.instance.HasItem(item.KEY))
        {
            int level = DataManager.instance.GetItemLevel(item.KEY);
            int bonusScore = (int)(score * item.effectValues[item.level]/100);
            ScoreManager.instance.AddScore(bonusScore);

            info_yarn.SetActive(true);
            info_yarn.transform.Find("Description").GetComponent<TMP_Text>().text = $"Bonus score: +{bonusScore}";
        }
        resultScore.text = score.ToString();
    }

    private void CalculateGem()
    {
        GameItem item = item_toyrat;
        float score = ScoreManager.instance.GetScore();
        int gem = (int)(score * 0.01);
        int bonusGem = 0;
        if (DataManager.instance.HasItem(item.KEY))
        {
            int level = DataManager.instance.GetItemLevel(item.KEY);
            bonusGem = (int)(gem * item.effectValues[item.level]);

            info_toyrat.SetActive(true);
            info_toyrat.transform.Find("Description").GetComponent<TMP_Text>().text = $"Bonus Gem: +{bonusGem}";
        }
        DataManager.instance.SetGem(gem + bonusGem);
        gainedGemText.text = gem.ToString();
    }
}