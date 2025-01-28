using System.Collections;
using UnityEngine;

public class DialogoAutomatico : MonoBehaviour
{
    [SerializeField] private TextAsset[] dialogos;
    [SerializeField] private GameObject zonaActiva;
    [SerializeField] private int idPuzzleRequerido = -1;  // ID del puzzle necesario para el diálogo
    [SerializeField] private bool startDialogueAutomaticamente = false;  // Para activar el diálogo al entrar en la escena
    

    public ProgressionManagerProxy progressionManagerProxy;

    

    private void Start()
    {
        if (dialogos == null && startDialogueAutomaticamente)
        {
            Debug.LogError("El diálogo no está asignado.");
            return;
        }

        // Verificamos si el puzzle ha sido completado y si el diálogo no ha sido reproducido antes.
        if (startDialogueAutomaticamente)
        {
            StartCoroutine(EsperarYReproducirDialogo());
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
        string lugar = zonaActiva.name;
        Debug.Log(lugar);
        Debug.Log(startDialogueAutomaticamente);
        if (startDialogueAutomaticamente)
        {
            // Lista de lugares en el mismo orden que los índices de los diálogos.
            string[] lugares = {
                "Villores2",
                "Villores3Presente",
                "Villores3Pasado",
                "PlazaPresente",
                "PlazaPasado",
                "ArciprestalPresente",
                "ArciprestalPasado",
                "MurallaPresente",
                "MurallaPasado",
                "BorrullPresente",
                "BorrullPasado",
                "CallejonPresente",
                "CallejonPasado",
                "HornoPresente",
                "HornoPasado",
                "FuentePasado",
                "FuentePresente",
                "PerePresente",
                "PerePasado",
                "ConventoPresente",
                "ConventoPasado",
                "ConventoGalletas"
            };

            // Buscar el índice del lugar en la lista.
            for (int i = 0; i < lugares.Length; i++)
            {
                if (lugar == lugares[i])
                {
                    // Si el diálogo aún no ha sido activado, lo iniciamos.
                    if (!progressionManagerProxy.GetBoolDialogo(i))
                    {
                        DialogueManager.GetInstance().EnterDialogueMode(dialogos[i]);
                        progressionManagerProxy.SetBoolDialogo(i);
                        Debug.Log($"Dialogo num: {i}. DialogoAutomatico.ReproducirDialogo()");
                    }
                    break; // Salir de la función, ya que el lugar fue encontrado.
                }
            }

            // Si el lugar no fue encontrado.
            Debug.Log("Lugar no reconocido");
        }
    }
}
