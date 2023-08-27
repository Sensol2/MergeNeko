using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameOverLine : MonoBehaviour
{
    public GameItem item_catTower;
    public float fadeDuration = 1f; // 페이드 인/아웃에 걸리는 시간
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        IncreaseGameoverY();
        FadeInOut();
    }

    private void IncreaseGameoverY()
    {
        if (DataManager.instance.HasItem(item_catTower.KEY)) //캣타워 아이템 효과로 게임오버 상한선 늘리기
        {
            int level = DataManager.instance.GetItemLevel(item_catTower.KEY);
            float distance = item_catTower.effectValues[level];
            this.transform.Translate(new Vector3(0, distance, 0));
        }
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
