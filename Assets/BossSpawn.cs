using System.Collections;
using UnityEngine;

public class BossSpawn : MonoBehaviour {
    public GameObject barrier;
    public AudioClip switchSFX, bossClip;
    public GameObject Hammuru;
    public Renderer head;
    public Camera playerCamera;

    private AudioSource audioSource;
    private bool alreadyActivated;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        playerCamera = FindObjectOfType<Camera>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player") && !alreadyActivated) {
            StartCoroutine(BarrierUp());
            BossActivate();
        }
    }

    private void BossActivate() {
        Hammuru.GetComponent<Animator>().enabled = true;
        Hammuru.GetComponent<HammuruAI>().enabled = true;
        head.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        Hammuru.GetComponent<HammuruAI>().enabled = true;
    }

    private IEnumerator BarrierUp() {
        alreadyActivated = true;
        audioSource.PlayOneShot(switchSFX);
        transform.Translate(0f, -.35f, 0f);
        playerCamera.GetComponent<AudioSource>().clip = bossClip;
        playerCamera.GetComponent<AudioSource>().Play(); ;

        while(barrier.transform.position.y < 10) {

            barrier.transform.Translate(0, 0f, 1f);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
}
