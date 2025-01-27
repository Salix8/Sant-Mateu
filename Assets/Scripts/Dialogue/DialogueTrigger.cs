using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset[] dialogos; // Array de diálogos según el progreso
    [SerializeField] private TextAsset dialogoPorDefecto; // Diálogo por defecto (opcional)
    
    private InputAction clickAction;
    private bool isDebug = false;

    [SerializeField] private int nivelPuzzle;     //Nivel de progreso del puzzle

    private void Awake()
    {
        if (isDebug) Debug.Log("Activando Dialogue trigger");
        clickAction = InputSystem.actions.FindAction("Click");
    }

    public void StartDialogue()
    {
        int dialogoSeleccionado = 0;
        if (isDebug && clickAction.IsPressed()) // También sirve para controles táctiles
        {
            Debug.Log("Se ha clicado en " + this.gameObject.name);
        }

        // Obtener el progreso actual desde el ProgresionManager
        int nivelProgreso = ProgresionManager.GetInstance().nivelprogreso;

        // Seleccionar el diálogo adecuado según el nivel de progreso
        TextAsset dialogoActual = dialogoPorDefecto; // Usamos el diálogo por defecto como base
        if (nivelProgreso == nivelPuzzle && dialogos.Length > 1)
        {
            dialogoSeleccionado = 1;
        }
        dialogoActual = dialogos[dialogoSeleccionado]; // Usamos el diálogo correspondiente al nivel de progreso

        // Verificar si el diálogo existe
        if (dialogoActual != null && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            // Iniciar el diálogo con el DialogueManager
            DialogueManager.GetInstance().EnterDialogueMode(dialogoActual);
        }
        else
        {
            Debug.LogWarning($"No se encontró un diálogo para el nivel de progreso {nivelProgreso}, usando el diálogo por defecto.");
        }
    }
}
