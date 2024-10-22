using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    //Cogemos una variable que se haya modificado en un Ink para cambiar propiedades del objeto entre ellas puede ser la imagen
    [SerializeField] string Inkvariable;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        string itemName = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState(Inkvariable)).value;

        switch (Inkvariable) 
        {
            case "":
                spriteRenderer.color = Color.white;
                break;
            default:
                Debug.Log("Valor no manejado por el Switch: " + Inkvariable);
                break;
        }
    }
}
