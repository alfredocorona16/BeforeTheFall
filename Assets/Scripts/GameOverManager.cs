using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    [Header("Referencias de UI")]
    public GameObject panelGameOver;     // Arrastra aquí tu objeto MenuGameOver
    public TMP_Text textoFraseMuerte;    // El texto de la frase roja estilo DMC

    [Header("Frases Épicas")]
    public string[] frasesDMC = {
        "Abandon All Hope...",
        "The last judgment draweth nigh...",
        "El ciclo se desvanece...",
        "La oscuridad reclama el camino..."
    };

    private bool estaMuerto = false;

    void Start()
    {
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(false); // Apaga el panel al arrancar el juego
        }
    }

    public void ActivarGameOver()
    {
        if (estaMuerto) return;
        estaMuerto = true;

        StartCoroutine(RutinaGameOver());
    }

    IEnumerator RutinaGameOver()
    {
        // 1. Pausa el tiempo del juego para que todo se detenga de forma dramática
        Time.timeScale = 0f;

        // 2. Selecciona una frase al azar
        if (textoFraseMuerte != null && frasesDMC.Length > 0)
        {
            int index = Random.Range(0, frasesDMC.Length);
            textoFraseMuerte.text = frasesDMC[index];
        }

        // 3. Activa el panel visualmente (como este script sí está activo, la corrutina no fallará)
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
        }

        yield return null;
    }

    // Métodos para los botones del menú
    public void BotonReintentar()
    {
        Time.timeScale = 1f; // Restaurar el tiempo normal antes de recargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BotonMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}