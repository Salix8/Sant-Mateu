using UnityEngine;

public class AbrirCerrarPlano : MonoBehaviour
{
    public GameObject mostrarPlano; 

    
    public void Activar()
    {
        if (mostrarPlano.activeSelf) {
            Desactivar();
        }
        else {
            mostrarPlano.SetActive(true); 
        }
    }

    
    public void Desactivar()
    {
        mostrarPlano.SetActive(false);
        
    }
}
