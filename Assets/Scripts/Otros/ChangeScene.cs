using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private GameObject nextScene;

    private void OnMouseUp()
    {
        transform.parent.gameObject.SetActive(false);
        nextScene.gameObject.SetActive(true);
        Debug.Log(nextScene);
        Debug.Log(transform.parent.gameObject);
        Debug.Log(nextScene.gameObject);
    }
}
