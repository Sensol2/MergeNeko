using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum DropType
{
    CAT,
    BOMB,
    TUNABITZ,
}


public class CatController : MonoBehaviour
{
    static public CatController instance;

    GameObject newObject;
    public GameObject[] cats;
    public float dropSpeed;
    public float GravityScale;
    public float generateDelay;
    public float scaleDelay;

    //게임 아이템
    public GameItem bombItem;
    public GameItem tunaBitzItem;
    public GameObject bomb;
    public GameObject tuna;

    int bombSpawnRate;
    int tunaBitzSpawnRate;
    bool hasBombItem;
    bool hasTunaBitzItem;

    private void Awake()
	{
        if (instance == null)
            instance = this;
	}

	void Start()
    {
        InitItemValue();
        GenerateObject();
        GameOverZone.OnGameOver += OnDisableWhenGameOver;
    }
    
    void Update()
    {
        if (GameManager.instance.isOnSetting)
            return;
        

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            MoveObject();
        }
        if (Input.GetMouseButtonUp(0))
        {
            DropObject();
        }
    }

    void InitItemValue() //스폰 확률 데이터에서 받아오기
    {
        bombSpawnRate = DataManager.instance.GetItemLevel(bombItem.KEY);
        tunaBitzSpawnRate = DataManager.instance.GetItemLevel(tunaBitzItem.KEY);

        hasBombItem = DataManager.instance.HasItem(bombItem.KEY);
        hasTunaBitzItem = DataManager.instance.HasItem(tunaBitzItem.KEY);
    }

    void GenerateObject() //스폰 확률에 따라 생성
    {
        bombSpawnRate = hasBombItem ? bombSpawnRate : 0;
        tunaBitzSpawnRate = hasTunaBitzItem ? tunaBitzSpawnRate : 0;

        int randomValue = UnityEngine.Random.Range(0, 100);

        if (randomValue < bombSpawnRate && hasBombItem)
        {
            Generate(DropType.BOMB);
        }
        else if (randomValue < bombSpawnRate + tunaBitzSpawnRate && hasTunaBitzItem)
        {
            Debug.Log("TUNABITZ 생성");
            Generate(DropType.TUNABITZ);
        }
        else
        {
            Generate(DropType.CAT);
        }
    }

    void Generate(DropType dropType)
    {
        switch (dropType)
        {
            case DropType.CAT:
                int maxLevel = LevelManager.instance.GetLevel();

                if (maxLevel <= 4)
                    newObject = Instantiate(cats[Random.Range(0, maxLevel)], new Vector3(0, 4, 0), Quaternion.identity);
                else
                    newObject = Instantiate(cats[Random.Range(0, 4)], new Vector3(0, 4, 0), Quaternion.identity);

                Rigidbody2D catRigidbody = newObject.GetComponent<Rigidbody2D>();
                PhysicsController.instance.AppendCat(catRigidbody);
                break;
            case DropType.BOMB:
                newObject = Instantiate(bomb, new Vector3(0, 4, 0), Quaternion.identity);
                break;
            case DropType.TUNABITZ:
                newObject = Instantiate(tuna, new Vector3(0, 4, 0), Quaternion.identity);
                break;
        }


        // 생성될때 사이즈 커지게
        var originScale = newObject.transform.localScale;
        newObject.transform.localScale = new Vector3(0, 0, 0);
        newObject.transform.DOScale(originScale, scaleDelay);

        // 물리 OFF, 아직생성된게 아니므로
        newObject.GetComponent<CircleCollider2D>().enabled = false;
        newObject.GetComponent<Rigidbody2D>().isKinematic = true;


    }

    void MoveObject() 
    {
        if (newObject == null) 
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 newPos = new Vector3(mousePos.x, newObject.transform.position.y, 0);
        newObject.transform.position = newPos;
    }

    private void DropObject()
    {
        if (newObject == null)
            return;

        //마우스버튼을 떼면 gravity scale에 값을 넣어 아래로 떨어지게 한다.
        newObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -dropSpeed, 0);
        newObject.GetComponent<CircleCollider2D>().enabled = true;
        newObject.GetComponent<Rigidbody2D>().isKinematic = false;

        //newObject.GetComponent<Cat>().isNewCat = true;

        newObject = null;

        // 1초 후에 GenerateCat() 코루틴을 호출합니다.
        StartCoroutine(GenerateCatWithDelay(generateDelay));
    }

    IEnumerator GenerateCatWithDelay(float delay)
    {
        // 지정된 시간 동안 대기합니다.
        yield return new WaitForSeconds(delay);

        // 대기 시간이 끝난 후에 GenerateObject() 메서드를 호출합니다.
        GenerateObject();
    }

    public void OnDisableWhenGameOver()
    {
        GameOverZone.OnGameOver -= OnDisableWhenGameOver;
        gameObject.SetActive(false);
    }
}
