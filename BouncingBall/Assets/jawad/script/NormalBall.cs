﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBall : MonoBehaviour, iBall {

	private int id = 1;
	private Vector2 position;
	public Vector3 movedirection;
	private float movespeed = 0.07f;
	public bool Thrown = false;

	public void SetMoveDirection(Vector2 dir){
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

	public void SetPosition(Vector2 pos){
		
		position = pos;
		this.GetComponent<Transform>().position = position;

	}
	public void SetSpeed(float speed){
		movespeed = speed;
	}

	public void Destroy(){
		Destroy (this.gameObject);
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.position = Vector2.MoveTowards (this.gameObject.transform.position, this.gameObject.transform.position + movedirection, movespeed);
	}

	void OnTriggerEnter2D(Collider2D other){

		//print (this.transform.position);
		iBall i = other.GetComponent<iBall>();
		print ("on trigger normal");
		if(i != null){
			if (i.isThrown)
			{
				Destroy (other.gameObject);
				Destroy (this.gameObject);
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

public interface iBall{
	int type {
		get;
	}
	bool isThrown {
		get;
		set;
	}

	void SetPosition (Vector2 pos);
	void SetMoveDirection (Vector2 dir);
	void SetSpeed (float speed);
	void Destroy ();

}