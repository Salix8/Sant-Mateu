using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_flores : MonoBehaviour
{
    //flores
    public GameObject[] flowers;
    public GameObject p_spawn;
    private int random_int;
    Rigidbody2D rigidbody2d;

    
    //timer
    [SerializeField] private float max_timer;

    private float current_time;

    private bool timer_active = false; 

    //generación
    
    public float max_time_flowers; //tiempo maximo entre flor y flor

    public float min_time_flowers;//tiempo minimo entre flor y flor

    private float random_float;
    
   
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        Start_timer();
        StartCoroutine(Generate_flower());
    }
    
    
    void Update()
    {
        if(timer_active){
            Change_Timer();
            
        }

        
        

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

        current_time += Time.deltaTime;

        if(current_time >= max_timer){
            Debug.Log("Fin del juego");
            

            Finish_timer();
        }
    }
    

    public void Start_timer(){
        current_time = 0;
        timer_active = true;
    }

    public void Finish_timer(){
        timer_active = false;
    }
    
    
 
}
