using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop1 : MonoBehaviour
{
    public int jumlahjawaban;
    public string jawabans;
    public bool jawaban1, jawaban2, jawaban3;
    public bool benar, salah;
    public AcakSoal5 acs5;
    public void Start()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<Drag1>().masuk = true;
        jawabans = other.gameObject.name;
        if (gameObject.name == jawabans)
        {
            benar = true;
            acs5.Benar();
        }
        else
        {
            acs5.Salah();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.GetComponent<Drag1>().masuk = false;
        jawabans = "";
    }

}
