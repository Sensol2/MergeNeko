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

            // 충돌한 다른 오브젝트의 MergeCat 컴포넌트를 가져옵니다.
            GameObject otherCat = coll.gameObject;
            Cat otherMergeCat = coll.gameObject.GetComponent<Cat>();

            if (otherMergeCat.level < 4)
            {
                isMerging = true;
                Vector3 startPos = this.gameObject.transform.position;
                Vector3 endPos = coll.gameObject.transform.position;

                // 두 오브젝트가 이동하는 동안 충돌을 무시합니다.
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
