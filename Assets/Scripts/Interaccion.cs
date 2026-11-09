// InteraccionAldeano.cs  (mejorado)
// Muestra el aviso "[E] Interactuar" al acercarse el jugador y abre el trueque con E.
// IMPORTANTE para que detecte:
//   - El JUGADOR debe tener Tag = "Player", un Collider2D y un Rigidbody2D.
//   - El ALDEANO debe tener un Collider2D con 'Is Trigger' = ON.
//   (En 2D, al menos UNO de los dos necesita Rigidbody2D o el trigger NO dispara.)
using UnityEngine;

public class InteraccionAldeano : MonoBehaviour
{
    public GameObject avisoTecla;   // el objeto "AvisoE" (icono E + texto Interactuar)
    private Aldeano aldeano;
    private InventarioJugador jugadorCerca;

    void Awake()
    {
        aldeano = GetComponent<Aldeano>();
        if (avisoTecla) avisoTecla.SetActive(false);   // oculto al inicio
    }

    void Update()
    {
        if (jugadorCerca != null && Input.GetKeyDown(KeyCode.E))
            PanelTrueque.Instancia.Abrir(aldeano, jugadorCerca);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // busca el inventario en el jugador o en su padre
        jugadorCerca = other.GetComponent<InventarioJugador>();
        if (jugadorCerca == null) jugadorCerca = other.GetComponentInParent<InventarioJugador>();

        if (jugadorCerca == null)
            Debug.LogWarning("El jugador entro pero no tiene el script InventarioJugador.");

        if (avisoTecla) avisoTecla.SetActive(true);    // muestra [E] Interactuar
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        jugadorCerca = null;
        if (avisoTecla) avisoTecla.SetActive(false);
        if (PanelTrueque.Instancia) PanelTrueque.Instancia.Cerrar();
    }
}
