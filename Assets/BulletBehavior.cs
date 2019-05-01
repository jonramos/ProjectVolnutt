using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    public AudioClip dmgSFX;
    public AudioClip missSFX;
    public GameObject bulletBurstFX;

    private AudioSource audioSource;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, 1f);
    }

    void OnCollisionEnter(Collision collision) {

        gameObject.GetComponent<MeshRenderer>().enabled = false;

        gameObject.GetComponent<SphereCollider>().enabled = false;

        if(collision.gameObject.CompareTag("Enemy")) {
            audioSource.PlayOneShot(dmgSFX);
            collision.gameObject.GetComponentInParent<EnemyStats>().hp -= 1;
        }

        else {
            audioSource.PlayOneShot(missSFX);
        }
        var position = transform.position;
        var obj = Instantiate(bulletBurstFX, position, transform.rotation);
        Destroy(obj, 1f);
    }
}
