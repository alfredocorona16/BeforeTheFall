// Item.cs
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "BeforeTheFall/Item")]
public class Item : ScriptableObject
{
    public string id;           // "semilla_maiz"
    public string nombre;       // "Semilla de Maíz"
    public Sprite icono;        // el sprite de tu memorama/assets
    [TextArea] public string descripcion;
}