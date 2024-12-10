using UnityEngine;
using UnityEngine.SceneManagement; 

public class ComenzarPartida : MonoBehaviour
{
    
    public void LoadScene(string TestDialogue)
    {
        SceneManager.LoadScene("TestDialogue");
    }
}
