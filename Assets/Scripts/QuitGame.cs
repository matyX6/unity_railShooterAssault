using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class QuitGame : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update ()
    {
        HandleBackButton();
    }

    private static void HandleBackButton()
    {
        if (CrossPlatformInputManager.GetButtonDown("Exit"))
        {
            if (SceneManager.GetActiveScene().name == "Splash Screen")
            {
                Application.Quit();
            }
            else
            {
                SceneManager.LoadScene("Splash Screen");
            }
        }
    }
}
