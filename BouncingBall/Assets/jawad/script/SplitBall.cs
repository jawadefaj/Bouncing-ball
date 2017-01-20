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
		if ((Time.time - instantiatetime) > splittime && Thrown)
		{
			instantiatetime = Time.time;
			Vector3 normal = new Vector3 ( 0.0f, 1.0f, 0.0f).normalized;
			float product = Vector3.Dot ( - movedirection, normal);
			float angle = Vector3.Angle (movedirection, new Vector3 (1.0f, 0.0f, 0.0f));
			print (" angle "+angle);
			Vector3 pro = 2 * product * normal;
			Vector3 newmovedirection = - movedirection - pro;
			newmovedirection.Normalize();
			GameObject newball = Instantiate (this.gameObject, this.transform.position, this.transform.rotation);

			iBall ib = newball.GetComponent<iBall> ();
			ib.isThrown = true;
			if (angle > 80 && angle < 100)
			{
				newmovedirection = new Vector3 (-newmovedirection.y, newmovedirection.y, newmovedirection.z);
				movedirection = new Vector3 (movedirection.y, movedirection.y, movedirection.z);
			}
				

			ib.SetMoveDirection (newmovedirection);
			this.transform.localScale *= 1.0f; 
			newball.transform.localScale = this.transform.localScale;
			ib.SetSpeed (movespeed);
			//split = false;
		}




	}
	void OnTriggerEnter2D(Collider2D other){

		//print (this.transform.position);
		iBall i = other.GetComponent<iBall>();
		print ("on trigger split");
		if(i != null){
			if (i.isThrown && !this.isThrown)
			{
				print ("isthwn");
				foreach (GameObject ob in Transform.FindObjectsOfType<GameObject>()) {
					IShootBall ishootball = ob.GetComponent<IShootBall> ();
					if(ishootball!=null)
					ishootball.setDestroyedID (this.GetComponent<iBall>().type);
				}
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
