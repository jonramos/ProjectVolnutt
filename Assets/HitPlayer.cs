using UnityEngine;

public class HitPlayer : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerStats>().hitByExplosion = true;

        }
    }
}