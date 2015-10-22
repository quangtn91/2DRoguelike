﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public float turnDelay = .1f;
	public BoardManager boardScript;
	public static GameManager instance = null;
	public int playerFoodPoints = 100;
	[HideInInspector] public bool playersTurn = true;

	private int level = 1;
	private List<Enemy> enemies;
	private bool enemyMoving;

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		}else if (instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad (gameObject);		
		enemies = new List<Enemy> ();
		boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}

	void OnLevelWasLoaded(int index)
	{
		//Add one to our level number.
		level++;
		//Call InitGame to initialize our level.
		InitGame();
	}

	void InitGame()
	{
		enemies.Clear ();
		boardScript.SetupScene (level);
	}

	public void GameOver(){
		enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if (playersTurn || enemyMoving)
			return;
		StartCoroutine("MoveEnemies");
	}

	public void AddEnemyToList(Enemy script){
		enemies.Add (script);
	}

	IEnumerator MoveEnemies(){
		enemyMoving = true;
		yield return new WaitForSeconds(turnDelay);
		if (enemies.Count == 0) {
			yield return new WaitForSeconds(turnDelay);
		}

		for (int i = 0; i < enemies.Count; i++) {
			enemies[i].MoveEnemy();
			yield return new WaitForSeconds(enemies[i].moveTime);
		}

		playersTurn = true;
		enemyMoving = false;
	}
}
