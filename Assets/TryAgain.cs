using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour {

    public AudioClip gameOverSFX;
    public void Start() {
        var obj = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        obj.clip = gameOverSFX;
        obj.loop = false;
        obj.Play();
    }
    public void Retry() {
        var obj = GameObject.FindGameObjectWithTag("Player");
        Destroy(obj);
        SceneManager.LoadScene("Level1");
    }
}
