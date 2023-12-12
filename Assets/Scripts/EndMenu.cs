using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public AudioSource endSound;

    private void Start()
    {
        endSound = GetComponent<AudioSource>();
    }
    public void EndGame()
    {
        endSound.Play();
        Application.Quit(); // Exit
    }


}
