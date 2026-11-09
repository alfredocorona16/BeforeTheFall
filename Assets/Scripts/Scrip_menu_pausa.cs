using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPausaManager : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject objetoMenuPausa; // El objeto 'menu' completo o su panel principal
    public GameObject panelOpciones;   // El panel de volumen y opciones

    [Header("Audio")]
    public Slider sliderVolumen;       // El slider para ajustar el volumen

    private bool juegoPausado = false;

    void Start()
    {
        // Asegurarnos de que el menú de pausa y opciones inicien apagados
        if (objetoMenuPausa != null) objetoMenuPausa.SetActive(false);
        if (panelOpciones != null) panelOpciones.SetActive(false);

        // Configurar el slider con el volumen actual de Unity
        if (sliderVolumen != null)
        {
            sliderVolumen.value = AudioListener.volume;
            sliderVolumen.onValueChanged.AddListener(CambiarVolumen);
        }
    }

    void Update()
    {
        // Detectar si el jugador presiona la tecla Escape (ESC)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Pausar()
    {
        juegoPausado = true;
        if (objetoMenuPausa != null) objetoMenuPausa.SetActive(true);
        Time.timeScale = 0f; // Congela el tiempo del juego
    }

    public void Reanudar()
    {
        juegoPausado = false;
        if (objetoMenuPausa != null) objetoMenuPausa.SetActive(false);
        if (panelOpciones != null) panelOpciones.SetActive(false); // Cierra opciones también si está abierto
        Time.timeScale = 1f; // Reanuda el tiempo normal
    }

    // Funciones para conectar con los botones
    public void AbrirOpciones()
    {
        if (panelOpciones != null) panelOpciones.SetActive(true);
        if (objetoMenuPausa != null) objetoMenuPausa.SetActive(false); // Oculta el menú principal de pausa temporalmente
    }

    public void CerrarOpciones()
    {
        if (panelOpciones != null) panelOpciones.SetActive(false);
        if (objetoMenuPausa != null) objetoMenuPausa.SetActive(true); // Vuelve al menú de pausa
    }

    public void CambiarVolumen(float valor)
    {
        // Cambia el volumen global de todo el juego (de 0.0 a 1.0)
        AudioListener.volume = valor;
    }

    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}