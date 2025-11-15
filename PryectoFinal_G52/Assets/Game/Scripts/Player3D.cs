using UnityEngine;
using UnityEngine.UI;

public class Player3D : MonoBehaviour
{
    public float HP_Min;
    public float HP_Max;
    public Image barra;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        barra.fillAmount = HP_Min / HP_Max;
    }
}
