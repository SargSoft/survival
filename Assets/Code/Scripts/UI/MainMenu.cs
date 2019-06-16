using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour {

	public static string displayVolume = "100%";

    public void PlayGame () {
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame () {
    	Debug.Log("QUIT");
    	Application.Quit();
    }

    public AudioMixer audioMixer;

    public void SetVolume (float volume) {
    	audioMixer.SetFloat("volume", volume);
    }

    public void GetVolume (float volume) {
    	// Debug.Log("Volume is: " + displayVolume);
    	displayVolume = Mathf.FloorToInt((volume + 80) * 5/4) + "%";
    }
}
