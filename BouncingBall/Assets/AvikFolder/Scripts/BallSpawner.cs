using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour, IBallSpawn {


	public List <GameObject> ballList = new List<GameObject>();
	public static List<GameObject> staticBallList = new List<GameObject> ();
	private List <GameObject> curBallList = new List<GameObject>();
	private float speed = 2f;
	private float time = 0;
	IBallSpawn iballSpawn;
	public Transform startpos, endpos;
	private GameObject b;
	public GameObject ballShooter;
	private GameObject tempBall;
	private int countUp = 0;
	private float timerInc = .1f;


	public void spawnBalls()
	{
		float inc = 0f;
		for (int i = 0; i < 5; i++) {
			Vector2 pos = new Vector2 (startpos.localPosition.x + inc, startpos.localPosition.y);
			int rand =  Random.Range (1,30) % 5 + 1;
			foreach (GameObject ball in ballList) {
				int type = ball.GetComponent<iBall> ().type;
				
				//print ("type " + type);
				//print ("rand " + rand);
				if (type == rand) 
				{
					//print (type);
					tempBall = ball;
					b = Instantiate (tempBall, new Vector3(20,20,20), Quaternion.identity);
					b.GetComponent<iBall> ().SetPosition (pos);
					curBallList.Add (b);
				}
			}
			inc += 1.545f;
		}
	}

	public void moveDown()
	{
		print ("total balls " + curBallList.Count);
		foreach (var item in curBallList) {
			if(item!=null)
				item.transform.Translate (new Vector3(0,-1,0)*1.6f);
		}
	}

	public void moveUp()
	{
		countUp++;
		print ("total balls " + curBallList.Count);
		foreach (var item in curBallList) {
			if(item!=null)
				item.transform.Translate (new Vector3(0,1,0)*1.6f);
		}
	}

	// Use this for initialization
	void Start () {
		ballShooter.GetComponent<IShootBall> ().initializeList (ballList);

		iballSpawn = this.GetComponent<IBallSpawn> ();
		iballSpawn.spawnBalls ();
		//iballSpawn.moveDown ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			
			iballSpawn.moveDown ();
			if(countUp==0)
				iballSpawn.spawnBalls ();
			else if(countUp>0)
				countUp--;
		} else if (Input.GetKeyDown (KeyCode.D)) {
			iballSpawn.moveUp ();
		}
		time += Time.deltaTime;
		if (time > 3f) {
			iballSpawn.moveDown ();
			if(countUp==0)
				iballSpawn.spawnBalls ();
			else if(countUp>0)
				countUp--;
			time = timerInc;
			timerInc += .1f;
		}
	}



}

public interface IBallSpawn
{
	void spawnBalls ();	
	void moveDown ();
	void moveUp ();

//	void setball (float x, float y);
}

public interface IShootBall
{
	void shoot (Vector3 dir);
	void setDestroyedID (int ballID);
	void initializeList (List<GameObject> list);
}
