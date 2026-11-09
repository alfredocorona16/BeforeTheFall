using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaludJugador : MonoBehaviour
{
    [Header("Configuración de Salud")]
    public int saludMaxima = 5;
    public int saludActual;

    [Header("UI Referencias")]
    public Image[] corazones; // Arrastra los sprites de tus corazones
    public Sprite corazonLleno;
    public Sprite corazonVacio;
    public TMP_Text textoOro;

    private int oroActual = 0;

    void Start()
    {
        saludActual = saludMaxima;
        ActualizarUI();
    }

    public void RecibirDano(int cantidad)
    {
        saludActual -= cantidad;
        if (saludActual < 0) saludActual = 0;

        ActualizarUI();

        if (saludActual <= 0)
        {
            Derrota();
        }
    }

    public void Curar(int cantidad)
    {
        saludActual += cantidad;
        if (saludActual > saludMaxima) saludActual = saludMaxima;

        ActualizarUI();
    }

    public void AgregarOro(int cantidad)
    {
        oroActual += cantidad;
        if (textoOro != null)
        {
            textoOro.text = "Oro: " + oroActual;
        }
    }

    void ActualizarUI()
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < saludActual)
            {
                corazones[i].sprite = corazonLleno;
            }
            else
            {
                corazones[i].sprite = corazonVacio;
            }

            // Muestra solo el número de corazones correspondientes a la salud máxima
            if (i < saludMaxima)
            {
                corazones[i].enabled = true;
            }
            else
            {
                corazones[i].enabled = false;
            }
        }
    }

    void Derrota()
    {
        Debug.Log("¡El jugador ha sido derrotado!");
        // Aquí puedes reiniciar el nivel o activar una pantalla de Game Over
    }
}