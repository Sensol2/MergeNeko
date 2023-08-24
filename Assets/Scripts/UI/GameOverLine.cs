using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameOverLine : MonoBehaviour
{
    public float fadeDuration = 1f; // ���̵� ��/�ƿ��� �ɸ��� �ð�
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
        // ���̵� �ƿ�
        spriteRenderer.DOFade(0, fadeDuration).OnComplete(() =>
        {
            // ���̵� ��
            spriteRenderer.DOFade(1, fadeDuration).OnComplete(FadeInOut);
        });
    }
}
