using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    [Tooltip("In seconds")][SerializeField] float loadDelay = 2f;

    void Start()
    {
        Invoke("LoadNextScene", loadDelay);
    }

    void LoadNextScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex + 1);
    }
}
