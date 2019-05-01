using System.Collections;
using UnityEngine;

public class HammuruAI : MonoBehaviour {

    public GameObject player;
    public float walkSpeed = 15f;
    public AudioClip walkSFX, attackSFX, contactSFX, dieSFX;
    public GameObject flameVFX, explosionVFX;
    public Transform attackArea;
    public Camera playerCamera;
    public GameObject treasure;

    private Animator animator;
    private AudioSource audioSource;
    private bool playerIsNear;
    private EnemyStats stats;
    void Start() {
        playerCamera = FindObjectOfType<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        stats = GetComponent<EnemyStats>();
        StartCoroutine(WalkFX());
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Bullet")) {
            stats.hp -= 1;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            animator.SetTrigger("PlayerIsNear");
            StopAllCoroutines();
            StartCoroutine(AttackFX());
            playerIsNear = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        print("hi");
        if(other.gameObject.CompareTag("Player")) {
            //StopAllCoroutines();
            StartCoroutine(WalkFX());
            playerIsNear = false;
        }
    }

    void Update() {
        transform.LookAt(player.transform);
        if(!playerIsNear)
            transform.position += transform.forward * Time.deltaTime * walkSpeed;

        if(stats.hp <= 0) {
            StopAllCoroutines();
            var position = transform.position + new Vector3(0f, 10f, 0f);
            explosionVFX.gameObject.transform.localScale = new Vector3(3f, 3f, 1f);
            var obj1 = Instantiate(explosionVFX, position, transform.rotation);
            var obj2 = Instantiate(explosionVFX, position, transform.rotation);
            //audioSource.PlayOneShot(dieSFX);
            playerCamera.GetComponent<AudioSource>().enabled = false;
            Destroy(obj1, 1f);
            Destroy(obj2, 1f);
            position = transform.position + new Vector3(0f, 1f, 0f);
            Instantiate(treasure, position, transform.rotation);
            Destroy(gameObject);
        }
    }

    IEnumerator WalkFX() {
        while(true) {
            audioSource.PlayOneShot(walkSFX, Random.Range(.4f, .8f));
            yield return new WaitForSeconds(.2f);
            audioSource.PlayOneShot(walkSFX, Random.Range(.4f, .8f));
            yield return new WaitForSeconds(.2f);
            audioSource.PlayOneShot(walkSFX, Random.Range(.4f, .8f));
            yield return new WaitForSeconds(.2f);
        }

    }

    IEnumerator AttackFX() {
        yield return new WaitForSeconds(.1f);
        audioSource.PlayOneShot(attackSFX);


        yield return new WaitForSeconds(.5f);
        var position = attackArea.position + new Vector3(0f, 5f, 0f);
        explosionVFX.GetComponent<AudioSource>().clip = contactSFX;
        GameObject obj = Instantiate(explosionVFX, position, attackArea.rotation);
        Destroy(obj, 1f);

    }



}
