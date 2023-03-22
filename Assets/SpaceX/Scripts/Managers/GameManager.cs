using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
	[Header("UI Settings")]
	public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI minerText;
    public GameObject instructionPanel;

	public static GameManager Instance { get; private set; }

	private int mCurrentScore;

    [Header("Game Settings")]
    public GameObject player;
    public PoolData shipData;

    public int numOfAsteroid = 100;
    public int numOfMiner = 3;
	public int numOfEnemy = 5;

	private int mFoundMiner;

    [Header("Spawner Settings")]
    public MinerSpawner minerSpawner;
	public AsteroidSpawner asteroidSpawner;
	public EnemySpawner enemySpawner;

    private void Awake()
    {
		Instance = this;
		mCurrentScore = 0;
		scoreText.text = "score: " + mCurrentScore;
	}

    // Use this for initialization
    void Start()
	{
		asteroidSpawner.SpawnAsteroid(numOfAsteroid);

		numOfMiner = 3;
		minerSpawner.SpawnMiner(numOfMiner);
		UpdateMinerText();

		SpawnPlayer();

		enemySpawner.player = player;
		enemySpawner.SpawnEnemy(numOfEnemy);

		StartCoroutine(HideInstruction());
		Time.timeScale = 0;
    }

	void SpawnPlayer()
	{
        var idx = GameHelper.LoadShipSelection();
        var ship = Instantiate(shipData.prefabs[idx], player.transform);
        var shipSettings = ship.GetComponent<ShipSettings>();
        ShootSystem.Instance.shipSettings = shipSettings;
    }

	IEnumerator HideInstruction()
    {
		yield return new WaitForSecondsRealtime(2.5f);
		instructionPanel.SetActive(false);

		Time.timeScale = 1;
    }

	public void BackToMenu()
    {
		SceneManager.LoadScene("Menu");
    }

	public void AddScore(int score)
    {
		mCurrentScore += score;
		UpdateScoreText();
    }

	void UpdateScoreText()
	{
        scoreText.text = "score: " + mCurrentScore;
    }

	public void CollectMiner(Miner miner)
	{
		mFoundMiner++;
		if (mFoundMiner == numOfMiner)
		{
			//mNumOfMiner++;
			mFoundMiner = 0;
			minerSpawner.SpawnMiner(numOfMiner);

			AddScore(1000);
		}
		UpdateMinerText();
	}

	void UpdateMinerText()
	{
        minerText.text = "miner: " + mFoundMiner + " / " + numOfMiner;
    }

	public void ScoreEnemy()
	{
		numOfEnemy--;
		AddScore(500);
	}

	public void CollideEnemy(Enemy enemy)
	{

	}
}

