using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour, IBallSpawn {

	public List <GameObject> ballList = new List<GameObject>();
	private List <GameObject> curBallList = new List<GameObject>();
	private float speed = 2f;
	private float time = 0;
	IBallSpawn iballSpawn;
	public Transform startpos, endpos;
	private GameObject b;

//	enum ballType {
//		normal,
//		fire,
//		freze,
//		split,
//		power
//	};


	public void spawnBalls()
	{
		float inc = 0f;
		for (int i = 0; i < 8; i++) {
			Vector2 pos = new Vector2 (startpos.localPosition.x + inc, startpos.localPosition.y);
			int rand = Random.Range (1,30) % 5 + 1;
			foreach (GameObject ball in ballList) {
				int type = ball.GetComponent<iBall> ().type;
				
				//print ("type " + type);
				//print ("rand " + rand);
				if (type == rand) 
				{
					print (type);
					b = Instantiate (ball, new Vector3(20,20,20), Quaternion.identity);
					b.GetComponent<iBall> ().SetPosition (pos);
					curBallList.Add (b);
				}
			}
			inc += 1.9f;
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
		iballSpawn = this.GetComponent<IBallSpawn> ();
		iballSpawn.spawnBalls ();
		iballSpawn.moveDown ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			iballSpawn.spawnBalls ();
			iballSpawn.moveDown ();
		}
	}



}

public interface IBallSpawn
{
	void spawnBalls ();	
	void moveDown ();
//	void setball (float x, float y);
}
