using UnityEngine;
using System.Collections;

public class CentralAudio : MonoBehaviour {

	private AudioClip menuClick;
	private AudioClip mainMusic;

	private static CentralAudio instance;
	public static CentralAudio Instance{
		get{
			if(instance == null){
				instance = new GameObject("CentralAudio").AddComponent<CentralAudio>();
				instance.menuClick = Resources.Load("menu") as AudioClip;
				instance.mainMusic = Resources.Load("Progenibeat") as AudioClip;
			}	
			return instance;
		}
	}
	
	private AudioSource mSource;
	private AudioSource musicSource{
		get{
			if(mSource == null){
				mSource = gameObject.AddComponent<AudioSource>();
				mSource.clip = mainMusic;
				mSource.loop = true;
				DontDestroyOnLoad (mSource);
			}
			return mSource;
		}
	}

	private AudioSource cSource;
	private AudioSource clickSource{
		get{
			if(cSource == null){
				cSource = gameObject.AddComponent<AudioSource>();
				cSource.clip = menuClick;
				cSource.loop = false;
				DontDestroyOnLoad (cSource);
			}
			return cSource;
		}
	}

	public void playMusic(){
		if(!musicSource.isPlaying){
			musicSource.Stop ();
			musicSource.Play ();
		}
	}
	
	public void pauseMusic(){
		musicSource.Pause ();
	}
	
	public void resumeMusic(){
		if(!musicSource.isPlaying){
			musicSource.Play ();
		}
	}
	
	public bool musicPlaying{
		get{
			return musicSource.isPlaying;
		}
	}

	public void playClick(){
		clickSource.Play ();                      
	}
}