using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null){
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null){
			Debug.Log("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary") {
			return;
		}
		Instantiate (explosion, transform.position, transform.rotation);
		if (other.tag != "Player") {
			gameController.AddScore (scoreValue);
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
		if (other.tag == "Player") {
			Destroy(other.gameObject);
			//Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")){
				Destroy(enemy);
			}
			gameController.GameOver ();
			Application.LoadLevel(Application.loadedLevel);
		}

	}
}
