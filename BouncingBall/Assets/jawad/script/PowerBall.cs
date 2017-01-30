using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBall : MonoBehaviour, iBall {
	
	private int id = 2;
	private Vector2 position;
	private Vector3 movedirection;
	private float movespeed = 0.0f;
	public bool Thrown = false;
	//public GameObject ballSpawner;

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

	public void SetPosition(Vector2 pos){

		position = pos;
		this.GetComponent<Transform>().localPosition = position;

	}
	public void SetSpeed(float speed){
		movespeed = speed;
	}

	public void Destroy(){
		Destroy (this.gameObject);
		foreach (GameObject ob in Transform.FindObjectsOfType<GameObject>()) {
			IShootBall ishootball = ob.GetComponent<IShootBall> ();
			if (ishootball != null)
				ishootball.isShootable = true;

		}
	}

	public void ScoreUpdate(int s){

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
		iBall iball = this.GetComponent<iBall>();
		if (iball != null && iball.isThrown) {
		//	print ("I am here");
			foreach (GameObject ob in Transform.FindObjectsOfType<GameObject>()) {
				IBallSpawn iballspawn = ob.GetComponent<IBallSpawn> ();
				if (iballspawn != null)
					iballspawn.moveUp ();
			}
			//ballSpawner.GetComponent<IBallSpawn> ().moveUp ();
		}

		iBall i = other.GetComponent<iBall>();
//		print ("on trigger power");
		if(i != null){
			if (i.isThrown)
			{
				
				foreach (GameObject ob in Transform.FindObjectsOfType<GameObject>()) {
					IShootBall ishootball = ob.GetComponent<IShootBall> ();
					//IBallSpawn iballSpawn = ob.GetComponent<IBallSpawn> ();
					if(ishootball!=null)
						ishootball.setDestroyedID (this.GetComponent<iBall>().type);
//					if (iballSpawn != null)
//						iballSpawn.moveUp ();
				}
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
