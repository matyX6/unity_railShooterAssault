using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")] [SerializeField] float sceneLoadDelay = 4f;
    [Tooltip("FX prefab on player")] [SerializeField] GameObject deathFX;

    [SerializeField] GameObject timeUp;
    [SerializeField] GameObject youDied;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "timePlus" && other.tag != "bulletPlus")
        {
            StartDeathSequence(false);
        }
    }

    public void StartDeathSequence(bool isTimeUp)
    {
        SendMessage("OnPlayerDeath");
        transform.GetComponent<PlayerController>().MakeGameNotActive();

        MakePlayerExplode();
        Invoke("MakePlayerDisappear", 1f);

        Invoke("ReloadScene", sceneLoadDelay);

        if (isTimeUp)
        {
            timeUp.gameObject.SetActive(true);
        }
        else
        {
            youDied.gameObject.SetActive(true);
        }
    }

    private void MakePlayerDisappear()
    {
        gameObject.SetActive(false);
    }
    private void MakePlayerExplode()
    {
        deathFX.SetActive(true); //activating explosion
        //gameObject.GetComponent<Renderer>().enabled = false; //make player ship invisible
        //gameObject.SetActive(false); //deactivating player object

    }

    private void ReloadScene() //string referenced
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Splash Screen");
    }
}
