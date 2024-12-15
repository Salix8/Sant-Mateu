using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonInstrucctions : MonoBehaviour
{

    private bool switcher = true;
    public GameObject imagen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void change (){
        if (switcher){
            switcher = false;
            imagen.SetActive(true);
            Time.timeScale = 0;
        }else{
            switcher = true;
            Time.timeScale = 1;
            imagen.SetActive(false);
        }
    }
}
