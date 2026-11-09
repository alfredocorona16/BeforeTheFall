using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HistoriaManager : MonoBehaviour
{
    [Header("Referencias")]
    public Image imagenVinieta;
    public Sprite[] vinietas;
    public CanvasGroup fade;
    public GameObject botonIniciar;

    [Header("Audio")]
    public AudioSource audioSource;      // el Audio Source del objeto
    public AudioClip[] narraciones;      // una narración por viñeta (mismo orden)

    [Header("Tiempos")]
    public float segundosPorVinieta = 60f;
    public float duracionFade = 1f;

    void Start()
    {
        if (botonIniciar != null) botonIniciar.SetActive(false);
        StartCoroutine(ReproducirHistoria());
    }

    IEnumerator ReproducirHistoria()
    {
        for (int i = 0; i < vinietas.Length; i++)
        {
            imagenVinieta.sprite = vinietas[i];
            if (fade != null) yield return Fade(0f, 1f);

            // Reproduce la narración de esta viñeta
            if (audioSource != null && narraciones != null &&
                i < narraciones.Length && narraciones[i] != null)
            {
                audioSource.Stop();
                audioSource.clip = narraciones[i];
                audioSource.Play();
            }

            bool esUltima = (i == vinietas.Length - 1);
            if (esUltima)
            {
                if (botonIniciar != null) botonIniciar.SetActive(true);
                yield break;
            }

            // Espera: lo que dure la narración (o el tiempo mínimo), permite saltar
            float duracion = (audioSource != null && audioSource.clip != null)
                             ? audioSource.clip.length
                             : segundosPorVinieta;

            float t = 0f;
            while (t < duracion)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    if (audioSource != null) audioSource.Stop();  // corta la voz al saltar
                    break;
                }
                t += Time.deltaTime;
                yield return null;
            }

            if (fade != null) yield return Fade(1f, 0f);
        }
    }

    public void IniciarJuego()
    {
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator Fade(float desde, float hasta)
    {
        float t = 0f;
        while (t < duracionFade)
        {
            t += Time.deltaTime;
            fade.alpha = Mathf.Lerp(desde, hasta, t / duracionFade);
            yield return null;
        }
        fade.alpha = hasta;
    }
}