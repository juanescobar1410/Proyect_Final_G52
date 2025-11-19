using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeEscena : MonoBehaviour
{
    
   

    public void LoaderScenes(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }




}