using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoluciónTripleClick : MonoBehaviour
{
    [SerializeField] private GameObject[] allGameObjects;
    [SerializeField] private GameObject menuTelefono;       
    [SerializeField] private GameObject mapa;

    public ProgressionManagerProxy progressionManagerProxy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MostrarMapa()
    {
        mapa.SetActive(true);
        menuTelefono.SetActive(false);
    }

    
    public void Volver()
    {
        menuTelefono.SetActive(false);
        mapa.SetActive(false);

        GameObject[] objs = GlobalManager.GetInstance().GetPathObjects();
        GlobalManager.GetInstance().SetPathObject(true);
        foreach (GameObject obj in allGameObjects)
        {
            obj.SetActive(true);
            
        }

    }
}
