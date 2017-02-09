using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shooterBehaviour : MonoBehaviour, IShootBall {

	public Transform shootingPoint;
	public Transform lowerend;
	private GameObject ball;
	private List<GameObject> balls = new List<GameObject> ();
	private int curBallID = 1;
	private bool isShoot = true;
	public static int score = 0;
	public Text scoreText;
	//private bool rotatePoint = false;


	public bool isShootable{
		get{ 
			return isShoot;
		}
		set{ 
			isShoot = value;
		}
	}

	public void initializeList(List<GameObject> list)
	{
		foreach (GameObject ob in list) {
			balls.Add (ob);
		}
		isShootable = true;
	}
	//overriding IshootBall functions
	public void setDestroyedID(int ballID)
	{
		
		curBallID = ballID;
	//	print (curBallID);
	}

	public void shoot(Vector3 dir)
	{
//		print ("Direction " + dir);
		//print ("Shoot called");
		//print (balls.Count);
		foreach(GameObject item in balls) {
		//	print ("Shoot called");
			//iBall iball = item.gameObject.GetComponent<iBall> ();
			int type = item.GetComponent<iBall>().type;

			if (type == curBallID) {
				//print (curBallID);
				lowerend.up = dir;
				Vector3 curDir = -lowerend.position + shootingPoint.position;
				ball = Instantiate(item, shootingPoint.position, Quaternion.identity);
				ball.GetComponent<iBall> ().SetMoveDirection (curDir);
				ball.GetComponent<iBall> ().SetSpeed (0.08f);
				ball.GetComponent<iBall> ().isThrown = true;
				//print("cur dir "+curDir);
				//lowerend.transform.Rotate (new Vector3 (0.0f, 0.0f, 1.0f), angle);
				//lowerend.RotateAround (lowerend.transform.position, new Vector3(0.0f, 0.0f, 1.0f), angle);

			}
		}
	}

	IEnumerator rotatePoint(float angle, Quaternion q)
	{
		float step = angle / 10;
		for (int i = 0; i < 10; i++) {
			transform.rotation = Quaternion.Slerp (transform.rotation,q,step);
			yield return null;
		}
	}

	// Use this for initialization
	void Start () {
		curBallID = 1;// Random.Range (1,30) % 5 + 1;
	}



	// Update is called once per frame
	void Update () {

		scoreText.text = "Score:" + score;
		if (Input.GetMouseButtonDown (0) && isShootable) {
			
			Vector3 pos = Input.mousePosition;
			pos.z = -10;
			pos = Camera.main.ScreenToWorldPoint (pos);
			//print (-pos.x + " " +  -pos.y);
			if (-pos.y > -3.5f) {
				Vector3 direc = new Vector3 (-pos.x, -pos.y, 0);
				Vector3 dir = (direc - lowerend.position).normalized;
				//shoot (dir);
				shoot (direc - lowerend.position);
				isShootable = false;
			}
		}
	}


}
