using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooterBehaviour : MonoBehaviour, IShootBall {

	public Transform shootingPoint;
	private GameObject ball;
	private List<GameObject> balls = new List<GameObject> ();
	private int curBallID = 1;
	//private bool rotatePoint = false;


	public void initializeList(List<GameObject> list)
	{
		foreach (GameObject ob in list) {
			balls.Add (ob);
		}
	}
	//overriding IshootBall functions
	public void setDestroyedID(int ballID)
	{
		curBallID = ballID;
	}

	public void shoot(Vector3 dir)
	{
		//print ("Shoot called");
		//print (balls.Count);
		foreach(GameObject item in balls) {
		//	print ("Shoot called");
			//iBall iball = item.gameObject.GetComponent<iBall> ();
			int type = item.GetComponent<iBall>().type;
			if (type == curBallID) {
				//ball = Instantiate ();
				//print("Entered");
				Vector3 targetDir = dir - transform.position;
				float angle = Mathf.Atan2 (targetDir.y,targetDir.x)*Mathf.Rad2Deg;
				Quaternion q = Quaternion.AngleAxis (angle,Vector3.forward);
				StartCoroutine (rotatePoint(angle,q));
				//transform.rotation = Quaternion.LookRotation (transform.rotation,q,step);
				//Vector3 curDir = new Vector3 (0,1,0);
				//Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir,10,0);
				
				//Vector3 zaxix = new Vector3(0,0,1);
				//this.transform.RotateAround (dir, zaxix, 20);
				//ball = Instantiate(item, shootingPoint.position, Quaternion.identity);
				//ball.GetComponent<iBall> ().SetMoveDirection (shootingPoint.position);


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
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			
			Vector3 pos = Input.mousePosition;
			pos.z = -10;
			pos = Camera.main.ScreenToWorldPoint (pos);
			print (pos.x + " " +  pos.y);
			Vector3 direc = new Vector3 (pos.x, pos.y, 0);
			shoot (direc);
		}
	}


}
