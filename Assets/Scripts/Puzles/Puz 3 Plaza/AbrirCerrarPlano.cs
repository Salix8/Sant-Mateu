using UnityEngine;

public class AbrirCerrarPlano : MonoBehaviour
{
    public GameObject mostrarPlano; 

    
    public void Activar()
    {
        mostrarPlano.SetActive(true); 
        
    }

    
    public void Desactivar()
    {
        mostrarPlano.SetActive(false);
        
    }
}
