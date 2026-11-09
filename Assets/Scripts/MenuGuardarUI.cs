using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuGuardarUI : MonoBehaviour
{
    [Header("Paneles de Menú")]
    public GameObject panelMenuPausa;               // El panel de botones (Continuar, Opciones, Guardar Partida)
    public GameObject panelGuardar;                 // El panel que contiene el InputField para escribir el nombre de la partida

    [Header("UI Guardar")]
    public TMP_InputField campoNombreGuardado;
    public GameObject panelAdvertencia;

    private string nombreTemporal;

    void Start()
    {
        // Ocultar la advertencia al iniciar
        if (panelAdvertencia!= null)
            panelAdvertencia.SetActive(false);
    }

    // ==========================================
    // NAVEGACIÓN DE PANELES
    // ==========================================

    // Enlazar este método al botón "Guardar Partida" del Menú de Pausa
    public void AbrirPanelGuardar()
    {
        if (panelMenuPausa != null) panelMenuPausa.SetActive(false);
        if (panelGuardar != null) panelGuardar.SetActive(true);
    }

    // Enlazar este método al botón "Cancelar" o "Regresar" del Panel de Guardar
    public void CancelarYVolverAlMenu()
    {
        if (panelGuardar != null) panelGuardar.SetActive(false);
        if (panelAdvertencia != null) panelAdvertencia.SetActive(false);
        if (panelMenuPausa != null) panelMenuPausa.SetActive(true);
    }

    // ==========================================
    // LÓGICA DE GUARDADO
    // ==========================================

    // Método para el botón "Guardar" (dentro de la ventana de guardado)
    public void IntentarGuardar()
    {
        if (campoNombreGuardado != null)
            nombreTemporal = campoNombreGuardado.text.Trim();

        if (string.IsNullOrEmpty(nombreTemporal))
        {
            nombreTemporal = "BeforeTheFall_Save"; // Nombre por defecto si lo dejan vacío
        }

        // Comprobar si ya existe
        if (SaveSystem.ExistePartida(nombreTemporal))
        {
            // Mostrar mensaje de advertencia
            if (panelAdvertencia != null)
                panelAdvertencia.SetActive(true);
        }
        else
        {
            // Si no existe, guardar directamente
            ConfirmarGuardado();
        }
    }

    // Conectar este botón a "Sí, Sobreescribir" en el panel de advertencia
    public void ConfirmarGuardado()
    {
        if (panelAdvertencia != null)
            panelAdvertencia.SetActive(false);

        // Obtener datos del juego (ejemplo con monedas de CoinManager y vida)
        int monedas = CoinManager.Instance != null ? CoinManager.Instance.contadorMonedas : 0;

        // Intentar obtener la vida actual desde el HUDManager
        float vida = 100f;
        HUDManager hud = FindObjectOfType<HUDManager>();
        if (hud != null)
        {
            // Si tienes una variable de vida en HUDManager la toma aquí
            // vida = hud.vidaActual;
        }

        SaveSystem.Guardar(nombreTemporal, SceneManager.GetActiveScene().name, monedas, vida);
        Debug.Log("Partida guardada exitosamente con el nombre: " + nombreTemporal);

        // Opcional: Volver al menú de pausa tras guardar con éxito
        CancelarYVolverAlMenu();
    }

    public void CancelarSobreescritura()
    {
        if (panelAdvertencia != null)
            panelAdvertencia.SetActive(false);
    }
}