using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")] [SerializeField] float sceneLoadDelay = 2f;
    [Tooltip("FX prefab on player")] [SerializeField] GameObject deathFX;

    void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");

        MakePlayerExplosde();

        Invoke("ReloadScene", sceneLoadDelay);
    }

    private void MakePlayerExplosde()
    {
        deathFX.SetActive(true); //activating explosion
        //gameObject.GetComponent<Renderer>().enabled = false; //make player ship invisible
        //gameObject.SetActive(false); //deactivating player object
    }

    private void ReloadScene() //string referenced
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}
