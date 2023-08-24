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
    public TMP_Text GainedGemText;

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
        float score;
        score = ScoreManager.instance.GetScore();

        ResultScore.text = score.ToString();
        TimeText.text = "TIME: " + TimeManager.instance.GetTimerText();
        MaxComboText.text = "COMBO: " + ComboManager.instance.maxCombo.ToString();
        MergedCatText.text = "MERGED CATS: " + Cat.mergedCats.ToString();

        int gem;
        gem = (int)(score * 0.01);
        GainedGemText.text = gem.ToString();
        DataManager.instance.SetGem(gem);

        blackScreen.DOFade(0.7f, 0.5f);
        transform.DOMove(targetPosition, duration).SetEase(Ease.OutElastic);
        GameOverZone.OnGameOver -= DisplayScoreBoard;
    }

}