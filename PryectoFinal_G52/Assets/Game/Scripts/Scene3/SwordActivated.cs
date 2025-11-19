using UnityEngine;
using System.Collections;   
using System.Collections.Generic;
using System.Xml.Serialization;

public class SwordActivated : MonoBehaviour
{
    public ShowSword cogerArmas;
    public int numeroArma;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cogerArmas = GameObject.FindGameObjectWithTag("Player").GetComponent<ShowSword>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag== "Player")
        {
            cogerArmas.ActivarArmar(numeroArma);
            Destroy(gameObject);
        }
    }

}
