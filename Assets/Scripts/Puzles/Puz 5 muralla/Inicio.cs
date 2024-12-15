using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicio : MonoBehaviour
{
    [SerializeField] private TextAsset dialogo;
    [SerializeField] private GameObject[] objetos;
    [SerializeField] private AudioClip cancion;
    private AudioSource audioSource;

    void Start()
    {
        DialogueManager.GetInstance().EnterDialogueMode(dialogo);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = cancion;
        audioSource.loop = true;
        audioSource.Play();

    }

    private void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        foreach (GameObject obj in objetos)
        {
            obj.SetActive(true);
        }
    }
    public void StopMusic()
    {
        audioSource.Stop(); //Espero que en el load se destruya
    }

}
