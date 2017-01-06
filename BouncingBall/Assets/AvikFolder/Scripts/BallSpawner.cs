using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour, IBallSpawn {

	public List <GameObject> ballList = new List<GameObject>();
	private List <GameObject> curBallList = new List<GameObject>();
	private float speed = 2f;
	private float time = 0;
	IBallSpawn iball;

	enum ballType {
		normal,
		fire,
		freze,
		split,
		power
	};


	public void spawnBalls()
	{
		foreach (var ball in ballList) {
			
			curBallList.Add (ball);
		}

	}

	public void moveDown()
	{
		foreach (var item in curBallList) {
			item.transform.Translate (new Vector3(0,-1,0)*2);
		}
	}

	// Use this for initialization
	void Start () {
		iball = this.GetComponent<IBallSpawn> ();
		iball.spawnBalls ();
		iball.moveDown ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			iball.spawnBalls ();
			iball.moveDown ();
		}
	}



}

public interface IBallSpawn
{
	void spawnBalls ();	
	void moveDown ();
//	void setball (float x, float y);
}
