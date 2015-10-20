using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public BoardManager boardScript;
	public static GameManager instance = null;
	public int level = 3;
	public int playerFoodPoints = 100;
	[HideInInspector] public bool playersTurn = true;

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		}else if (instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad (gameObject);		
		boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}

	void InitGame()
	{
		boardScript.SetupScene (level);

	}

	public void GameOver(){
		enabled = false;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
