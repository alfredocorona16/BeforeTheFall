// BotonOferta.cs  (version TextMeshPro - TMP)
// Va en el PREFAB de cada fila de oferta del panel de trueque.
// Muestra icono + nombre + precio, y al pulsarlo ejecuta la compra.
using UnityEngine;
using UnityEngine.UI;
using TMPro;                 // <-- necesario para TextMeshPro

public class BotonOferta : MonoBehaviour
{
    public Image iconoImg;      // arrastra aqui el Image del prefab
    public TMP_Text nombreTxt;  // arrastra aqui el objeto "Text (TMP)" del nombre
    public TMP_Text precioTxt;  // arrastra aqui el objeto "Text (TMP)" del precio
    public Button boton;        // el Button del prefab

    private Oferta oferta;

    // Lo llama el PanelTrueque al crear cada fila
    public void Configurar(Oferta o)
    {
        oferta = o;
        iconoImg.sprite = o.item.icono;
        nombreTxt.text = o.cantidad + "x " + o.item.nombre;
        precioTxt.text = o.precioOro + " oro";
        boton.onClick.RemoveAllListeners();
        boton.onClick.AddListener(() => PanelTrueque.Instancia.Comprar(oferta));
    }
}
