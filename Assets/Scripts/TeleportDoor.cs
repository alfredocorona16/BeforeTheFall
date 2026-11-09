using UnityEngine;

public class TeleportDoor : MonoBehaviour
{
    [Header("Configuración del Teletransporte")]
    [Tooltip("Arrastra aquí el Transform del punto donde debe aparecer el personaje")]
    public Transform puntoDestino;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica que lo que entró a la puerta sea el Jugador
        if (other.CompareTag("Player"))
        {
            // Mueve al jugador a la posición exacta del destino
            other.transform.position = puntoDestino.position;

            Debug.Log("Jugador teletransportado a: " + puntoDestino.name);
        }
    }
}