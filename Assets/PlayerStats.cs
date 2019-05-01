using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {
    public int hp = 10;
    public bool highDamage;
    public bool simpleDamage;
    public GameObject screenFade;

    public AudioClip dmgSFX;
    private AudioSource audioSource;
    public bool hitByExplosion, collidingWithWall, gameOver;
    private Animator animator, screenAnimator;

    void Start() {
        animator = GetComponent<Animator>();

        screenAnimator = screenFade.GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update() {

        if(hitByExplosion) {
            GetHighDamage(gameObject);
            hitByExplosion = false;
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Enemy")) {
            GetDamage(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Wall")) {
            collidingWithWall = true;
        }

        if(hp <= 0) {

            if(!gameOver)
                StartCoroutine(LoadGameOver());

        }
    }

    private void OnCollisionExit(Collision collision) {
        if(collision.gameObject.CompareTag("Wall")) {
            collidingWithWall = false;
        }
    }

    private void GetDamage(GameObject obj) {

        //If a power is not set, the default is 1 (enemy touch)
        hp -= obj.GetComponent<EnemyStats>() ? obj.GetComponent<EnemyStats>().power : 1;
        audioSource.PlayOneShot(dmgSFX, .7f);
        animator.SetTrigger("Damage");
    }
    private void GetHighDamage(GameObject obj) {

        //If a power is not set, the default is 1 (enemy touch)
        hp -= obj.GetComponent<EnemyStats>() ? obj.GetComponent<EnemyStats>().power : 1;
        audioSource.PlayOneShot(dmgSFX, .7f);
        animator.SetTrigger("HighDamage");
        StartCoroutine(BackMovement());
    }
    IEnumerator BackMovement() {
        GetComponent<CharacterController>().enabled = false;
        var position = transform.position - new Vector3(0f, 0f, 5f);
        while(transform.position.z > position.z) {
            if(!collidingWithWall) {
                transform.Translate(0, 0f, -1f);
            }
            yield return new WaitForFixedUpdate();
        }
        GetComponent<CharacterController>().enabled = true;
    }

    IEnumerator LoadGameOver() {
        gameOver = true;
        audioSource.PlayOneShot(dmgSFX);
        animator.Play("Die");
        GetComponent<PlayerController>().enabled = false;
        GetComponent<CharacterController>().enabled = false;
        GetComponent<BusterWeapon>().enabled = false;

        yield return new WaitForSeconds(2);

        screenFade.GetComponent<Image>().enabled = true;
        screenAnimator.Play("FadeOut");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);

    }
}
