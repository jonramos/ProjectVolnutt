using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuToogle : MonoBehaviour {

    public GameObject startButton;
    public GameObject playerCamera;
    public GameObject controlsButton;
    public AudioClip buttonSFX;
    public float winkingDelay = .5f;

    private Text text;
    private bool routineActivated;
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        text = GetComponent<Text>();
        StartCoroutine(WinkingAnimation());
    }
    void Update() {
        if(Input.anyKey && !routineActivated) {
            StopAllCoroutines();
            StartCoroutine(Enter());

        }
    }
    IEnumerator WinkingAnimation() {
        while(true) {
            text.enabled = false;
            yield return new WaitForSeconds(winkingDelay);
            text.enabled = true;
            yield return new WaitForSeconds(winkingDelay);
        }
    }
    IEnumerator Enter() {
        routineActivated = true;
        audioSource.PlayOneShot(buttonSFX, .7f);

        GetComponent<Text>().enabled = false;

        startButton.SetActive(true);
        controlsButton.SetActive(true);

        yield return new WaitForSeconds(1f);

        playerCamera.GetComponent<AudioSource>().enabled = true;

        gameObject.SetActive(false);

    }
}
