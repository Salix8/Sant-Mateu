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

    
    public void AbrirMenuTelefono()
    {
        

        foreach (GameObject obj in allGameObjects)
        {
            obj.SetActive(true);
        }

        menuTelefono.SetActive(true);

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
