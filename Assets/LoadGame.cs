using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

    public void LoadFirstLevel() {

        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
}
