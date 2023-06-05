using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cat : MonoBehaviour
{
	static public int mergedCats = 0;
	[SerializeField]
	public GameObject[] cats;
	public float GRAVITY_SCALE;
	public int level = 0;
	public int comboCounter = 0;

	public bool isNewCat;
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
		StartCoroutine(ResetComboAfterSec(3.0f));
		GameOverZone.OnGameOver += DestroySelf;
	}

	private void Update()
	{
		if (transform.position.y < -20.0f)
			Destroy(gameObject);
	}

	private IEnumerator ResetComboAfterSec(float sec)
	{
		yield return new WaitForSeconds(sec);

		this.comboCounter = 0;
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll == null)
			return;

		if (level + 1 >= cats.Length) // 이미 최고레벨일 경우, 인덱스 초과를 방지하기 위해 return
			return;

		// 무언가에 닿으면 중력 정상화
		if (isNewCat && !ScoreManager.instance.isFeverTime)
		{
			isNewCat = false;
			SoundManager.instance.PlayPitSound();
			rigid.gravityScale = CatController.instance.GravityScale;
		}
			

		if (ScoreManager.instance.isFeverTime) // 피버타임 때 무언가랑 닿으면
			ScoreManager.instance.AddScore(1);


		if (coll.gameObject.CompareTag("Cat"))
		{
			if (isMerging)
				return;

			// 충돌한 다른 오브젝트의 MergeCat 컴포넌트를 가져옵니다.
			GameObject otherCat = coll.gameObject;
			Cat otherMergeCat = coll.gameObject.GetComponent<Cat>();

			// id 높은 경우(뒤늦게 만들어진 냥이) 우선 처리
			if (id > otherMergeCat.id)
			{
				// 같은 레벨의 고양이끼리 합치기
				if (level == otherMergeCat.level)
				{
					isMerging = true;
					Vector3 startPos = this.gameObject.transform.position;
					Vector3 endPos = coll.gameObject.transform.position;

					// 두 오브젝트가 이동하는 동안 충돌을 무시합니다.
					this.GetComponent<Collider2D>().enabled = false;

					this.gameObject.transform.DOMove(endPos, 0.1f).OnComplete(() => 
					{
						DestroyCat(gameObject);
						DestroyCat(otherCat.gameObject);

						// 다음 레벨의 고양이 프리팹을 중간 위치에 생성합니다.
						GameObject spawnedCat = Instantiate(cats[level + 1], endPos, Quaternion.identity);
						spawnedCat.GetComponent<Rigidbody2D>().gravityScale = CatController.instance.GravityScale;

						//스폰된 고양이 콤보 +1
						spawnedCat.GetComponent<Cat>().comboCounter = this.comboCounter + 1;
						ComboManager.instance.MakeCombo(this.comboCounter + 1, spawnedCat.transform.position);

						// 생성될때 사이즈 커지게
						var originScale = spawnedCat.transform.localScale;
						spawnedCat.transform.localScale = new Vector3(0, 0, 0);
						spawnedCat.transform.DOScale(originScale + new Vector3(0.01f, 0.01f, 0.01f), 0.1f).OnComplete(() => {
							spawnedCat.transform.DOScale(originScale, 0.1f);
						});

						// 합친 고양이 개수 +1 (전역)
						mergedCats += 1;

					});
				}
			}
		}
	}
	private void DestroySelf()
	{
		DestroyCat(gameObject);
	}

	public void DestroyCat(GameObject obj)
	{
		// 이펙트 생성
		VFXManager.instance.GenerateEffect(gameObject.transform.position);
		SoundManager.instance.PlayMergeSound();

		// 최대 레벨 갱신
		LevelManager.instance.UpdateLevel(level + 1);

		// 점수 올리기
		ScoreManager.instance.AddScoreByLevel(level + 1);
		Destroy(obj);
	}

	// Don't forget to unregister the event when the object is destroyed.
	private void OnDestroy()
	{
		GameOverZone.OnGameOver -= DestroySelf;
	}
}
