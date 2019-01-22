using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour {
    public AudioClip music;

    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.Play();
    }
}