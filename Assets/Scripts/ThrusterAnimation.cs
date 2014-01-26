using UnityEngine;
using System.Collections;

public class ThrusterAnimation : MonoBehaviour {
	private Animator thrusterAnimator;
	private AudioSource thrusterAudioSource;

	private bool thrusting = false;

	private bool everyOther = false;

	void Start(){
		thrusterAnimator = GetComponent<Animator>();
		thrusterAudioSource = GetComponent<AudioSource>();
	}

	public void startThrusting(){
		thrusterAnimator.SetBool("thrusting", true);
		thrusterAudioSource.Play();
		thrusting = true;
	}

	public void stopThrusting(){
		thrusterAnimator.SetBool("thrusting", false);
		thrusterAudioSource.Stop();
		thrusting = false;
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
