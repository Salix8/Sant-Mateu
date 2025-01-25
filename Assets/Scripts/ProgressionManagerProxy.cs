using UnityEngine;

public class ProgressionManagerProxy : MonoBehaviour
{

	public void LoadMainScene()
	{
		ProgresionManager.GetInstance().LoadMainScene();
	}
	public void LoadSceneMinijuego(int sceneIndex)
	{
		ProgresionManager.GetInstance().LoadSceneMinijuego(sceneIndex);
	}
	public void ReloadScene()
	{
		ProgresionManager.GetInstance().ReloadScene();
	}
	public void SetComplete(int n){
		ProgresionManager.GetInstance().SetComplete(n);
	}
}