using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionEffect;

    public float pushForce = 10.0f; // �о�� ��
    public float radius = 5.0f;     // ��ź�� ȿ�� ����
    private bool exploded = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!exploded)
        {
            Debug.Log("���𰡿� �浹");
            exploded = true;
            StartCoroutine(ExplodeAfterDelay());
        }
    }

    private IEnumerator ExplodeAfterDelay()
    {
        SoundManager.instance.PlaySound(SoundType.BOMB);
        yield return new WaitForSeconds(3.0f);

        // ���� ����Ʈ ����
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        // ��ź�� ȿ�� ���� ���� ��� ������Ʈ�� ã���ϴ�.
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

        Destroy(gameObject); // ��ź ��ü�� ����
    }
}
