using UnityEngine;
using System.Collections;
using System;

public class ShipControls : MonoBehaviour {

	private const float ANGLETORADIANS = 0.0174532925f;
	private const float DIRECTIONCHANGEADJUST = 0.08f;

	private float angleSpeed=0;
	private float thrust = 18f;
	private float maxAngleSpeed = 250f;
	private float minAngleSpeed = 40f;
	private float speedWarmup = 3f;

	public float shipAccel { get; set;}

	//private int cyclesSinceLastBurst = 100;

	private ThrusterAnimation thrusterAnimation;
	private AudioSource shipAudioSource;

	public delegate void ChangedEventHandler(object sender, EventArgs e);
	public event ChangedEventHandler ShipActivated;

	void Start () {
		GameState.Instance.playerActive = true;
		GameState.Instance.ActiveCheckpointChanged += new GameState.ChangedEventHandler (ActiveCheckpointChanged);

		shipAccel = 0;
		foreach (Transform child in transform) {
			ThrusterAnimation animation = child.GetComponent<ThrusterAnimation> ();
			if(animation != null){
				this.thrusterAnimation = animation;
				break;
			}
		}
		shipAudioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(GameState.Instance.playerActive){
			calculateAcceleration ();

			if(Input.GetButtonDown("Thrust")){
				thrusterAnimation.startThrusting();
			}
			if(Input.GetButtonUp("Thrust")){
				thrusterAnimation.stopThrusting();
			}
		}
	}

	private void calculateAcceleration(){
		//cyclesSinceLastBurst++;
		shipAccel = 0;
		if(Input.GetButton ("Thrust")){
			countFuel();
			this.shipAccel += this.thrust;
			if(GetComponent<Rigidbody2D>().velocity.magnitude < 6){
				this.shipAccel*=2;
			}
			/*
			 * 
			 * If player hasn't thrusted in a few cycles give boost
			if(this.cyclesSinceLastBurst>100){
				this.shipAccel*=40;
				this.cyclesSinceLastBurst=0;
			}
			else{
				this.cyclesSinceLastBurst/=2;
			}*/
		}
		/*if(Input.GetButton ("Brake")){
			rigidbody2D.velocity = rigidbody2D.velocity*0.975f;

		}*/
	}

	private void countFuel(){
		if(GameState.Instance.InOpenSpace){
			GameState.Instance.fuelUsed++;
			GameState.Instance.fuelUsedLastCheckpoint++;
		}
	}

	void FixedUpdate (){
		if(GameState.Instance.playerActive){

			applyAcceleration ();

			applyASTTurning();

			//alignWithMouse();

			if(Input.GetButtonDown("Flip")){
				transform.Rotate(new Vector3(0,0,180));
			}
		}
	}

	private void applyASTTurning(){
		float hArrowKeyInput = Input.GetAxis("Horizontal");
		if(hArrowKeyInput >=1){
			applyClockwiseRotation();
			
		}
		else if(hArrowKeyInput <= -1){
			applyCounterClockwiseRotation();
		}
		else{
			angleSpeed = 0;
			GetComponent<Rigidbody2D>().angularVelocity *= 0.95f;
		}
	}

	private void applyClockwiseRotation(){
		if(angleSpeed >= 0){
			angleSpeed = -minAngleSpeed;
		}
		else if(angleSpeed > -maxAngleSpeed){
			angleSpeed -= speedWarmup;
		}
		else{
			angleSpeed = -maxAngleSpeed;
		}
		GetComponent<Rigidbody2D>().angularVelocity = angleSpeed;
	}
	private void applyCounterClockwiseRotation(){
		if(angleSpeed <= 0){
			angleSpeed = minAngleSpeed;
		}
		else if(angleSpeed < maxAngleSpeed){
			angleSpeed += speedWarmup;
		}
		else{
			angleSpeed = maxAngleSpeed;
		}
		GetComponent<Rigidbody2D>().angularVelocity = angleSpeed;
	}

	private void alignWithMouse(){
		Vector3 mousePosition = MouseWorldPosition ();

		Vector3 shipToMouseVector = mousePosition - transform.position;

		float angleOfShipToMouseVector = Mathf.Atan (shipToMouseVector.y / shipToMouseVector.x);
		if (shipToMouseVector.x >= 0f && shipToMouseVector.y < 0f) {
			angleOfShipToMouseVector += 2f * Mathf.PI;
		}
		else if (shipToMouseVector.x < 0f) {
			angleOfShipToMouseVector += Mathf.PI;
		}
		angleOfShipToMouseVector *= Mathf.Rad2Deg;

		transform.localEulerAngles = new Vector3 (0,0,angleOfShipToMouseVector);
	}

	public Vector3 MouseWorldPosition() {
		Vector3 cameraPosition = Camera.main.transform.position;
		Vector3 playerPosition = transform.position;
		
		float cameraToShipDistance = Mathf.Abs (cameraPosition.z - playerPosition.z);
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
		Vector3 point = ray.origin + (ray.direction * cameraToShipDistance);
		return point;
	}

	public void deactivateShip(){
		GameState.Instance.playerActive = false;
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		GetComponent<Rigidbody2D> ().angularVelocity = 0f;
		GameState.Instance.exitOpenSpace();
		thrusterAnimation.stopThrusting ();
	}

	public void activateShip(){
		GameState.Instance.playerActive = true;
		GetComponent<SpriteRenderer> ().enabled = true;
		GameState.Instance.exitOpenSpace();
		checkThrustingSound ();
		if(ShipActivated != null){
			ShipActivated(this, EventArgs.Empty);
		}
	}

	public void checkThrustingSound(){
		if (Input.GetButton ("Thrust")) {
			thrusterAnimation.startThrusting();
		}
	}

	public void moveToLastCheckpoint(){
		CheckPoint lastPoint = GameState.Instance.getLastCheckPoint();
		Vector3 checkPointPosition = lastPoint.transform.position;
		transform.position = new Vector3 (checkPointPosition.x, checkPointPosition.y, transform.position.z);
		GetComponent<Rigidbody2D>().angularVelocity = 0f;
		transform.rotation = lastPoint.transform.rotation;
		transform.Rotate (0,0,90);
	}

	private void ActiveCheckpointChanged(object sender, EventArgs e){
		GameState.Instance.fuelUsedLastCheckpoint =0;
	}

	public void die(){
		deactivateShip ();
		shipAudioSource.Play ();
		GameState.Instance.fuelUsed -= GameState.Instance.fuelUsedLastCheckpoint;
		GameState.Instance.fuelUsedLastCheckpoint = 0;
	}

	private void applyAcceleration(){
		if(shipAccel != 0){
			float boost = DIRECTIONCHANGEADJUST*Vector3.Angle(GetComponent<Rigidbody2D>().velocity, transform.right);
			shipAccel += boost;
		}
		GetComponent<Rigidbody2D>().AddForce (transform.right*shipAccel);

		if(!GameState.Instance.InOpenSpace){
			GetComponent<Rigidbody2D>().velocity*=0.988f;
		}

	}

}
