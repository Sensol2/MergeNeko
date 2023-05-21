using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MergeCat : MonoBehaviour
{
	[SerializeField]
	public GameObject[] cats;
	public float GRAVITY_SCALE;
	public int level = 0;


	public bool isMerging;
	Rigidbody2D rigid;
	

	// 고유한 ID를 위한 정적 변수와 프로퍼티
	private static int nextId = 0;
	public int id;

	private void Awake()
	{
		id = nextId;
		nextId++;
	}
	private void Start()
	{
		rigid = GetComponent<Rigidbody2D>();
		isMerging = false;
	}

	private void Update()
	{
		if (transform.position.y < -20.0f)
			Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll == null)
			return;

		if (level + 1 >= cats.Length) // 이미 최고레벨일 경우, 인덱스 초과를 방지하기 위해 return
			return;

		// 무언가에 닿으면 중력 정상화
		if (!ScoreManager.instance.isFeverTime)
			rigid.gravityScale = CatController.instance.GravityScale;

		if (coll.gameObject.CompareTag("Cat"))
		{
			// 충돌한 다른 오브젝트의 MergeCat 컴포넌트를 가져옵니다.
			MergeCat otherMergeCat = coll.gameObject.GetComponent<MergeCat>();

			// id 높은 경우(뒤늦게 만들어진 냥이) 우선 처리
			if (id > otherMergeCat.id)
			{
				// 같은 레벨의 고양이끼리 합치기
				if (level == otherMergeCat.level)
				{
					Vector3 startPos = this.gameObject.transform.position;
					Vector3 endPos = coll.gameObject.transform.position;

					// 두 오브젝트가 이동하는 동안 충돌을 무시합니다.
					this.GetComponent<Collider2D>().enabled = false;

					this.gameObject.transform.DOMove(endPos, 0.1f).OnComplete(() => 
					{
						Debug.Log(coll.gameObject.name);
						Debug.Log(gameObject.name);


						//Destroy(gameObject);
						//// 두 오브젝트를 삭제합니다.
						Destroy(coll.gameObject);

					});
					//StartCoroutine(MergeCats(coll.gameObject));
					//SoundManager.instance.PlaySound();
				}
			}
		}
	}

	IEnumerator MergeCats(GameObject other)
	{
		if (isMerging)
			yield break;

		isMerging = true;
		yield return null;  // 한 프레임 기다립니다.

		// 두 오브젝트의 중간 위치를 계산하기 위해
		Vector3 targetPos = other.transform.position;

		if (ScoreManager.instance.isFeverTime)
		{
			// 두 오브젝트를 삭제합니다.
			Destroy(other.gameObject);
			Destroy(gameObject);

			// 이펙트 생성
			VFXManager.instance.GenerateEffect(targetPos);
			yield break;
		}

		yield return StartCoroutine(MergeMove(other));


		// 두 오브젝트를 삭제합니다.
		Destroy(other.gameObject);
		Destroy(gameObject);

		// 이펙트 생성
		VFXManager.instance.GenerateEffect(targetPos);

		// 다음 레벨의 고양이 프리팹을 중간 위치에 생성합니다.
		GameObject spawnedCat = Instantiate(cats[level + 1], targetPos, Quaternion.identity);
		spawnedCat.GetComponent<Rigidbody2D>().gravityScale = CatController.instance.GravityScale;

		// 최대 레벨 갱신
		int spawnedCatLevel = spawnedCat.GetComponent<MergeCat>().level;
		LevelManager.instance.UpdateLevel(spawnedCatLevel);

		// 점수 올리기
		ScoreManager.instance.AddScoreByLevel(spawnedCatLevel);

		isMerging = false;
	}

	IEnumerator MergeMove(GameObject other)
	{
		// 두 오브젝트의 중간 위치를 계산하기 위해
		Vector3 originPos = other.transform.position;
		Vector3 targetPos = other.transform.position;

		// 두 오브젝트가 이동하는 동안 충돌을 무시합니다.
		GetComponent<Collider2D>().enabled = false;

		// 두 오브젝트의 중력을 일시적으로 비활성화합니다.
		Rigidbody2D rb1 = GetComponent<Rigidbody2D>();
		Rigidbody2D rb2 = other.GetComponent<Rigidbody2D>();
		rb1.gravityScale = 0;
		rb2.gravityScale = 0;

		float duration = 0.1f; // 이동에 걸리는 시간 (초)
		float elapsedTime = 0.0f;

		// 리팩토링 필요
		while (elapsedTime < duration)
		{
			try
			{
				targetPos = other.transform.position;
				transform.position = Vector3.Lerp(transform.position, targetPos, elapsedTime / duration);

				elapsedTime += Time.deltaTime;
			}
			catch
			{
				targetPos = originPos;
				transform.position = Vector3.Lerp(transform.position, targetPos, elapsedTime / duration);
				elapsedTime += Time.deltaTime;
				Debug.LogError("Collision Error");
			}
			yield return null;
		}



	}
}
