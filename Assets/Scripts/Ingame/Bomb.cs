using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionEffect;

    public float pushForce = 10.0f; // 밀어내는 힘
    public float radius = 5.0f;     // 폭탄의 효과 범위
    private bool exploded = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!exploded)
        {
            Debug.Log("무언가와 충돌");
            exploded = true;
            StartCoroutine(ExplodeAfterDelay());
        }
    }

    private IEnumerator ExplodeAfterDelay()
    {
        SoundManager.instance.PlaySound(SoundType.BOMB);
        yield return new WaitForSeconds(3.0f);

        // 폭발 이펙트 생성
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        // 폭탄의 효과 범위 내의 모든 오브젝트를 찾습니다.
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Cat"))
            {
                Vector2 explosionDirection = hit.transform.position - transform.position;
                float distance = explosionDirection.magnitude;

                Rigidbody2D rb = hit.gameObject.GetComponent<Rigidbody2D>();
                if (rb)
                {
                    rb.AddForce(explosionDirection.normalized * pushForce / (distance == 0 ? 1 : distance), ForceMode2D.Impulse);
                }
            }
        }

        Destroy(gameObject); // 폭탄 자체를 삭제
    }
}
