using UnityEngine;

public class Moneda : MonoBehaviour
{
    [Header("Configuración")]
    public int valorMoneda = 1;

    [Header("Audio (Opcional)")]
    public AudioClip sonidoRecoger;
    [Range(0f, 1f)] public float volumenSonido = 1f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprueba si el objeto que chocó con la moneda es el jugador
        if (collision.CompareTag("Player") || collision.GetComponent<AnimationTranslade>() != null)
        {
            // Valida que el CoinManager exista en la escena antes de usarlo
            if (CoinManager.Instance != null)
            {
                CoinManager.Instance.SumarMonedas(valorMoneda);
            }
            else
            {
                Debug.LogWarning("No se encontró el CoinManager en la escena.");
            }

            // Reproduce el sonido de recolección si está asignado
            if (sonidoRecoger != null)
            {
                AudioSource.PlayClipAtPoint(sonidoRecoger, transform.position, volumenSonido);
            }

            // Destruye el objeto de la moneda de la escena
            Destroy(gameObject);
        }
    }
}