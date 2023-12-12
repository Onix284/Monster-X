using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused;

    public GameObject mainCamera;
    public GameObject pauseCamera;

    public AudioSource clickSound;

    public Button resumeButton;

    public Button menuButton;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera.SetActive(true); 
        pauseCamera.SetActive(false);
        clickSound = GetComponent<AudioSource>();
        menuButton.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void pauseGame()
    {
        resumeButton.enabled = true;
        menuButton.enabled = true;
        Time.timeScale = 0f;
        isPaused = true;
        mainCamera.SetActive(false);
        pauseCamera.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        mainCamera.SetActive(true);
        pauseCamera.SetActive(false);
        clickSound.Play();
        resumeButton.enabled =false;
        menuButton.enabled = false;


    }
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        clickSound.Play();
        SceneManager.LoadScene(0);
        resumeButton.enabled = false;
        menuButton.enabled=false;
    }
    public void EndGame()
    {
        clickSound.Play();
        Application.Quit();
    }
}

