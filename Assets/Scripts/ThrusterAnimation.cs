using UnityEngine;
using System.Collections;

public class ThrusterAnimation : MonoBehaviour {
	private Animator thrusterAnimator;
	private AudioSource thrusterAudioSource;

	private bool thrusting = false;
	private bool everyOther = false;

	private float pitch;
	private const float STARTINGPITCH = 0.3f;
	private const float MAXPITCH = 1.3f;


	void Start(){
		thrusterAnimator = GetComponent<Animator>();
		thrusterAudioSource = GetComponent<AudioSource>();
		pitch = STARTINGPITCH;
	}

	public void startThrusting(){
		thrusterAnimator.SetBool("thrusting", true);
		thrusterAudioSource.Play();
		thrusting = true;
		pitch = STARTINGPITCH;
	}

	public void stopThrusting(){
		thrusterAnimator.SetBool("thrusting", false);
		//thrusterAudioSource.Stop();
		thrusting = false;
	}

	void Update(){
		if(thrusting){
			if(pitch < MAXPITCH){
				pitch += 0.035f;
			}
		}
		else{
			if(pitch > 0){
				pitch -= 0.08f;
			}
			else{
				pitch = 0;
			}
		}
		thrusterAudioSource.pitch = pitch;
		thrusterAudioSource.volume = pitch*0.5f;
	}

	/*void OnTriggerEnter2D(Collider2D collision){
		if(collider.transform.position.z == transform.position.z){
			spawnParticleEffect (transform.position);
		}
	}
	void OnTriggerStay2D(Collider2D collision){
		if(collider.transform.position.z == transform.position.z){
			spawnParticleEffect (transform.position);
		}

	}*/

	private void spawnParticleEffect(Vector3 position){
		if(thrusting){
			if(everyOther){
				SpecialEffectsHelper.Instance.ThrusterCollision(position);
				everyOther = false;
			}
			else{
				everyOther = true;
			}
		}
	}

}
