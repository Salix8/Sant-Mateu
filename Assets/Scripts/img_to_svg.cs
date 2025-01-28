using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public Image imageComponent; // Arrastra aquí el componente Image desde el editor
    public Sprite svgSprite;    // Arrastra aquí el Sprite generado del SVG

    void Start()
    {
        imageComponent.sprite = svgSprite;
    }
}