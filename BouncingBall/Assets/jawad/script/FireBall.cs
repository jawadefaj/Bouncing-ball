using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour, iBall {

	private int id = 4;
	private Vector2 position;
	public Vector3 movedirection;
	private float movespeed = 0.06f;
	public bool Thrown = false;
	public float blustdistance;


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
		///print ("called");
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
			if (i != null)
			{
				RaycastHit2D rr1 = Physics2D.Raycast (other.transform.position + new Vector3(0.0f, 0.8f, 0.0f), new Vector2 (0.0f, 1.0f), blustdistance);
				//print ("rr1 "+rr1.collider.name);
				if(rr1 != null)
				Destroy (rr1.collider.gameObject);

				RaycastHit2D rr2 = Physics2D.Raycast (other.transform.position + new Vector3(0.8f, 0.0f, 0.0f), new Vector2 (1.0f, 0.0f), blustdistance);
				//print ("rr2 "+rr2.collider.name);
				if(rr1 != null)
				Destroy (rr2.collider.gameObject);

				RaycastHit2D rr3 = Physics2D.Raycast (other.transform.position + new Vector3(-0.8f, 0.0f, 0.0f), new Vector2 (-1.0f, 0.0f), blustdistance);
				//print ("rr3 "+rr3.collider.name);
				if(rr1 != null)
				Destroy (rr3.collider.gameObject);
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
