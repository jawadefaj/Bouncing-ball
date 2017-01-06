using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBall : MonoBehaviour, iBall {

	private int id = 1;
	private Vector2 position;
	public Vector3 movedirection;

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
	public void SetPosition(Vector2 pos){
		
		position = pos;
		this.GetComponent<Transform>().position = position;

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.position = Vector2.MoveTowards (this.gameObject.transform.position, this.gameObject.transform.position + movedirection, 0.05f);
	}

	void OnTriggerEnter2D(Collider2D other){


		if (other.tag == "Boundary")
		{
			print ("nothings done here too");
		}
//		else
//		{
//			print ("Nothings done here");
//		}
	}
}

public interface iBall{
	int type {
		get;
	}

	void SetPosition (Vector2 pos);
	void SetMoveDirection (Vector2 dir);
}