using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip musicLoop;

    void Start()
    {
        if (musicLoop != null)
        {
            musicSource.clip = musicLoop;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}