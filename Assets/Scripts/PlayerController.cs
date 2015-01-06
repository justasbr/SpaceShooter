using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed;
	public float horizontalTilt;
	public float verticalTilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

	void Update () 
	{
		if (Input.GetKeyDown("space") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			//Debug.Log (shotSpawn.rotation.x);
			//Fixed a bug where bullet disappears if ship is tilted vertically
			shotSpawn.rotation = Quaternion.Euler (0.0f, shotSpawn.rotation.y, shotSpawn.rotation.z);
			Debug.Log (shotSpawn.rotation.x);
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // as GameObject;
			audio.Play ();
		}
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;

		//If rocket goes out of bounds
		rigidbody.position = new Vector3 
		(	
			Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax),
			0.0f, 
			Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
		);

		//Rocket rotation while moving
		rigidbody.rotation = Quaternion.Euler 
		(
			rigidbody.velocity.z * -verticalTilt, 
			0.0f, 
			rigidbody.velocity.x * -horizontalTilt
		);
	}
}
