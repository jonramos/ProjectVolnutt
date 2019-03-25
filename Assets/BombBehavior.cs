using UnityEngine;

public class BombBehavior : MonoBehaviour {


    private void OnCollisionEnter(Collision collision) {

        Destroy(gameObject);
    }

    void Update() {
        transform.Rotate(0, 0, 360f * 3f * Time.deltaTime);
    }

}
