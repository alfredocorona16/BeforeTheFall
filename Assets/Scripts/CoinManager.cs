using UnityEngine;
using TMPro; // Necesario para usar TextMeshPro

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance; // Instancia global para acceder fácil desde cualquier script

    public int contadorMonedas = 0;
    public TextMeshProUGUI textoContador; // Arrastra aquí tu texto de la UI

    void Awake()
    {
        // Configura el Singleton para que el GameManager esté disponible globalmente
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ActualizarTexto();
    }

    public void SumarMonedas(int cantidad)
    {
        contadorMonedas += cantidad;
        ActualizarTexto();
    }

    void ActualizarTexto()
    {
        if (textoContador != null)
        {
            textoContador.text = "Oro: " + contadorMonedas;
        }
    }

    // Función para descontar monedas al realizar una compra
    public bool GastarMonedas(int cantidad)
    {
        if (contadorMonedas >= cantidad)
        {
            contadorMonedas -= cantidad;
            ActualizarTexto();
            return true; // Compra exitosa
        }
        else
        {
            Debug.Log("¡Oro insuficiente!");
            return false; // No tiene suficiente oro
        }
    }

    // O si prefieres una función genérica que sirva tanto para sumar como para restar:
    public void ModificarMonedas(int cantidad)
    {
        contadorMonedas += cantidad;

        // Asegurar que el oro nunca sea menor a 0
        if (contadorMonedas < 0)
        {
            contadorMonedas = 0;
        }

        ActualizarTexto();
    }
}