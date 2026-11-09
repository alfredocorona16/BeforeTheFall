using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeletransporteFade : MonoBehaviour
{
    [Header("Destino")]
    public Transform puntoDeDestino; // El punto vacío dentro de la casa donde aparecerá el jugador

    [Header("Configuración de Fade")]
    public CanvasGroup canvasGroupFade; // Arrastra aquí el CanvasGroup del PanelTransicion
    public float velocidadFade = 2f;    // Velocidad con la que se oscurece y aclara

    private bool enTransicion = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el que entra es el jugador y si no hay otra transición en curso
        if (collision.CompareTag("Player") && !enTransicion)
        {
            StartCoroutine(RutinaTransicion(collision.transform));
        }
    }

    IEnumerator RutinaTransicion(Transform jugador)
    {
        enTransicion = true;

        // 1. FADE OUT: Oscurecer la pantalla (Alpha de 0 a 1)
        yield return StartCoroutine(Fade(0f, 1f));

        // 2. TELETRANSPORTE: Mover al jugador a la posición de la casa
        jugador.position = puntoDeDestino.position;

        // Pequeña pausa opcional en negro para que respire la escena
        yield return new WaitForSeconds(0.15f);

        // 3. FADE IN: Aclarar la pantalla de vuelta (Alpha de 1 a 0)
        yield return StartCoroutine(Fade(1f, 0f));

        enTransicion = false;
    }

    IEnumerator Fade(float alfaInicial, float alfaFinal)
    {
        float tiempoTranscurrido = 0f;
        float duracion = 1.0f / velocidadFade;

        if (canvasGroupFade != null)
        {
            canvasGroupFade.alpha = alfaInicial;

            while (tiempoTranscurrido < duracion)
            {
                tiempoTranscurrido += Time.deltaTime;
                canvasGroupFade.alpha = Mathf.Lerp(alfaInicial, alfaFinal, tiempoTranscurrido / duracion);
                yield return null;
            }

            canvasGroupFade.alpha = alfaFinal;
        }
    }
}