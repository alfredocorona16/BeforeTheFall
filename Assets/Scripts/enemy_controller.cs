using UnityEngine;

public class EnemigoIA : MonoBehaviour
{
    [Header("Movimiento y Patrulla")]
    public float velocidadMovimiento = 3f;
    public Transform[] puntosPatrulla;
    public float tiempoEsperaEnPunto = 1f; // Tiempo que se detiene en cada punto
    private int indicePuntoActual = 0;
    private float temporizadorEspera = 0f;

    [Header("Detección y Persecución (VisionZone)")]
    [HideInInspector] public bool jugadorDetectado = false;
    private Transform jugador;

    [Header("Sistema de Vida")]
    public float vidaMaxima = 50f;
    private float vidaActual;

    [Header("Sistema de Daño")]
    public float dañoAlJugador = 5f;
    public float tiempoEntreDaños = 3f;
    private float tiempoSiguienteDaño = 0f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        vidaActual = vidaMaxima;

        GameObject objJugador = GameObject.FindGameObjectWithTag("Player");
        if (objJugador != null)
        {
            jugador = objJugador.transform;
        }
    }

    void Update()
    {
        // Si la VisionZone detectó al jugador, lo persigue
        if (jugadorDetectado && jugador != null)
        {
            PerseguirJugador();
        }
        else
        {
            Patrullar();
        }
    }

    void Patrullar()
    {
        if (puntosPatrulla.Length == 0) return;

        Transform objetivoActual = puntosPatrulla[indicePuntoActual];

        // Comprobar si ya llegó al punto actual con un margen de tolerancia (0.2f)
        float distanciaAlPunto = Vector2.Distance(transform.position, objetivoActual.position);

        if (distanciaAlPunto < 0.2f)
        {
            // Detenerse un momento en el punto antes de ir al siguiente
            temporizadorEspera += Time.deltaTime;
            if (temporizadorEspera >= tiempoEsperaEnPunto)
            {
                indicePuntoActual = (indicePuntoActual + 1) % puntosPatrulla.Length;
                temporizadorEspera = 0f;
            }
        }
        else
        {
            // Moverse hacia el punto de patrulla de forma suave y controlada
            transform.position = Vector2.MoveTowards(transform.position, objetivoActual.position, velocidadMovimiento * Time.deltaTime);
            GirarSprite(objetivoActual.position.x - transform.position.x);
        }
    }

    void PerseguirJugador()
    {
        transform.position = Vector2.MoveTowards(transform.position, jugador.position, velocidadMovimiento * Time.deltaTime);
        GirarSprite(jugador.position.x - transform.position.x);
    }

    void GirarSprite(float direccionX)
    {
        if (spriteRenderer != null)
        {
            if (direccionX > 0.05f)
                spriteRenderer.flipX = false; // Derecha
            else if (direccionX < -0.05f)
                spriteRenderer.flipX = true;  // Izquierda
        }
    }

    // Métodos públicos para conectar con la VisionZone
    public void SetJugadorDetectado(bool detectado)
    {
        jugadorDetectado = detectado;
    }

    public void RecibirDaño(float cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time >= tiempoSiguienteDaño)
        {
            HUDManager hud = FindObjectOfType<HUDManager>();
            if (hud != null)
            {
                hud.ModificarVida(-dañoAlJugador);
            }
            tiempoSiguienteDaño = Time.time + tiempoEntreDaños;
        }
    }
}