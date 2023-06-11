using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuSettings : MonoBehaviour {
    public AudioMixer audioMixer;
    public void SetVolume (float volumeMixer) {
        audioMixer.SetFloat("volumeMixer", volumeMixer);
        Debug.Log("Volume is now settled to : " +volumeMixer);
    }
}