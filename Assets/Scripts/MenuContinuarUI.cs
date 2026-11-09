using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class MenuContinuarUI : MonoBehaviour
{
    [Header("Paneles de Navegación")]
    public GameObject panelMenuPausa;      // El menú principal de pausa (Continuar, Opciones, Guardar, etc.)
    public GameObject panelContinuarUI;    // El panel actual que contiene el ScrollView de las partidas

    [Header("Referencias UI")]
    public Transform contenedorBotones;    // El 'Content' del ScrollView
    public GameObject prefabBotonPartida;  // Prefab de un botón con texto TMP
    public TMP_Text textoMensajeVacio;     // Opcional: Un texto que diga "No hay partidas guardadas"

    // Este método se vincula al botón "Continuar" del menú de pausa
    public void AbrirPanelContinuar()
    {
        if (panelMenuPausa != null) panelMenuPausa.SetActive(false);
        if (panelContinuarUI != null) panelContinuarUI.SetActive(true);

        CargarListaDePartidas();
    }

    // Este método se vincula al botón "Regresar" o "Cancelar" dentro de este panel
    public void CerrarPanelContinuar()
    {
        if (panelContinuarUI != null) panelContinuarUI.SetActive(false);
        if (panelMenuPausa != null) panelMenuPausa.SetActive(true);
    }

    void OnEnable()
    {
        // Por si se activa mediante código en lugar del botón
        CargarListaDePartidas();
    }

    public void CargarListaDePartidas()
    {
        // Validación de seguridad para que no de error si falta el prefab
        if (prefabBotonPartida == null || contenedorBotones == null)
        {
            Debug.LogError("Falta asignar el Prefab del Botón o el Contenedor en el Inspector de MenuContinuarUI.");
            return;
        }

        // Limpiar lista anterior
        foreach (Transform child in contenedorBotones)
        {
            Destroy(child.gameObject);
        }

        List<DatosPartida> partidas = SaveSystem.ObtenerTodasLasPartidas();

        if (partidas == null || partidas.Count == 0)
        {
            if (textoMensajeVacio != null)
            {
                textoMensajeVacio.gameObject.SetActive(true);
                textoMensajeVacio.text = "No hay partidas guardadas.";
            }
            return;
        }

        if (textoMensajeVacio != null)
        {
            textoMensajeVacio.gameObject.SetActive(false);
        }

        foreach (DatosPartida datos in partidas)
        {
            GameObject nuevoBoton = Instantiate(prefabBotonPartida, contenedorBotones);

            TMP_Text textoBoton = nuevoBoton.GetComponentInChildren<TMP_Text>();
            if (textoBoton != null)
            {
                textoBoton.text = $"{datos.nombreGuardado} - [{datos.fechaGuardado}]";
            }

            Button btn = nuevoBoton.GetComponent<Button>();
            if (btn != null)
            {
                string escenaACargar = datos.nombreEscena; // Capturamos la variable localmente
                btn.onClick.AddListener(() => CargarEscenaGuardada(escenaACargar));
            }
        }
    }

    void CargarEscenaGuardada(string nombreEscena)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreEscena);
    }
}