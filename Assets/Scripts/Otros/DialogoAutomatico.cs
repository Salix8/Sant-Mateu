using System.Collections;
using UnityEngine;

public class DialogoAutomatico : MonoBehaviour
{
    [SerializeField] private TextAsset dialogo;
    [SerializeField] private GameObject zonaActiva;
    [SerializeField] private int idPuzzleRequerido = -1;  // ID del puzzle necesario para el diálogo
    [SerializeField] private bool startDialogueAutomaticamente = false;  // Para activar el diálogo al entrar en la escena
    private bool dialogoPlayed = false;

    private void Start()
    {
        if (dialogo == null)
        {
            Debug.LogError("El diálogo no está asignado.");
            return;
        }

        // Verificamos si el diálogo debe empezar automáticamente al entrar en la escena.
        if (startDialogueAutomaticamente && !ProgresionManager.GetInstance().IsPuzzleCompleted(0))
        {
            ReproducirDialogo();
        }
        else
        {
            // Verificamos si el puzzle ha sido completado y si el diálogo no ha sido reproducido antes.
            if (idPuzzleRequerido != -1 && ProgresionManager.GetInstance().IsPuzzleCompleted(idPuzzleRequerido) && !dialogoPlayed)
            {
                StartCoroutine(EsperarYReproducirDialogo());
            }
        }
    }

    private IEnumerator EsperarYReproducirDialogo()
    {
        // Esperamos un pequeño tiempo antes de reproducir el diálogo, para asegurarnos de que los datos se hayan actualizado correctamente.
        yield return new WaitForSeconds(0.5f);  // Puedes ajustar el tiempo si es necesario

        ReproducirDialogo();
    }

    private void ReproducirDialogo()
    {
        if (!dialogoPlayed && zonaActiva.activeSelf)  // Verificamos si el objeto zonaActiva está activo
        {
            DialogueManager.GetInstance().EnterDialogueMode(dialogo);
            dialogoPlayed = true;  // Aseguramos que el diálogo solo se reproduzca una vez
        }
    }
}
