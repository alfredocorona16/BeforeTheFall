// InventarioJugador.cs
using System.Collections.Generic;
using UnityEngine;

public class InventarioJugador : MonoBehaviour
{
    public int oro = 50;
    // item.id -> cantidad
    public Dictionary<string, int> items = new Dictionary<string, int>();

    public bool TieneOro(int cantidad) => oro >= cantidad;

    public void GastarOro(int cantidad) => oro -= cantidad;

    public void AgregarItem(Item item, int cantidad)
    {
        if (!items.ContainsKey(item.id)) items[item.id] = 0;
        items[item.id] += cantidad;
        Debug.Log($"Recibiste {cantidad}x {item.nombre}. Oro: {oro}");
    }
}