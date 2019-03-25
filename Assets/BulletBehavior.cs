using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    public AudioClip dmgSFX;
    public AudioClip missSFX;

    private AudioSource audioSource;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, 1f);
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Enemy")) {
            audioSource.PlayOneShot(dmgSFX);
            collision.gameObject.GetComponentInParent<EnemyStats>().hp -= 1;
        }

        else {
            audioSource.PlayOneShot(missSFX);
        }
        gameObject.GetComponent<MeshRenderer>().enabled = false;

    }
}
