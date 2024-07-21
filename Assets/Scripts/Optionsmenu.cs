using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenuController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    private const string VolumeKey = "MasterVolume";

    private void Start()
    {
        // Load the saved volume setting on start
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0f);
        SetVolume(savedVolume);
        volumeSlider.value = savedVolume;
    }

    public void SetVolume(float volume)
    {
        // Convert the volume slider value to decibels
        float dbVolume = Mathf.Log10(volume) * 20f;

        // Set the volume level in the Audio Mixer
        audioMixer.SetFloat("MasterVolume", dbVolume);

        // Save the volume setting
        SaveVolumeSetting(volume);
    }

    private void SaveVolumeSetting(float volume)
    {
        // Save the volume setting in PlayerPrefs
        PlayerPrefs.SetFloat(VolumeKey, volume);
    }

    public void BackToMainMenu()
    {
        // Load the main menu
        SceneManager.LoadScene("Main Menu");
    }
}
