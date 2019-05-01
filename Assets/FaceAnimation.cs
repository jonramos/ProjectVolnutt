using System.Collections;
using UnityEngine;

public class FaceAnimation : MonoBehaviour {
    public Texture faceTexture;
    public Texture winkingTexture;
    public GameObject head;

    Renderer m_Renderer;

    void Start() {
        m_Renderer = head.GetComponent<Renderer>();
        StartCoroutine(WinkingAnimation());

    }

    IEnumerator WinkingAnimation() {
        while(true) {
            m_Renderer.material.SetTexture("_MainTex", winkingTexture);
            yield return new WaitForSeconds(.1f);
            m_Renderer.material.SetTexture("_MainTex", faceTexture);
            yield return new WaitForSeconds(2f);
        }
    }
}
