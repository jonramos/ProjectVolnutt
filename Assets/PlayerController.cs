using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public AudioClip walkSFX;
    public AudioClip jumpSFX;
    public AudioClip landSFX;

    private AudioSource audioSource;
    private Animator anim;
    private CharacterController controller;

    public float speed = 6.0f;
    public float turnSpeed = 60.0f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;

    private bool jumped, isCoroutineRunning = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();

    }

    void Update() {

        if(Input.GetKeyDown(KeyCode.W) && anim.GetBool("isGrounded")) {
            anim.SetBool("isRunning", true);
            if(!isCoroutineRunning) StartCoroutine(Walk());
        }

        if(Input.GetKeyDown(KeyCode.S) && anim.GetBool("isGrounded") && !anim.GetBool("isShooting")) {
            anim.SetBool("isBackRunning", true);
            if(!isCoroutineRunning) StartCoroutine(Walk());
        }

        if(Input.GetKeyDown(KeyCode.A) && anim.GetBool("isGrounded") && !anim.GetBool("isShooting")) {
            anim.SetBool("isLeftRunning", true);
            if(!isCoroutineRunning) StartCoroutine(Walk());
        }

        if(Input.GetKeyDown(KeyCode.D) && anim.GetBool("isGrounded") && !anim.GetBool("isShooting")) {
            anim.SetBool("isRightRunning", true);
            if(!isCoroutineRunning) StartCoroutine(Walk());
        }

        if(Input.GetKeyUp(KeyCode.W)) {
            anim.SetBool("isRunning", false);
            StopAllCoroutines();
            isCoroutineRunning = false;
        }
        if(Input.GetKeyUp(KeyCode.S)) {
            anim.SetBool("isBackRunning", false);
            anim.SetBool("isRunning", false);
            StopAllCoroutines();
            isCoroutineRunning = false;
        }
        if(Input.GetKeyUp(KeyCode.D)) {
            anim.SetBool("isRightRunning", false);
            StopAllCoroutines();
            isCoroutineRunning = false;
        }
        if(Input.GetKeyUp(KeyCode.A)) {
            anim.SetBool("isLeftRunning", false);
            StopAllCoroutines();
            isCoroutineRunning = false;


        }

        if(controller.isGrounded) {
            anim.SetBool("isGrounded", true);
            if(jumped) {
                audioSource.PlayOneShot(landSFX, .7f);
                jumped = false;
            }

            moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
            moveDirection += transform.right * Input.GetAxis("Horizontal") * speed;
        }


        if(Input.GetKeyDown(KeyCode.Space) && anim.GetBool("isGrounded") && !anim.GetBool("isBackRunning") && !anim.GetBool("isLeftRunning") && !anim.GetBool("isRightRunning")) {

            jumped = true;
            audioSource.PlayOneShot(jumpSFX, .7f);
            anim.SetBool("isGrounded", false);
            anim.Play("Jumping");
            moveDirection.y = 20f;
        }

        float turn = Input.GetAxis("Mouse X");

        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        controller.Move(moveDirection * Time.deltaTime);
    }

    IEnumerator Walk() {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(.2f);
        while(true) {
            audioSource.PlayOneShot(walkSFX, .7f);
            yield return new WaitForSeconds(.3f);
        }
    }
}