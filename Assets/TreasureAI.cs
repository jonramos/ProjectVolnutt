using System.Collections;
using UnityEngine;

public class TreasureAI : MonoBehaviour {

    public AudioClip openSFX, closeSFX;
    public GameObject EndGameUI;
    private bool isOpen;
    private Animator animator;
    private AudioSource audioSource;


    //private GameObject Player;
    void Start() {
        //Player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update() {
        if(!isOpen && Input.GetKeyDown(KeyCode.F)) {
            isOpen = true;
            StartCoroutine(Reveal());

        }
    }

    IEnumerator Reveal() {
        //Player.GetComponent<CharacterController>().enabled = false;
        animator.Play("Open");
        audioSource.PlayOneShot(openSFX, .7f);
        Instantiate(EndGameUI);

        yield return new WaitForSeconds(5f);
        animator.Play("Close");
        audioSource.PlayOneShot(closeSFX, .7f);


    }
}
