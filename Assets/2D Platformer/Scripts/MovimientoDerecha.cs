using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float moveInput;

    void Update()
    {
        // Esto permite moverte tanto con el teclado como con las variables de los botones
        transform.Translate(Vector3.right * moveInput * moveSpeed * Time.deltaTime);
    }

    // Funciones que activarán los botones
    public void MoverDerecha()
    {
        moveInput = 1f; // Se mueve hacia la derecha
    }

    public void MoverIzquierda()
    {
        moveInput = -1f; // Se mueve hacia la izquierda
    }

    public void Detenerse()
    {
        moveInput = 0f; // Se detiene cuando sueltas el botón
    }
}