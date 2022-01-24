using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public bool masuk;

    public void OnTriggerStay(Collider other)
    {
        masuk = true;
    }

    public void OnTriggerExit(Collider other)
    {
        masuk = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
