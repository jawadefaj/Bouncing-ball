﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour, iBall {

	private int id = 4;
	private Vector2 position;
	public Vector3 movedirection;
	private float movespeed = 0.06f;
	public bool Thrown = false;
	public float blustdistance = 2f;
	private List<GameObject> activeBallList = new List<GameObject> (BallSpawner.curBallList);


	public void SetMoveDirection(Vector3 dir){
		movedirection.x = dir.x;
		movedirection.y = dir.y;
		movedirection.z = this.transform.position.z;
	}
	public int type{
		get{ 
			return id;
		}
	}
	public bool isThrown{
		get{ 
			return Thrown;
		}
		set{ 
			Thrown = value;
		}
	}

	public void ScoreUpdate(int s){
		shooterBehaviour.score += s;
	}

	public void SetPosition(Vector2 pos){
		///print ("called");
		position = pos;
		this.GetComponent<Transform>().localPosition = position;

	}
	public void SetSpeed(float speed){
		movespeed = speed;
	}

	public void Destroy(){
		BallSpawner.curBallList.Remove (this.gameObject);
		Destroy (this.gameObject);
		foreach (GameObject ob in Transform.FindObjectsOfType<GameObject>()) {
			IShootBall ishootball = ob.GetComponent<IShootBall> ();
			if (ishootball != null)
				ishootball.isShootable = true;

		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.position = Vector2.MoveTowards (this.gameObject.transform.position, this.gameObject.transform.position + movedirection, movespeed);
	}

	void OnTriggerEnter2D(Collider2D other){
		iBall i = other.GetComponent<iBall>();
		if (this.Thrown == true)
		{
		//	print ("inside if");
//			if (i != null)
//			{
//				RaycastHit2D rr1 = Physics2D.Raycast (other.transform.position + new Vector3(0.0f, 0.8f, 0.0f), new Vector2 (0.0f, 1.0f), blustdistance);
//				//print ("rr1 "+rr1.collider.name);
//				if(rr1 != null && rr1.collider.name.Contains("Ball"))
//					Destroy (rr1.collider.gameObject);
//
//				RaycastHit2D rr2 = Physics2D.Raycast (other.transform.position + new Vector3(0.8f, 0.0f, 0.0f), new Vector2 (1.0f, 0.0f), blustdistance);
//				//print ("rr2 "+rr2.collider.name);
//				if(rr2 != null && rr2.collider.name.Contains("Ball"))
//					Destroy (rr2.collider.gameObject);
//
//				RaycastHit2D rr3 = Physics2D.Raycast (other.transform.position + new Vector3(-0.8f, 0.0f, 0.0f), new Vector2 (-1.0f, 0.0f), blustdistance);
//				//print ("rr3 "+rr3.collider.name);
//				if(rr3 != null && rr3.collider.name.Contains("Ball"))
//					Destroy (rr3.collider.gameObject);
//			}
			print("Other: " + other.transform.position);
			foreach (GameObject ball in activeBallList) {
				print (ball.transform.position);

				float dist = Vector3.Distance (other.transform.position,ball.transform.position);
				print ("distance : " + dist);
				if (dist < blustdistance) {
					print ("dist : " + dist);
					print ("Blast " + blustdistance);
					print (dist < blustdistance);
					//print ("bal");
					ball.GetComponent<iBall> ().Destroy ();
				}
			}

		}

		//print (this.transform.position);

		//print ("on trigger fire");
		if(i != null){
			if (i.isThrown)
			{
				
				foreach (GameObject ob in  Transform.FindObjectsOfType<GameObject>()) {
					IShootBall ishootball = ob.GetComponent<IShootBall> ();
					if(ishootball!=null)
					ishootball.setDestroyedID (this.GetComponent<iBall>().type);
				}
				this.GetComponent<iBall> ().ScoreUpdate (1);
				other.GetComponent<iBall> ().Destroy ();
				this.GetComponent<iBall> ().Destroy ();
				//Destroy (other.gameObject);
				//Destroy (this.gameObject);
			}
		}

		if (other.tag == "Left")
		{
			Vector3 normal = new Vector3 ( 1.0f, 0.0f, 0.0f).normalized;
			float product = Vector3.Dot (movedirection, normal);
			Vector3 pro = 2 * product * normal;
			movedirection = movedirection - pro;
		}
		else if (other.tag == "Right")
		{
			Vector3 normal = new Vector3 ( -1.0f, 0.0f, 0.0f).normalized;
			float product = Vector3.Dot (movedirection, normal);
			Vector3 pro = 2 * product * normal;
			movedirection = movedirection - pro;
		}
		else if (other.tag == "Top")
		{
			Vector3 normal = new Vector3 ( 0.0f, -1.0f, 0.0f).normalized;
			float product = Vector3.Dot (movedirection, normal);
			Vector3 pro = 2 * product * normal;
			movedirection = movedirection - pro;
			Destroy (this.gameObject);
		}
		else if (other.tag == "Bottom")
		{
			Vector3 normal = new Vector3 ( 0.0f, 1.0f, 0.0f).normalized;
			float product = Vector3.Dot (movedirection, normal);
			Vector3 pro = 2 * product * normal;
			movedirection = movedirection - pro;
		}
	}
}
