using UnityEngine;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class DatosPartida
{
    public string nombreGuardado;
    public string nombreEscena;
    public int monedas;
    public float vidaActual;
    public string fechaGuardado;
}

public static class SaveSystem
{
    // Ruta base dentro del dispositivo: .../AppData/LocalLow/TuEmpresa/Before_the_fall/Saves/
    private static string RutaCarpetaSaves => Path.Combine(Application.persistentDataPath, "Saves");

    // Verificar si existe la carpeta de guardados
    private static void ConfirmarCarpeta()
    {
        if (!Directory.Exists(RutaCarpetaSaves))
        {
            Directory.CreateDirectory(RutaCarpetaSaves);
        }
    }

    // Comprobar si ya existe un archivo con ese nombre
    public static bool ExistePartida(string nombreGuardado)
    {
        ConfirmarCarpeta();
        string rutaArchivo = Path.Combine(RutaCarpetaSaves, nombreGuardado + ".json");
        return File.Exists(rutaArchivo);
    }

    // Guardar la partida en el dispositivo
    public static void Guardar(string nombreGuardado, string escena, int monedas, float vida)
    {
        ConfirmarCarpeta();

        DatosPartida datos = new DatosPartida
        {
            nombreGuardado = nombreGuardado,
            nombreEscena = escena,
            monedas = monedas,
            vidaActual = vida,
            fechaGuardado = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm")
        };

        string json = JsonUtility.ToJson(datos, true);
        string rutaArchivo = Path.Combine(RutaCarpetaSaves, nombreGuardado + ".json");
        File.WriteAllText(rutaArchivo, json);

        Debug.Log("Partida guardada en: " + rutaArchivo);
    }

    // Obtener la lista de todas las partidas guardadas
    public static List<DatosPartida> ObtenerTodasLasPartidas()
    {
        ConfirmarCarpeta();
        List<DatosPartida> listaPartidas = new List<DatosPartida>();
        string[] archivos = Directory.GetFiles(RutaCarpetaSaves, "*.json");

        foreach (string archivo in archivos)
        {
            string json = File.ReadAllText(archivo);
            DatosPartida datos = JsonUtility.FromJson<DatosPartida>(json);
            listaPartidas.Add(datos);
        }

        return listaPartidas;
    }
}