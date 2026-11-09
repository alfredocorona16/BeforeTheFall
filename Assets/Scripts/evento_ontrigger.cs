using UnityEngine;

public class VisionZoneDetector : MonoBehaviour
{
    private EnemigoIA enemigoIA;

    void Start()
    {
        // Busca el script en el objeto padre (el goblin)
        enemigoIA = GetComponentInParent<EnemigoIA>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (enemigoIA != null)
            {
                enemigoIA.SetJugadorDetectado(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (enemigoIA != null)
            {
                enemigoIA.SetJugadorDetectado(false);
            }
        }
    }
}