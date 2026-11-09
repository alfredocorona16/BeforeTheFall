using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemTiendaBoton : MonoBehaviour
{
    [Header("Datos Configurables del Producto")]
    public string nombreItem = "Maiz"; // Puedes cambiar este nombre en el Inspector para cada prefab u oferta
    public int precioItem = 10;

    [Header("Referencias UI de la Oferta")]
    public TMP_Text nombreTxt;
    public TMP_Text precioTxt;
    public Image iconoImg;

    private Button boton;

    void Awake()
    {
        boton = GetComponent<Button>();
        if (boton == null)
        {
            boton = GetComponentInChildren<Button>();
        }

        if (boton != null)
        {
            // Escuchar el clic automáticamente sin importar en qué escena esté el jugador
            boton.onClick.AddListener(Comprar);
        }
    }

    void Start()
    {
        // Actualiza los textos del cuadro visualmente con los datos del Inspector
        ActualizarUI();
    }

    public void ConfigurarItem(string nuevoNombre, int nuevoPrecio, Sprite nuevoIcono)
    {
        nombreItem = nuevoNombre;
        precioItem = nuevoPrecio;
        if (iconoImg != null && nuevoIcono != null)
        {
            iconoImg.sprite = nuevoIcono;
        }
        ActualizarUI();
    }

    void ActualizarUI()
    {
        if (nombreTxt != null) nombreTxt.text = nombreItem;
        if (precioTxt != null) precioTxt.text = "Precio: " + precioItem;
    }

    public void Comprar()
    {
        // Buscar a Quetzalcóatl dinámicamente en la jerarquía activa
        InventarioPlayer inventario = FindObjectOfType<InventarioPlayer>();

        if (inventario != null)
        {
            // Pasa el nombre específico de ESTE botón/objeto al inventario
            inventario.ComprarItem(nombreItem);
        }
        else
        {
            Debug.LogWarning("No se encontró al jugador Quetzalcóatl en la escena.");
        }
    }
}