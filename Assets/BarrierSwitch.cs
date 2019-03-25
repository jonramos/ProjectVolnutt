using System.Collections;
using UnityEngine;

public class BarrierSwitch : MonoBehaviour {

    public GameObject barrier;
    public GameObject enemy1, enemy2;
    public AudioClip switchSFX;

    private bool alreadyActive;
    private AudioSource audioSource;


    private void Start() {
        audioSource = GetComponent<AudioSource>();
        barrier.SetActive(false);
        enemy1.SetActive(false);
        enemy2.SetActive(false);
    }


    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            barrier.SetActive(true);
            if(!alreadyActive)
                StartCoroutine(BarrierUp());
            enemy1.SetActive(true);
            enemy2.SetActive(true);
        }

    }

    private IEnumerator BarrierUp() {
        GetComponent<CapsuleCollider>().enabled = false;
        alreadyActive = true;
        audioSource.PlayOneShot(switchSFX);
        transform.Translate(0f, -.35f, 0f);


        while(barrier.transform.position.y < 10) {

            barrier.transform.Translate(0, 0f, 1f);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

    void Update() {
        if(enemy1 == null && enemy2 == null)
            barrier.SetActive(false);
    }
}
