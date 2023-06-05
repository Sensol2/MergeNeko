using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ScoreBoard : MonoBehaviour
{

    public Image blackScreen;
    public TMP_Text ResultScore;
    public TMP_Text TimeText;
    public TMP_Text MaxComboText;
    public TMP_Text MergedCatText;

    [SerializeField] private float duration = 1f; // Animation duration
    [SerializeField] private Vector3 targetPosition; // The position where the banner will move to
    [SerializeField] private float delay = 2f; // Delay before the banner starts moving back

    private Vector3 initialPosition; // Initial position of the banner

    void Start()
    {
        initialPosition = transform.position;
        GameOverZone.OnGameOver += DisplayScoreBoard;
    }

    //todo : ´Ý±â ½Ã GameOverZone.OnGameOver -= DisplayScoreBoard;
    public void DisplayScoreBoard()
    {
        ResultScore.text = ScoreManager.instance.GetScore().ToString();
        TimeText.text = "TIME: " + TimeManager.instance.GetTimerText();
        MaxComboText.text = "COMBO: " + ComboManager.instance.maxCombo.ToString();
        MergedCatText.text = "MERGED CATS: " + Cat.mergedCats.ToString();

        blackScreen.DOFade(0.7f, 0.5f);
        transform.DOMove(targetPosition, duration).SetEase(Ease.OutElastic);
        GameOverZone.OnGameOver -= DisplayScoreBoard;
    }

}