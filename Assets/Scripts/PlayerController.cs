using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In m/s")][SerializeField] float controlSpeed = 10f;
    [Tooltip("In m")] [SerializeField] float xRange = 4.5f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;

    [SerializeField] GameObject blackScreenWin;

    [Header("Firing")]
    [SerializeField] GameObject[] guns;

    [Header("Screen-Position Based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 5f;

    [Header("Control-Throe Based")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;

    [Header("other")]
    [SerializeField] GameObject bulletCountCanvas;
    [SerializeField] GameObject timeCanvas;

    int bulletCount = 1000;
    int time = 30;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    private void Start()
    {
        Application.targetFrameRate = 60;
        Invoke("LevelCompleted", 95f);

        InvokeRepeating("TimeUpdate", 1f, 1f);
    }

    // Update is called once per frame
    void Update ()
    {
        if(isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "timePlus")
        {
            time = 31;
            TimeUpdate();
            Destroy(other.gameObject);
            ChangeColor(timeCanvas, time, 10);
        }

        if (other.tag == "bulletPlus")
        {
            bulletCount += 200;
            bulletCountCanvas.GetComponent<Text>().text = bulletCount.ToString();
            Destroy(other.gameObject);
            ChangeColor(bulletCountCanvas, bulletCount, 200);
        }
    }
    private void TimeUpdate()
    {
        time--;
        ChangeColor(timeCanvas, time, 10);

        if (time >= 0)
        {
            timeCanvas.GetComponent<Text>().text = time.ToString();
        }

        if (time <= 0)
        {
            transform.GetComponent<CollisionHandler>().StartDeathSequence(true);
        }
    }

    private void ChangeColor(GameObject go, int currentValue, int valueMin)
    {
        if (currentValue < valueMin)
        {
            go.GetComponent<Text>().color = Color.red;
            go.transform.parent.GetComponent<Image>().color = Color.red;
        }
        else
        {
            go.GetComponent<Text>().color = Color.white;
            go.transform.parent.GetComponent<Image>().color = Color.white;
        }
    }

    private void LevelCompleted()
    {
        blackScreenWin.gameObject.SetActive(true);
        Invoke("LoadSplash", 5f);
    }

    private void LoadSplash()
    {
        SceneManager.LoadScene("Splash Screen");
    }

    private void OnPlayerDeath() //called by string reference
    {
        isControlEnabled = false;
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float controlDueToPosition = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + controlDueToPosition;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float yOffset = yThrow * controlSpeed * Time.deltaTime;

        float rawXPosition = transform.localPosition.x + xOffset;
        float rawYPosition = transform.localPosition.y + yOffset;

        float clampedXPosition = Mathf.Clamp(rawXPosition, -xRange, xRange);
        float clampedYPosition = Mathf.Clamp(rawYPosition, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPosition, clampedYPosition, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire") && bulletCount > 0)
        {
            SetGunsActive(true);
            bulletCount--;
            bulletCountCanvas.GetComponent<Text>().text = bulletCount.ToString();

            ChangeColor(bulletCountCanvas, bulletCount, 200);


        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            //gun.SetActive(isActive);
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
