using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour {

    public GameObject FadeIn;

    private Animator animator;

    private void Start() {
        animator = FadeIn.GetComponent<Animator>();
    }
    public void LoadFirstLevel() {
        FadeIn.GetComponent<Image>().enabled = true;
        animator.Play("FadeOut");
        StartCoroutine(LoadLevel());
    }
    IEnumerator LoadLevel() {

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
}
