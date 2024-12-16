using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject[] allGameObjects;    
    public GameObject menuTelefono;       
    public GameObject mapa;               

    void Start()
    {
        
        menuTelefono.SetActive(false);
        mapa.SetActive(false);
    }

    
    public void AbrirMenuTelefono() // no se abre cuando hay dialogo, desactiva cambio de Zona
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            foreach (GameObject obj in allGameObjects)
            {
                obj.SetActive(true);

            }

            GlobalManager.GetInstance().SetPathObject(false);
            menuTelefono.SetActive(true);
        }
    }

    
    public void MostrarMapa()
    {
        
        mapa.SetActive(true);
    }

    
    public void Volver()
    {
        
        menuTelefono.SetActive(false);
        mapa.SetActive(false);

        
        foreach (GameObject obj in allGameObjects)
        {
            obj.SetActive(true);
        }
    }


}
