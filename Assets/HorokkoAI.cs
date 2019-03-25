using System.Collections;
using UnityEngine;

public class HorokkoAI : MonoBehaviour {

    public Transform target;
    public Transform bombOrigin;
    public GameObject bombPrefab;
    public float speed = 4f;
    public GameObject Horokko;


    public AudioClip walkSFX;
    public AudioClip shotSFX;
    public AudioClip dieSFX;
    public AudioClip triggerSFX;

    private AudioSource audioSource;
    private Animator animator;
    private bool born;
    private bool alreadyBorn;
    private bool isWalkRoutineOn;
    private bool isAlive, startShooting;
    private bool isShootingRoutineOff = true;

    private void Start() {

        animator = Horokko.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        transform.LookAt(target);
        transform.Rotate(Vector3.up, -90f);

        var distance = Vector3.Distance(transform.position, target.position);
        if(distance < 30f) {
            born = true;
        }

        if(born == true && alreadyBorn == false) {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            Horokko.GetComponent<Animator>().Play("Spawn");
            alreadyBorn = true;
            isAlive = true;
        }

        if(GetComponent<EnemyStats>().hp <= 0 && isAlive) {
            isAlive = false;
            StopAllCoroutines();
            animator.SetBool("isDying", true);
            audioSource.PlayOneShot(dieSFX, .7f);
            StartCoroutine(DieFeedback());
        }

        if(alreadyBorn && isAlive) {
            if(distance > 80f) {
                //Stop Walking and enter in Idle
                StopAllCoroutines();
                isWalkRoutineOn = false;

                animator.SetBool("PlayerIsNear", false);
                animator.SetBool("TriggeredPlayer", false);

            }
            else if(distance < 60f && distance > 15f) {
                //Walk Towards the player
                StopAllCoroutines();
                //StopCoroutine(Shoot());
                isShootingRoutineOff = true;
                if(!isWalkRoutineOn)
                    StartCoroutine(WalkRoutine());

                animator.SetBool("PlayerIsNear", true);
                animator.SetBool("TriggeredPlayer", false);
                transform.position += transform.right * Time.deltaTime * speed;
            }

            else if(distance < 15f) {
                //Start shooting
                StopCoroutine(WalkRoutine());
                isWalkRoutineOn = false;
                startShooting = true;

                animator.SetBool("PlayerIsNear", false);
                animator.SetBool("TriggeredPlayer", true);
                if(startShooting && isShootingRoutineOff) {
                    StartCoroutine(Shoot());
                }

            }
            else {

                isShootingRoutineOff = true;
                animator.SetBool("PlayerIsNear", true);
                animator.SetBool("TriggeredPlayer", false);
            }
        }

    }

    IEnumerator Shoot() {
        isShootingRoutineOff = false;
        audioSource.PlayOneShot(triggerSFX, .7f);
        yield return new WaitForSeconds(.5f);
        while(true) {
            GameObject bomb = Instantiate(bombPrefab, bombOrigin.transform.position, bombPrefab.transform.rotation);
            audioSource.PlayOneShot(shotSFX);
            bomb.GetComponent<Rigidbody>().velocity = bombOrigin.transform.right * 40f;
            yield return new WaitForSeconds(1f);

        }
    }

    IEnumerator WalkRoutine() {
        isWalkRoutineOn = true;
        while(true) {
            audioSource.PlayOneShot(walkSFX, .7f);
            yield return new WaitForSeconds(.2f);
            audioSource.PlayOneShot(walkSFX, .7f);

            yield return new WaitForSeconds(.7f);
        }
    }

    IEnumerator DieFeedback() {
        yield return new WaitForSeconds(1.5f);
        //add particle/vfx

        Destroy(gameObject);
        yield return null;
    }


}
