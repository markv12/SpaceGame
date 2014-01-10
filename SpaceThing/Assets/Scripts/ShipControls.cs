using UnityEngine;
using System.Collections;

public class ShipControls : MonoBehaviour {

	private const float ANGLETORADIANS = 0.0174532925f;
	private const float AVERAGEFRAMERATE = 0.02f;

	public float thrust = 0.35f;
	public float maxAngleSpeed = 140f;

	private Vector2 movement;
	private Vector3 rotation;

	public float XAccel { get; set;}
	public float YAccel { get; set;}
	
	private float angleSpeed=0;

	private ThrusterAnimation thrusterAnimation;
	private AudioSource shipAudioSource;

	void Start () {
		XAccel = 0;
		YAccel = 0;
		thrusterAnimation = GameObject.FindGameObjectWithTag ("Thruster").GetComponent<ThrusterAnimation> ();
		shipAudioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(rigidbody2D != null){
			calculateAcceleration ();

			if(Input.GetButtonDown("Jump")){
				thrusterAnimation.startThrusting();
			}
			if(Input.GetButtonUp("Jump")){
				thrusterAnimation.stopThrusting();
			}

			if(Input.GetKeyDown ("x")){
				flip();
			}
		}
	}

	private void calculateAcceleration(){
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
			
			if(rigidbody2D.velocity.magnitude < 6){
				this.XAccel*=2;
				this.YAccel*=2;
			}
		}
		else{
			XAccel = 0;
			YAccel = 0;
		}
	}

	void FixedUpdate ()
	{
		if(rigidbody2D != null){
			float frameRateAdjustment = Time.fixedDeltaTime / AVERAGEFRAMERATE;

			applyAcceleration (frameRateAdjustment);

			float hArrowKeyInput = Input.GetAxis("Horizontal");
			if(hArrowKeyInput > 0){
				angleSpeed = -maxAngleSpeed;
				rigidbody2D.angularVelocity = angleSpeed * frameRateAdjustment;
			}
			else if(hArrowKeyInput < 0){
				angleSpeed = maxAngleSpeed;
				rigidbody2D.angularVelocity = angleSpeed * frameRateAdjustment;
			}
			rigidbody2D.angularVelocity *= 0.92f;


			checkForNeedToFlip ();
		}
	}

	public void destroyShip(){
		Destroy (GetComponent<SpriteRenderer>());
		//rigidbody2D.velocity = Vector2.zero;
		Destroy(GetComponent<Rigidbody2D>());
		thrusterAnimation.stopThrusting ();
	}

	public void die(){
		destroyShip ();
		shipAudioSource.Play ();
	}

	private void applyAcceleration(float frameRateAdjustment){
		Vector2 thrustVector = new Vector2 (XAccel, YAccel) * (frameRateAdjustment);
		rigidbody2D.velocity += thrustVector;
	}

	private void checkForNeedToFlip(){
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

	private void flip(){
		transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,0);
	}

	private bool flipped{
		get{
			return transform.localScale.x < 0; 
		}

	}

}
