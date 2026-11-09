// Aldeano.cs
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Oferta
{
    public Item item;        // qué recibes
    public int cantidad = 1; // cuántos
    public int precioOro;    // cuánto oro cuesta
}

public class Aldeano : MonoBehaviour
{
    public string nombreAldeano = "Mercader Tlaxcalteca";
    public List<Oferta> ofertas = new List<Oferta>(); // se llenan en el Inspector
}