using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public AudioClip walkSFX;
    public bool isRunning;

    private AudioSource audioSource;
    private Animator anim;
    private CharacterController controller;

    public float speed = 6.0f;
    public float turnSpeed = 60.0f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();

    }

    void Update() {
        if(Input.GetKey("w")) {
            anim.SetBool("isRunning", true);

        }
        else {
            anim.SetBool("isRunning", false);
            isRunning = false;
        }
        if(Input.GetKeyDown(KeyCode.W)) {
            //anim.SetBool("isRunning", true);
            StartCoroutine(Walk());
        }
        if(Input.GetKeyUp(KeyCode.W)) {
            //anim.SetBool("isRunning", true);
            StopAllCoroutines();
        }
        if(controller.isGrounded) {
            moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
        }


        float turn = Input.GetAxis("Horizontal");
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        controller.Move(moveDirection * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime;
    }

    IEnumerator Walk() {
        yield return new WaitForSeconds(.2f);
        while(true) {
            audioSource.PlayOneShot(walkSFX, .7f);
            yield return new WaitForSeconds(.3f);
        }
    }
}