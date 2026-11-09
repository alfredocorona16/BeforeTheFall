using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [Header("Referencias de UI")]
    public Slider barraDeVida;
    public TMP_Text textoOro;
    public Image imagenAvatar;

    [Header("Estadísticas del Jugador")]
    public float vidaMaxima = 100f;
    private float vidaActual;
    private int oroActual = 0;

    [Header("Gestor de Muerte")]
    public GameOverManager gameOverManager;

    void Start()
    {
        vidaActual = vidaMaxima;
        barraDeVida.maxValue = vidaMaxima;
        barraDeVida.value = vidaActual;
        ActualizarTextoOro();
    }

    public void ModificarVida(float cantidad)
    {
        vidaActual += cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        barraDeVida.value = vidaActual;

        if (vidaActual <= 0)
        {
            MuerteJugador();
        }
    }

    public void ModificarOro(int cantidad)
    {
        oroActual += cantidad;
        ActualizarTextoOro();
    }

    void ActualizarTextoOro()
    {
        if (textoOro != null)
        {
            textoOro.text = "🪙 " + oroActual;
        }
    }

    void MuerteJugador()
    {
        Debug.Log("Quetzalcóatl ha caído...");

        // Oculta al jugador
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            jugador.SetActive(false);
        }

        // Activa la cinemática/menú estilo Devil May Cry
        if (gameOverManager != null)
        {
            gameOverManager.ActivarGameOver();
        }
        else
        {
            // Respaldo por si acaso: recarga directa si no se asignó el manager
            Invoke("ReiniciarNivel", 1.5f);
        }
    }

    void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}