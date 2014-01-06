using UnityEngine;
using System.Collections;

public class ShipControls : MonoBehaviour {

	private const float ANGLETORADIANS = 0.0174532925f;
	private const float AVERAGEFRAMERATE = 0.02f;

	private float thrust = 0.35f;
	private float maxAngleSpeed = 140f;

	private Vector2 movement;
	private Vector3 rotation;

	public float XAccel { get; set;}
	public float YAccel { get; set;}
	
	private float angleSpeed=0;

	private Animator thrusterAnimator;
	private AudioSource thrusterAudioSource;

	void Start () {
		XAccel = 0;
		YAccel = 0;
		thrusterAnimator = GameObject.FindGameObjectWithTag ("Thruster").GetComponent<Animator> ();
		thrusterAudioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton ("Jump")){
			float xThrust = this.thrust*Mathf.Cos(transform.rotation.eulerAngles.z*ANGLETORADIANS);
			float yThrust = this.thrust*Mathf.Sin(transform.rotation.eulerAngles.z*ANGLETORADIANS);
			if(flipped){
				this.XAccel = -xThrust;
				this.YAccel = -yThrust;
			}
			else{
				this.XAccel = xThrust;
				this.YAccel = yThrust;
			}
			this.XAccel-= (rigidbody2D.velocity.x/90f);
			this.YAccel-= (rigidbody2D.velocity.y/90f);

			if(rigidbody2D.velocity.magnitude < 5){
				this.XAccel*=2;
				this.YAccel*=2;
			}

		}
		else{
			XAccel = 0;
			YAccel = 0;
		}

		if(Input.GetButtonDown("Jump")){
			thrusterAnimator.SetBool("thrusting", true);
			thrusterAudioSource.Play();
		}
		if(Input.GetButtonUp("Jump")){
			thrusterAnimator.SetBool("thrusting", false);
			thrusterAudioSource.Stop();
		}

		if(Input.GetKeyDown ("x")){
			flip();
		}
	}

	void FixedUpdate ()
	{
		float frameAdjustment = Time.fixedDeltaTime / AVERAGEFRAMERATE;

		Vector2 thrustVector = new Vector2 (XAccel, YAccel) * (frameAdjustment);

		rigidbody2D.velocity += thrustVector;

		float hArrowKeyInput = Input.GetAxis("Horizontal");
		if(hArrowKeyInput > 0){
			angleSpeed = -maxAngleSpeed;
			rigidbody2D.angularVelocity = angleSpeed * frameAdjustment;
		}
		else if(hArrowKeyInput < 0){
			angleSpeed = maxAngleSpeed;
			rigidbody2D.angularVelocity = angleSpeed * frameAdjustment;
		}

		rigidbody2D.angularVelocity *= 0.92f;



		float currentAngle = transform.rotation.eulerAngles.z;
		if(currentAngle > 180){
			currentAngle = currentAngle-360;
		}
		if(currentAngle <-90){
			flip();
			transform.Rotate (new Vector3(0,0,180));
		}
		else if(currentAngle >90){
			flip();
			transform.Rotate (new Vector3(0,0,180));
		}
	}

	void flip(){
		transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,0);
	}

	bool flipped{
		get{
			return transform.localScale.x < 0; 
		}

	}

}
