using System.Collections;
using UnityEngine;

public class DialogoAutomatico : MonoBehaviour
{
    [SerializeField] private TextAsset[] dialogos;
    [SerializeField] private TextAsset dialogoPuzzle;
    [SerializeField] private GameObject zonaActiva;
    [SerializeField] private int idPuzzleRequerido = -1;  // ID del puzzle necesario para el diálogo
    [SerializeField] private bool startDialogueAutomaticamente = false;  // Para activar el diálogo al entrar en la escena
    private bool dialogoPlayed = false;

    public ProgressionManagerProxy progressionManagerProxy;

    

    private void Start()
    {
        if ((dialogos == null && startDialogueAutomaticamente) || (dialogoPuzzle == null))
        {
            Debug.LogError("El diálogo no está asignado.");
            return;
        }

        // Verificamos si el diálogo debe empezar automáticamente al entrar en la escena.
        if (startDialogueAutomaticamente)
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
        string lugar = zonaActiva.name;
        if (startDialogueAutomaticamente) {
            if (lugar == "Villores2" && !progressionManagerProxy.GetBoolDialogo(0)) {
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[0]);
                progressionManagerProxy.SetBoolDialogo(0);
            }
            if (lugar == "Villores3Presente" && !progressionManagerProxy.GetBoolDialogo(1)) {
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[1]);
                progressionManagerProxy.SetBoolDialogo(1);
            }
            else if (lugar == "Villores3Pasado" && !progressionManagerProxy.GetBoolDialogo(2)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[2]);
                progressionManagerProxy.SetBoolDialogo(2);
            }
            else if (lugar == "PlazaPresente" && !progressionManagerProxy.GetBoolDialogo(3)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[3]);
                progressionManagerProxy.SetBoolDialogo(3);
            }
            else if (lugar == "PlazaPasado" && !progressionManagerProxy.GetBoolDialogo(4)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[4]);
                progressionManagerProxy.SetBoolDialogo(4);
            }
            else if (lugar == "ArciprestalPresente" && !progressionManagerProxy.GetBoolDialogo(5)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[5]);
                progressionManagerProxy.SetBoolDialogo(5);
            }
            else if (lugar == "ArciprestalPasado" && !progressionManagerProxy.GetBoolDialogo(6)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[6]);
                progressionManagerProxy.SetBoolDialogo(6);
            }
            else if (lugar == "MurallaPresente" && !progressionManagerProxy.GetBoolDialogo(7)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[7]);
                progressionManagerProxy.SetBoolDialogo(7);
            }
            else if (lugar == "MurallaPasado" && !progressionManagerProxy.GetBoolDialogo(8)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[8]);
                progressionManagerProxy.SetBoolDialogo(8);
            }
            else if (lugar == "BorrullPresente" && !progressionManagerProxy.GetBoolDialogo(9)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[9]);
                progressionManagerProxy.SetBoolDialogo(9);
            }
            else if (lugar == "BorrullPasado" && !progressionManagerProxy.GetBoolDialogo(10)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[10]);
                progressionManagerProxy.SetBoolDialogo(10);
            }
            else if (lugar == "CallejonPresente" && !progressionManagerProxy.GetBoolDialogo(11)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[11]);
                progressionManagerProxy.SetBoolDialogo(11);
            }
            else if (lugar == "CallejonPasado" && !progressionManagerProxy.GetBoolDialogo(12)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[12]);
                progressionManagerProxy.SetBoolDialogo(12);
            }
            else if (lugar == "HornoPresente" && !progressionManagerProxy.GetBoolDialogo(13)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[13]);
                progressionManagerProxy.SetBoolDialogo(13);
            }
            else if (lugar == "HornoPasado" && !progressionManagerProxy.GetBoolDialogo(14)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[14]);
                progressionManagerProxy.SetBoolDialogo(14);
            }
            else if (lugar == "FuentePasado" && !progressionManagerProxy.GetBoolDialogo(15)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[15]);
                progressionManagerProxy.SetBoolDialogo(15);
            }
            else if (lugar == "FuentePresente" && !progressionManagerProxy.GetBoolDialogo(16)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[16]);
                progressionManagerProxy.SetBoolDialogo(16);
            }
            else if (lugar == "PerePresente" && !progressionManagerProxy.GetBoolDialogo(17)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[17]);
                progressionManagerProxy.SetBoolDialogo(17);
            }
            else if (lugar == "PerePasado" && !progressionManagerProxy.GetBoolDialogo(18)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[18]);
                progressionManagerProxy.SetBoolDialogo(18);
            }
            else if (lugar == "ConventoPresente" && !progressionManagerProxy.GetBoolDialogo(19)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[19]);
                progressionManagerProxy.SetBoolDialogo(19);
            }
            else if (lugar == "ConventoPasado" && !progressionManagerProxy.GetBoolDialogo(20)){
                DialogueManager.GetInstance().EnterDialogueMode(dialogos[20]);
                progressionManagerProxy.SetBoolDialogo(20);
            }

            else Debug.Log("Lugar no reconocido");
        }

        if (!dialogoPlayed && zonaActiva.activeSelf)  // Verificamos si el objeto zonaActiva está activo
        {
            DialogueManager.GetInstance().EnterDialogueMode(dialogoPuzzle);
            dialogoPlayed = true;  // Aseguramos que el diálogo solo se reproduzca una vez
        }
    }
}
