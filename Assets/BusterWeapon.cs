using System.Collections;
using UnityEngine;

public class BusterWeapon : MonoBehaviour {

    public float shootDelay = 1f;
    public float bulletSpeed = 50f;
    public GameObject buster;
    public GameObject arm;
    public GameObject bullet;

    public Transform busterOrigin;

    public AnimationClip animClip;
    private Animator anim;

    public AudioClip sfx;
    private AudioSource audioSource;

    public bool isEquipped;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    void Update() {

        if(isEquipped) {
            buster.SetActive(true);
            arm.SetActive(false);
        }
        else {
            buster.SetActive(false);
            arm.SetActive(true);

        }


        if(Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0) && isEquipped && !anim.GetBool("isRightRunning") && !anim.GetBool("isLeftRunning") && !anim.GetBool("isBackRunning")) {
            anim.SetBool("isShooting", true);
            StartCoroutine(Shoot());

        }
        if(Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonUp(0)) {
            StopAllCoroutines();
            anim.SetBool("isShooting", false);
        }
    }

    IEnumerator Shoot() {
        while(true) {
            audioSource.PlayOneShot(sfx, .7f);
            if(!anim.GetBool("isRunning") && anim.GetBool("isGrounded"))
                anim.Play("BusterShooting", 0, 0.25f);

            GameObject x = Instantiate(bullet, busterOrigin.position, busterOrigin.rotation);
            x.GetComponent<Rigidbody>().velocity = x.transform.forward * bulletSpeed;
            yield return new WaitForSeconds(shootDelay);
        }
    }
}