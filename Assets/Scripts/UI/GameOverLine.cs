using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameOverLine : MonoBehaviour
{
    public float fadeDuration = 1f; // 페이드 인/아웃에 걸리는 시간
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        FadeInOut();
    }

    private void FadeInOut()
    {
        // 페이드 아웃
        spriteRenderer.DOFade(0, fadeDuration).OnComplete(() =>
        {
            // 페이드 인
            spriteRenderer.DOFade(1, fadeDuration).OnComplete(FadeInOut);
        });
    }
}
