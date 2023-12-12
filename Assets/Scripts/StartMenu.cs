using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public AudioSource clickSound;

    public Button startButton;
    private void Start()
    {
        clickSound = GetComponent<AudioSource>();
        startButton = GetComponent<Button>();
    }
    public void StartGame()
    {
        startButton.enabled = false;
        clickSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
