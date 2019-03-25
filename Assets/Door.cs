using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour {

    private bool doorIsOpen;
    private AudioSource audioSource;
    public GameObject rightDoor;
    public GameObject leftDoor;

    public AudioClip doorSFX;
    void Start() {
        audioSource = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            if(!doorIsOpen)
                StartCoroutine(DoorOpening());
        }
    }
    // Update is called once per frame
    void Update() {

    }
    private IEnumerator DoorOpening() {
        audioSource.PlayOneShot(doorSFX);
        doorIsOpen = true;
        int i = 0;
        while(i < 20) {

            rightDoor.transform.Translate(-1f, 0f, 0f);
            leftDoor.transform.Translate(1f, 0f, 0f);
            yield return new WaitForFixedUpdate();
            i++;
        }
        yield return null;
    }
}
