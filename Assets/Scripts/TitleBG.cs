using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBG : MonoBehaviour
{
    public float fadeOutDuration = 3f;
    public float destroyDelay = 3f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOutAndDestroyCoroutine());
    }

    IEnumerator FadeOutAndDestroyCoroutine()
    {
        float elapsedTime = 0f;
        Color initialColor = spriteRenderer.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0);

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeOutDuration);
            yield return null;
        }

        spriteRenderer.color = targetColor;
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}