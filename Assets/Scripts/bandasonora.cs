using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        // Reproducir si no está sonando
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void DetenerMusica()
    {
        audioSource.Stop();
    }

    public void CambiarVolumen(float nuevoVolumen)
    {
        audioSource.volume = nuevoVolumen;
    }
}