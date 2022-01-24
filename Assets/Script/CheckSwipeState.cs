using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CheckSwipeState : MonoBehaviour
{
    public string jawabandia;
    public GameObject game1, game2, game3;
    public bool atas,bawah,tengah;
    public RectTransform rek;
    public Vector2 vecAtas,vecTengah,vecBawah;

    public void Atas()
    {
        if (atas)
        {

        }
        else if(tengah)
        {
            tengah = false;
            atas = true;
        }
        else if (bawah)
        {
            bawah = false;
            tengah = true;
        }
    }

    public void Bawah()
    {
        if (atas)
        {
            atas = false;
            tengah = true;
        }
        else if (tengah)
        {
            tengah = false;
            bawah = true;
        }

        else if (bawah)
        {
            
        }
    }

    public void Check()
    {

    }

    public void Geraks()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        rek = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (atas)
        {
            rek.anchoredPosition = vecAtas;
            jawabandia = game1.name;
        }
        else if (tengah)
        {
            rek.anchoredPosition = vecTengah;
            jawabandia = game2.name;
        }
        else if (bawah)
        {
            rek.anchoredPosition = vecBawah;
            jawabandia = game3.name;
        }
    }
}
