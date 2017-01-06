using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBall :MonoBehaviour, iBall {

	private int id = 3;
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
