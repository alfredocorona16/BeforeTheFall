using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
  
    public string escenaDelJuego = "SampleScene";


    public GameObject panelOpciones;

    public void NuevaPartida()
    {
        SceneManager.LoadScene("StoryBoard_BeforeTheFall");
    }


    public void Continuar()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void AbrirOpciones()
    {
        if (panelOpciones != null) panelOpciones.SetActive(true);
    }

    public void CerrarOpciones()
    {
        if (panelOpciones != null) panelOpciones.SetActive(false);
    }

    public void Salir()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }
}