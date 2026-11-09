using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class ItemData
{
    public string nombreItem;
    public Sprite iconoItem;
    public int precio;
    public float valorCuracion; // Si es una poción o curativo
}

public class InventarioPlayer : MonoBehaviour
{
    [Header("Configuración de Inventario")]
    public GameObject panelInventario;
    public int capacidadMaxima = 10;

    [Header("Referencias UI de Slots (Arrastra tus 10 casillas aquí)")]
    public Image[] imagenesSlots; // Arrastra los componentes Image de tus 10 slots visuales

    [Header("Lista de Items Disponibles en el Juego")]
    public List<ItemData> baseDeDatosItems; // Configura aquí los objetos que vende el aldeano

    // Inventario actual del jugador (guarda los nombres de los items)
    private List<ItemData> itemsInventario = new List<ItemData>();
    private int indiceSlotSeleccionado = 0; // Para la navegación por teclado/control
    private bool inventarioAbierto = false;

    void Start()
    {
        if (panelInventario != null)
        {
            panelInventario.SetActive(false);
        }
        ActualizarVisualInventario();
    }

    void Update()
    {
        // Abrir / Cerrar inventario con la letra 'I'
        if (Input.GetKeyDown(KeyCode.I))
        {
            AlternarInventario();
        }

        // Navegación dentro del inventario si está abierto
        if (inventarioAbierto)
        {
            NavegarInventario();

            // Usar objeto seleccionado con la tecla Espacio o Enter
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                UsarItemSeleccionado();
            }
        }
    }

    void AlternarInventario()
    {
        inventarioAbierto = !inventarioAbierto;
        if (panelInventario != null)
        {
            panelInventario.SetActive(inventarioAbierto);
        }
    }

    // ==========================================
    // 1. COMPRAR OBJETO AL ALDEANO
    // ==========================================
    public void ComprarItem(string nombreItem)
    {
        // Buscar el objeto en la base de datos
        ItemData itemAComprar = baseDeDatosItems.Find(i => i.nombreItem == nombreItem);
        if (itemAComprar == null)
        {
            Debug.LogWarning("El objeto no existe en la base de datos.");
            return;
        }

        // Verificar si hay espacio en el inventario
        if (itemsInventario.Count >= capacidadMaxima)
        {
            Debug.Log("¡El inventario está lleno!");
            return;
        }

        // Verificar si el jugador tiene suficiente oro (usando el CoinManager que ya tienes)
        CoinManager coinManager = FindObjectOfType<CoinManager>();
        if (coinManager != null)
        {
            // Asumiendo que CoinManager tiene un entero para el oro llamado 'contadorMonedas'
            if (coinManager.contadorMonedas >= itemAComprar.precio)
            {
                // Descontar oro
                coinManager.ModificarMonedas(-itemAComprar.precio); // Asegúrate de tener este método o réstalo directamente

                // Añadir al inventario
                itemsInventario.Add(itemAComprar);
                ActualizarVisualInventario();
                Debug.Log("¡Comprado con éxito: " + itemAComprar.nombreItem + "!");
            }
            else
            {
                Debug.Log("No tienes suficiente oro para comprar esto.");
            }
        }
    }

    // ==========================================
    // 2. ACTUALIZAR LOS ICONOS VISUALES
    // ==========================================
    void ActualizarVisualInventario()
    {
        for (int i = 0; i < imagenesSlots.Length; i++)
        {
            if (i < itemsInventario.Count)
            {
                // Mostrar icono del objeto
                imagenesSlots[i].sprite = itemsInventario[i].iconoItem;
                imagenesSlots[i].gameObject.SetActive(true);
                // Si la imagen estaba totalmente transparente, asegúrate de poner alfa a 1
                Color color = imagenesSlots[i].color;
                color.a = 1f;
                imagenesSlots[i].color = color;
            }
            else
            {
                // Slot vacío: Ocultar icono o poner transparente
                imagenesSlots[i].sprite = null;
                Color color = imagenesSlots[i].color;
                color.a = 0f; // Invisible
                imagenesSlots[i].color = color;
            }
        }
    }

    // ==========================================
    // 3. NAVEGACIÓN ENTRE OBJETOS (TECLADO/CONTROL)
    // ==========================================
    void NavegarInventario()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            indiceSlotSeleccionado = (indiceSlotSeleccionado + 1) % capacidadMaxima;
            ResaltarSlotActual();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            indiceSlotSeleccionado = (indiceSlotSeleccionado - 1 + capacidadMaxima) % capacidadMaxima;
            ResaltarSlotActual();
        }
    }

    void ResaltarSlotActual()
    {
        // Aquí puedes cambiar el color del borde del slot seleccionado para que el jugador sepa cuál está tocando
        Debug.Log("Slot seleccionado: " + indiceSlotSeleccionado);
    }

    // ==========================================
    // 4. USAR / CONSUMIR OBJETO
    // ==========================================
    public void UsarItemSeleccionado()
    {
        if (indiceSlotSeleccionado < itemsInventario.Count)
        {
            ItemData itemUsado = itemsInventario[indiceSlotSeleccionado];

            // Ejemplo de uso: Si cura vida, afectar al HUDManager
            if (itemUsado.valorCuracion > 0)
            {
                HUDManager hud = FindObjectOfType<HUDManager>();
                if (hud != null)
                {
                    hud.ModificarVida(itemUsado.valorCuracion);
                    Debug.Log("Usaste " + itemUsado.nombreItem + " y recuperaste vida.");
                }
            }

            // Remover el objeto del inventario tras usarlo
            itemsInventario.RemoveAt(indiceSlotSeleccionado);
            ActualizarVisualInventario();
        }
        else
        {
            Debug.Log("El slot seleccionado está vacío.");
        }
    }
}