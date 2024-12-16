using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// Inicia, muestra y termina los dialogos
//Continua los dialogos, muestra las opciones, cambia posiciones del layout


public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f; //Cuanto menor sea mas rapido escribir�

    //[Header("Globals Ink File")]
    //[SerializeField] private TextAsset loadGlobalsJSON;


    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    private Animator layoutAnimator;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [Header("Audio")]
    [SerializeField] private DialogueAudioInfoSO defaultAudioInfo;
    [SerializeField] private DialogueAudioInfoSO[] audioInfos;
    [SerializeField] private bool makePredictable;
    private DialogueAudioInfoSO currentAudioInfo;
    private Dictionary<string, DialogueAudioInfoSO> audioInfoDictionary;
    private AudioSource audioSource;



    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;
    private Coroutine displayLineCoroutinte;

    private static DialogueManager instance;
    private InputAction clickAction;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string AUDIO_TAG = "audio";
    private const string PANEL_TAG = "panel";

    //private DialogueVariables dialogueVariables;

    private bool isDebug = false;


    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Se ha encontrado mas de un DialogueManager en la escena");
        }
        instance = this;

        //dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        clickAction = InputSystem.actions.FindAction("Click");

        audioSource = this.gameObject.AddComponent<AudioSource>();
        currentAudioInfo = defaultAudioInfo;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    //____

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        layoutAnimator = dialoguePanel.GetComponent<Animator>();

        choicesText = new TextMeshProUGUI[choices.Length];

        for (int i = 0; i < choices.Length; i++)
        {
            choicesText[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
        }
        InitializeAudioInfoDictionary();
    }
    private void InitializeAudioInfoDictionary()
    {
        audioInfoDictionary = new Dictionary<string, DialogueAudioInfoSO>();
        audioInfoDictionary.Add(defaultAudioInfo.id, defaultAudioInfo);
        foreach (DialogueAudioInfoSO audioInfo in audioInfos)
        {
            audioInfoDictionary.Add(audioInfo.id, audioInfo);
        }
    }
    private void SetCurrentAudioInfo(string id)
    {
        DialogueAudioInfoSO audioInfo = null;
        audioInfoDictionary.TryGetValue(id, out audioInfo);
        if (audioInfo != null)
        {
            this.currentAudioInfo = audioInfo;
        }
        else
        {
            Debug.LogWarning("Failed to find audio info for id: " + id);
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying) { // Solo queremos que se actualice si hay algun dialogo
            return;
        }

        if (canContinueToNextLine && currentStory.currentChoices.Count == 0 && InputManager.GetInstance().GetSubmitPressed())
        {
            ContinueStory();
        }

    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        //dialogueVariables.StartListening(currentStory);

        canContinueToNextLine = false; // asegurarse de que no se esta ejecutando otra linea de dialogo
        if (displayLineCoroutinte != null)
        {
            StopCoroutine(displayLineCoroutinte);
            displayLineCoroutinte = null;
        }

        // Reset portrait, layout, and speaker
        displayNameText.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("right");
        dialogueText.text = "";

        ContinueStory();
    }

    public IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        //dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        SetCurrentAudioInfo(defaultAudioInfo.id);
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            //set text la linea actual del dialogo
            //dialogueText.text = currentStory.Continue();
            if (displayLineCoroutinte != null)
                StopCoroutine(displayLineCoroutinte);

            string nextLine = currentStory.Continue();
            // Las etiquetas que definen la imagen posicion etc del npc que habla
            HandleTags(currentStory.currentTags);

            displayLineCoroutinte = StartCoroutine(Displayline(nextLine));
        }
        else
        {
            StartCoroutine( ExitDialogueMode() );
        }
    }

    private IEnumerator Displayline(string line)
    {
        //yield return new WaitForSeconds(0.15f);//En teoria esto deberia solucionar lo de que el primero se vea muy rapido,
        //pero si lo pongo luego cuando hay un cambio de personaje se ve extra�o porque se espera ese poco con el texto de otro sin cambiar pero con el layout del nuevo queda raro
        // Vaciamos el texto para que la linea anterior ya no se muestre
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        continueIcon.SetActive(false); // Ocultamos la UI mientras se escribe
        HideChoices();
        canContinueToNextLine = false; // Para evitar que el jugador continue hasta que haya completado el texto

        bool isAddingRichTextTag = false;

        foreach (char letter in line.ToCharArray())
        {
            if (InputManager.GetInstance().GetSubmitPressed())
            {
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }

            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                //dialogueText.text += letter;
                if (letter == '>')
                    isAddingRichTextTag = false;
            }
            else // No es rich text colorinchis
            {
                PlayDialogueSound(dialogueText.maxVisibleCharacters, dialogueText.text[dialogueText.maxVisibleCharacters]);
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        continueIcon.SetActive(true); // Volvemos activar la UI para que el Jugador sepa que puede continuar
        DisplayChoices();

        canContinueToNextLine = true;
    }


    private void PlayDialogueSound(int currentDisplayedCharacterCount, char currentCharacter)
    {
        // set variables for the below based on our config
        AudioClip[] dialogueTypingSoundClips = currentAudioInfo.dialogueTypingSoundClips;
        int frequencyLevel = currentAudioInfo.frequencyLevel;
        float minPitch = currentAudioInfo.minPitch;
        float maxPitch = currentAudioInfo.maxPitch;
        bool stopAudioSource = currentAudioInfo.stopAudioSource;

        // play the sound based on the config
        if (currentDisplayedCharacterCount % frequencyLevel == 0)
        {
            if (stopAudioSource)
            {
                audioSource.Stop();
            }
            AudioClip soundClip = null;
            // create predictable audio from hashing
            if (makePredictable)
            {
                int hashCode = currentCharacter.GetHashCode();
                // sound clip
                int predictableIndex = hashCode % dialogueTypingSoundClips.Length;
                soundClip = dialogueTypingSoundClips[predictableIndex];
                // pitch
                int minPitchInt = (int)(minPitch * 100);
                int maxPitchInt = (int)(maxPitch * 100);
                int pitchRangeInt = maxPitchInt - minPitchInt;
                // cannot divide by 0, so if there is no range then skip the selection
                if (pitchRangeInt != 0)
                {
                    int predictablePitchInt = (hashCode % pitchRangeInt) + minPitchInt;
                    float predictablePitch = predictablePitchInt / 100f;
                    audioSource.pitch = predictablePitch;
                }
                else
                {
                    audioSource.pitch = minPitch;
                }
            }
            // otherwise, randomize the audio
            else
            {
                // sound clip
                int randomIndex = Random.Range(0, dialogueTypingSoundClips.Length);
                soundClip = dialogueTypingSoundClips[randomIndex];
                // pitch
                audioSource.pitch = Random.Range(minPitch, maxPitch);
            }

            // play sound
            audioSource.PlayOneShot(soundClip);
        }
    }

    private void HideChoices()
    {
        foreach (var choiceBtn in choices)
        {
            choiceBtn.SetActive(false);
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2 )
            {
                Debug.LogError("TAG. Puede que no se haya parseado bien esta tag: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // Handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    if(isDebug) Debug.Log("Speaker= " + tagValue);
                    displayNameText.text = tagValue;
                    break;
                case PORTAIT_TAG:
                    if(isDebug) Debug.Log("Portrait= " + tagValue);
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    if(isDebug) Debug.Log("Layout= " + tagValue);
                    layoutAnimator.Play(tagValue);
                    break;
                case AUDIO_TAG:
                    SetCurrentAudioInfo(tagValue);
                    break;
                //case PANEL_TAG:
                //    if (isDebug) Debug.Log("Panel= " + tagValue);
                //    dialoguePanel.ima
                //    break;
                default:
                    Debug.LogWarning("La tag ha llegado, pero no se esta gestionando correctamente: " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // if (currentChoices.Count == 0) return; // No podemos hacer esto porque si no veremos botones que no queremos, maas abajo haglo el bucle que los oculta

        if (currentChoices.Count > choices.Length)
        {
            // Si ha saltado este error prueba a mirar el Dialoguemanager o los DialogueChoices del canvas
            Debug.LogError("Hay mas opciones de las que puede soportar la UI. Numero de opciones: " + currentChoices.Count);
        }

        // Activa e inicializa las opciones necesarias de esa linea
        int index = 0;
        foreach (Choice choice in currentChoices) // foreache es mas legble aunque tenga que manejar una variable extra
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // Oculta los botones de las opciones que sobran
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine( SelectFirstChoice() );

    }

    private IEnumerator SelectFirstChoice()
    {
        // EventSystem requiere que se borre primero y luego ya se puede establecer en un frame distinto
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            //Debug.LogWarning(clickAction.IsPressed());
            InputManager.GetInstance().RegisterSubmitPressed();
            ContinueStory();
        }
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        //dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue != null)
        {
            Debug.Log("Ink Variable es nula: " + variableName);
        }
        return variableValue;
    }
}
