using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour, iBall {

	private int id = 4;
	private Vector2 position;

	public int type{
		get{ 
			return id;
		}
	}
	public void SetPosition(Vector2 pos){
		///print ("called");
		position = pos;
		this.GetComponent<Transform>().localPosition = position;

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
