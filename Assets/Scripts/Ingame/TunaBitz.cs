using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunaBitz : MonoBehaviour
{
    public bool isMerging;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll == null)
            return;

        if (coll.gameObject.CompareTag("Cat"))
        {
            if (isMerging)
                return;

            // �浹�� �ٸ� ������Ʈ�� MergeCat ������Ʈ�� �����ɴϴ�.
            GameObject otherCat = coll.gameObject;
            Cat otherMergeCat = coll.gameObject.GetComponent<Cat>();

            if (otherMergeCat.level < 4)
            {
                isMerging = true;
                Vector3 startPos = this.gameObject.transform.position;
                Vector3 endPos = coll.gameObject.transform.position;

                // �� ������Ʈ�� �̵��ϴ� ���� �浹�� �����մϴ�.
                this.GetComponent<Collider2D>().enabled = false;

                this.gameObject.transform.DOMove(endPos, 0.1f).OnComplete(() =>
                {
                    Destroy(gameObject);
                    otherMergeCat.DestroyCat(otherCat);

                    otherMergeCat.SpawnCat(endPos);
                });
            }

        }
    }
}
