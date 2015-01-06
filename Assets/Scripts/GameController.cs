using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject[] hazards;
	private GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText recordText;

	private bool gameOver;
	private bool restart;
	private int score;
	private static int record;

	void Start (){
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update (){
		if (restart && Input.GetKeyDown( KeyCode.R )) {
			Application.LoadLevel(Application.loadedLevel);	
		}
	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds(startWait);
		while (true){
			for (int i = 0; i < hazardCount; i++){
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x,spawnValues.x),spawnValues.y,spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Debug.Log ( Random.Range (0, hazards.Length).ToString() );
				hazard = hazards[Random.Range (0, hazards.Length)];
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);

			if (gameOver){
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore (int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
		}

	void UpdateScore(){
		if (score > record) {
			record = score;		
		}
		recordText.text = record.ToString();
		scoreText.text = score.ToString();
	}

	public void GameOver (){
		gameOver = true;
	}
}
