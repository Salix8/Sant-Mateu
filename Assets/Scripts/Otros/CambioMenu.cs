using UnityEngine;

public class CambioMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] allGameObjects;    
    [SerializeField] private GameObject menuTelefono;       
    [SerializeField] private GameObject mapa;               

    void Start()
    {
        
        menuTelefono.SetActive(false);
        mapa.SetActive(false);
    }

    
    public void AbrirMenuTelefono() // no se abre cuando hay dialogo, desactiva cambio de Zona
    {
        Debug.Log($"Hay dialogo? {DialogueManager.GetInstance().dialogueIsPlaying}");
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            Debug.Log($"1 No hay dialogo");
            foreach (GameObject obj in allGameObjects)
            {
                Debug.Log($"{obj}");
                obj.SetActive(true);

            }
            Debug.Log($"2");

            GlobalManager.GetInstance().SetPathObject(false);
            menuTelefono.SetActive(true);
            Debug.Log($"3 {menuTelefono}");
        }
    }
    public void test()
    {
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
