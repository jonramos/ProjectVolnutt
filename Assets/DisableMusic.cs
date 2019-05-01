using UnityEngine;

public class DisableMusic : MonoBehaviour {
    private bool trigger;
    private void OnTriggerEnter(Collider other) {
        if(!trigger) {
            Camera cam = FindObjectOfType<Camera>();
            cam.GetComponent<AudioSource>().Stop();
            trigger = true;
        }

    }
}
