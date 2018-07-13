using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour {

    float fadingDuration = 2f;

	void Start ()
    {
        TextFadeOut();
    }

    void Update()
    {
        LoadNextScene();
    }

    private static void LoadNextScene()
    {
        if (Input.anyKey)
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneIndex + 1);
        }
    }

    private void TextFadeIn()
    {
        Text text = GetComponent<Text>();
        text.CrossFadeAlpha(1f, fadingDuration, false);

        Invoke("TextFadeOut", fadingDuration);
    }

    private void TextFadeOut()
    {
        Text text = GetComponent<Text>();
        text.CrossFadeAlpha(0f, fadingDuration, false);

        Invoke("TextFadeIn", fadingDuration);
    }
}
