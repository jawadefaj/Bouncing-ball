using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooterBehaviour : MonoBehaviour, IShootBall {

	public Transform shootingPoint;
	private GameObject ball;
	private List<GameObject> balls = new List<GameObject> (BallSpawner.ballList);
	private int curBallID;



	//overriding IshootBall functions
	public void setDestroyedID(int ballID)
	{
		curBallID = ballID;
	}

	public void shoot()
	{
		foreach(GameObject item in balls) {
			//iBall iball = item.gameObject.GetComponent<iBall> ();
			int type = ball.GetComponent<iBall>().type;
			if (type == curBallID) {
				//ball = Instantiate ();
				ball = Instantiate(item, shootingPoint.position, Quaternion.identity);


			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			shoot ();
		}
	}


}
