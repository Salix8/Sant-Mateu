using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mov_flores : MonoBehaviour
{
    //flores
    public GameObject[] flowers;
    public GameObject p_spawn;
    private int random_int;
  

    
    //timer

    private bool timer_active = false; 

    public int t_min, t_seg;
    public Text t_texto;

    float nextTime = 0;
    int interval = 1;

    //generación
    
    public float max_time_flowers; //tiempo maximo entre flor y flor

    public float min_time_flowers;//tiempo minimo entre flor y flor

    private float random_float;
    
   
    void Start()
    {
       

        Start_timer();
        StartCoroutine(Generate_flower());
    }
    
    
    void Update()
    {
        if(timer_active){
            Change_Timer();
            
        }
        

        t_texto.text = t_min.ToString("00") + ":" + t_seg.ToString("00");

        
        

    }

    private IEnumerator Generate_flower(){

        while (timer_active) // Hacer que la corutina continúe generando flores indefinidamente
        {
            random_float = Random.Range(min_time_flowers, max_time_flowers);

            yield return new WaitForSeconds(random_float);

            Vector2 position = GetComponent<Rigidbody2D>().position;

            random_int = Random.Range(-8,9);

            position.x = random_int;

            random_int = Random.Range(0,flowers.Length);

                
            Instantiate(flowers[random_int],position, p_spawn.transform.rotation);
        }
       

    }
     
     private void Change_Timer(){
        if (Time.time >= nextTime){
            nextTime += interval;
            t_seg -=1;  
        }
        if (t_seg < 0){
            t_seg = 0;    
        }

        if( t_seg == 0 & t_min >= 1){
            t_min -=1;
            t_seg = 60;
        }   
        if(t_min == 0 & t_seg == 0){
            Debug.Log("Fin del juego");
            

            Finish_timer();
        }
        
    }
    

    public void Start_timer(){
        
        timer_active = true;
    }

    public void Finish_timer(){
        timer_active = false;
    }
    
    
 
}
