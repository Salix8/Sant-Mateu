using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInstructions : MonoBehaviour
{
    private bool switcher = false;
    public bool timeinit = false;

    public GameObject imagen;

    // Start is called before the first frame update
    void Start()
    {
        if (!timeinit){
            Time.timeScale = 0;   
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void change ()
    {
        if(switcher){
            imagen.SetActive(true);
            switcher = false;
            Time.timeScale = 0;
        }
        else{
            imagen.SetActive(false);
            switcher = true;
            Time.timeScale = 1;
        }
    }

    public void changeNoTime ()
    {
        if(switcher){
            imagen.SetActive(true);
            switcher = false;
            
        }
        else{
            imagen.SetActive(false);
            switcher = true;
            
        }
    }
}
