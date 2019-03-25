using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinkingButton : MonoBehaviour {

    public float winkingDelay = .5f;
    private Text text;
    void Start() {
        text = GetComponent<Text>();
        StartCoroutine(WinkingAnimation());
    }

    // Update is called once per frame
    IEnumerator WinkingAnimation() {
        while(true) {
            text.enabled = false;
            yield return new WaitForSeconds(winkingDelay);
            text.enabled = true;
            yield return new WaitForSeconds(winkingDelay);
        }
    }

}