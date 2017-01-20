using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBall : MonoBehaviour, iBall {

	private int id = 5;
	private Vector2 position;

	public Vector3 movedirection;
	public float movespeed = 0.0f;
	public bool Thrown = false;

	private float instantiatetime = 0.0f;
	public float splittime = 0.0f;
	public static bool split = true;

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
	}
	// Use this for initialization
	void Start () {
		instantiatetime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.position = Vector2.MoveTowards (this.gameObject.transform.position, this.gameObject.transform.position + movedirection, movespeed);
		if ((Time.time - instantiatetime) > splittime && split)
		{
			Vector3 normal = new Vector3 ( 0.0f, 1.0f, 0.0f).normalized;
			float product = Vector3.Dot ( - movedirection, normal);
			Vector3 pro = 2 * product * normal;
			Vector3 newmovedirection = - movedirection - pro;
			newmovedirection.Normalize();
			GameObject newball = Instantiate (this.gameObject);
			iBall ib = newball.GetComponent<iBall> ();
			ib.isThrown = true;
			ib.SetMoveDirection (newmovedirection);
			this.transform.localScale *= 0.7f; 
			newball.transform.localScale = this.transform.localScale;
			ib.SetSpeed (movespeed);
			split = false;
		}




	}
	void OnTriggerEnter2D(Collider2D other){

		//print (this.transform.position);
		iBall i = other.GetComponent<iBall>();
		print ("on trigger split");
		if(i != null){
			if (i.isThrown && !this.isThrown)
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
