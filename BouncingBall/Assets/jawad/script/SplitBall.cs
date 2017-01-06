﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBall : MonoBehaviour, iBall {

	private int id = 5;
	private Vector2 position;

	public int type{
		get{ 
			return id;
		}
	}
	public void SetPosition(Vector2 pos){

		position = pos;
		this.GetComponent<Transform>().position = position;

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
