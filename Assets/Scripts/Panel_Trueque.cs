// PanelTrueque.cs  (version completa TMP - lista de ofertas, PASO 5)
// Va en el Panel de UI. Crea una fila por cada oferta del aldeano.
using UnityEngine;
using UnityEngine.UI;
using TMPro;                 // <-- necesario para TextMeshPro

public class PanelTrueque : MonoBehaviour
{
    public static PanelTrueque Instancia;

    [Header("Referencias de UI")]
    public GameObject panel;             // el Panel (desactivado al inicio)
    public Transform contenedorOfertas;  // objeto vacio con Vertical Layout Group
    public GameObject prefabOferta;      // el prefab con el script BotonOferta
    public TMP_Text tituloTxt;           // nombre del aldeano (opcional)
    public TMP_Text oroTxt;              // muestra el oro del jugador (opcional)

    private Aldeano aldeanoActual;
    private InventarioJugador jugadorActual;

    void Awake() => Instancia = this;

    public void Abrir(Aldeano aldeano, InventarioJugador jugador)
    {
        aldeanoActual = aldeano;
        jugadorActual = jugador;
        panel.SetActive(true);
        if (tituloTxt) tituloTxt.text = aldeano.nombreAldeano;

        // --- PASO 5: generar una fila por cada oferta ---
        foreach (Transform hijo in contenedorOfertas)   // limpia las anteriores
            Destroy(hijo.gameObject);

        foreach (Oferta o in aldeano.ofertas)
        {
            GameObject fila = Instantiate(prefabOferta, contenedorOfertas);
            fila.GetComponent<BotonOferta>().Configurar(o);
        }
        RefrescarOro();
    }

    public void Cerrar() => panel.SetActive(false);

    public void Comprar(Oferta oferta)
    {
        if (jugadorActual.TieneOro(oferta.precioOro))
        {
            jugadorActual.GastarOro(oferta.precioOro);
            jugadorActual.AgregarItem(oferta.item, oferta.cantidad);
            RefrescarOro();
        }
        else Debug.Log("\u00a1No tienes suficiente oro!");
    }

    void RefrescarOro()
    {
        if (oroTxt && jugadorActual) oroTxt.text = "Oro: " + jugadorActual.oro;
    }
}
