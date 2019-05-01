using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour {

    private bool PlayerIsNear;
    private AudioSource audioSource;
    public GameObject rightDoor;
    public GameObject leftDoor;
    public bool loadNextLevel;
    public AudioClip doorSFX;
    public GameObject screenFade;
    public GameObject Player;

    private Animator animator;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        animator = screenFade.GetComponent<Animator>();

    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            PlayerIsNear = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            PlayerIsNear = false;
        }
    }
    // Update is called once per frame
    void Update() {
        if(PlayerIsNear) {
            if(Input.GetKeyDown(KeyCode.F)) {
                StartCoroutine(DoorOpening());
                if(loadNextLevel) {
                    Player.GetComponent<CharacterController>().enabled = false;
                    screenFade.GetComponent<Image>().enabled = true;
                    animator.Play("FadeOut");
                    StartCoroutine(LoadLevel());
                }

            }
        }
    }

    IEnumerator LoadLevel() {

        yield return new WaitForSeconds(2f);
        Player.transform.position = new Vector3(-3f, 0.45f, -0.40f);
        Player.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        Player.GetComponent<CharacterController>().enabled = true;
        SceneManager.LoadScene("Level2", LoadSceneMode.Single);
    }
    private IEnumerator DoorOpening() {
        audioSource.PlayOneShot(doorSFX);
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
