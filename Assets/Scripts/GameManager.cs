using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStatus
{
    next, play, gameOver, win
}

public class GameManager : Singleton<GameManager>
{
    #region Variables
   //public static GameManager instance = null;

    [SerializeField]
    private GameObject spwanPoint;

    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private int maxEnemiesOnScreen, totalEnemies, enemiesPerSpwan;
    private int enemiesOnScreen = 0;

    const float EnemySpawnDelay = 1.5f;

    public List<Enemy> EnemyList = new List<Enemy> ();

    #region UI Varibles
    [SerializeField]
    private int totalWaves = 10;

    [SerializeField]
    private Text totalMoneyLabel, currentWaveLabel, playButtonLabel;

    [SerializeField]
    private Button playButton;

    private int waveNumber = 0, totalMoney = 10, totalEscaped = 0, totalEscapedRound = 0, totalKilled = 0, whichEnemyToSpwan = 0;

    private GameStatus currentState = GameStatus.play;
    #endregion

    #endregion

    #region Main Methods
    /*
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    */

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(SpwanEnemies());
	}
	
	// Update is called once per frame
	void Update ()
    {
		Escapes ();
    }
    #endregion

    #region Unity Utilities
    void SpwanEnemy ()
    {
        if(enemiesPerSpwan > 0 && enemiesOnScreen < totalEnemies)
        {
            for(int i =0; i < enemiesPerSpwan; i++)
            {
                    GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
                    newEnemy.transform.position = spwanPoint.transform.position;
                    enemiesOnScreen++;                
            }
        }
    }

    //Spwan Enemies in ways
    IEnumerator SpwanEnemies()
    {
        if (enemiesPerSpwan > 0 && EnemyList.Count < totalEnemies)
        {
           for(int i = 0; i < enemiesPerSpwan; i++)
            {
                if(EnemyList.Count < maxEnemiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
                    newEnemy.transform.position = spwanPoint.transform.position;
                }
                //enemiesOnScreen++;
            }
        }

        yield return new WaitForSeconds(EnemySpawnDelay);
        StartCoroutine(SpwanEnemies());
    }


    public void RegisterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }

    public void UnRegiesterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void DestoryAllEnemies()
    {
        foreach(Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }

        EnemyList.Clear();
    }

	public void Escapes ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			TowerManager.Instance.disableDragSprite ();
			TowerManager.Instance.towerButtonPressed = null;
		}
	}
    
    /*
    public void RemoveEnemiesOnScreen ()
    {
        if(enemiesOnScreen > 0)
        {           
            enemiesOnScreen--;
        }
    }
    */
    #endregion
}
