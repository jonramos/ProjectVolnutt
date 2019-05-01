using UnityEngine;

public class MouseOverSFX : MonoBehaviour {

    public GameObject soundManager;
    public AudioClip mouseOverSFX;
    public AudioClip clikSFX;

    public GameObject StartButton;
    public GameObject ControlsButton;

    public GameObject Controls;

    private AudioSource audioSource;


    private void Start() {
        audioSource = soundManager.GetComponent<AudioSource>();

    }

    public void PlaySFX() {
        audioSource.PlayOneShot(mouseOverSFX, .7f);
    }
    public void ClickSFX() {
        audioSource.PlayOneShot(clikSFX, .7f);
    }
    public void ShowControls() {
        StartButton.SetActive(false);
        ControlsButton.SetActive(false);
        Controls.SetActive(true);
    }

    public void Back() {
        StartButton.SetActive(true);
        ControlsButton.SetActive(true);
        Controls.SetActive(false);
    }
}
