using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooterBehaviour : MonoBehaviour, IShootBall {

	public Transform shootingPoint;
	private GameObject ball;
	private List<GameObject> balls = new List<GameObject> (BallSpawner.ballList);


	//overriding IshootBall functions
	public void shoot(int ballID)
	{
		foreach(GameObject item in balls) {
			//iBall iball = item.gameObject.GetComponent<iBall> ();
			int type = ball.GetComponent<iBall>().type;
			if (type == ballID) {
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
		
	}


}
