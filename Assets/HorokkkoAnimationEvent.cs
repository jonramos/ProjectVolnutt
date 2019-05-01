using UnityEngine;

public class HorokkkoAnimationEvent : MonoBehaviour {

    public AudioClip walkSFX;

    private AudioSource audioSource;


    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    public void WalkSFX() {
        audioSource.PlayOneShot(walkSFX, Random.Range(.5f, .7f));
    }
}
