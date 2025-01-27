using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicionMinijuegos : MonoBehaviour
{
    private Animator animator;
    [SerializeField] public GameObject instructions; // Asigna esto en el inspector



    private void Start()
    {
        animator = GetComponent<Animator>();

        
        
    }

    
    
    public void TransicionMinijuego()
    {
        StartCoroutine(HacerAnimacionDeCierre() );
    }

    public IEnumerator HacerAnimacionDeCierre()
    {
        // Activar la animación de cierre
        animator.SetTrigger("Finalizar");

        
        yield return new WaitForSeconds(1f);
    }
}
