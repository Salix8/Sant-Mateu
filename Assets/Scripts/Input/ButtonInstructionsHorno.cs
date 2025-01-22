using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInstructionsHorno : MonoBehaviour
{
    private bool switcher = true;

    public GameObject imagen;

    public List<GameObject> objetosaocultar;

    // Start is called before the first frame update
    void Start()
    {
        change();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void change ()
    {
        if(switcher){
            imagen.SetActive(true);
            foreach (var v in objetosaocultar){
                v.SetActive(false);
            }
            switcher = false;
            Time.timeScale = 0;
            
        }
        else{
            imagen.SetActive(false);
            foreach (var v in objetosaocultar){
                v.SetActive(true);
            }
            switcher = true;
            Time.timeScale = 1;
        }
    }
}
