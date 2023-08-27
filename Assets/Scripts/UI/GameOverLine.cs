using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameOverLine : MonoBehaviour
{
    public GameItem item_catTower;
    public float fadeDuration = 1f; // ���̵� ��/�ƿ��� �ɸ��� �ð�
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
        if (DataManager.instance.HasItem(item_catTower.KEY)) //ĹŸ�� ������ ȿ���� ���ӿ��� ���Ѽ� �ø���
        {
            int level = DataManager.instance.GetItemLevel(item_catTower.KEY);
            float distance = item_catTower.effectValues[level];
            this.transform.Translate(new Vector3(0, distance, 0));
        }
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
