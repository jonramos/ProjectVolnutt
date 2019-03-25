using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public int hp = 10;
    public bool highDamage;
    public bool simpleDamage;

    public AudioClip dmgSFX;
    private AudioSource audioSource;

    private Animator animator;
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Enemy")) {
            hp -= collision.gameObject.GetComponent<EnemyStats>().power;
            audioSource.PlayOneShot(dmgSFX, .7f);
            animator.SetTrigger("Damage");
        }

        if(hp <= 0) {
            //Game Over
            audioSource.PlayOneShot(dmgSFX, .7f);
            animator.Play("Die");
            GetComponent<PlayerController>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
        }
    }

}
