using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicMenuUtama : MonoBehaviour
{

    PostScore pskor;

    public GameObject go1, go2, go3, go4, go5, go6, go7;
    public Button but1, but2, but3, but4, but5, but6, but7;
    public Image im1, im2, im3, im4, im5, im6, im7;

    // Start is called before the first frame update
    void Start()
    {
        pskor = GameObject.FindGameObjectWithTag("WebManager").GetComponent<PostScore>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pskor.statuslevel1 == "belum")
        {
            but2.interactable = false;
            go2.SetActive(true);

        }
        else if (pskor.statuslevel1 == "selesai")
        {
            but2.interactable = true;
            go2.SetActive(false);
        }
        if (pskor.statuslevel2 == "belum")
        {
            but3.interactable = false;
            go3.SetActive(true);

        }
        else if (pskor.statuslevel2 == "selesai")
        {
            but3.interactable = true;
            go3.SetActive(false);
        }
        if (pskor.statuslevel3 == "belum")
        {
            but4.interactable = false;
            go4.SetActive(true);
        }
        else if (pskor.statuslevel3 == "selesai")
        {
            but4.interactable = true;
            go4.SetActive(false);
        }
        if (pskor.statuslevel4 == "belum")
        {
            but5.interactable = false;
            go5.SetActive(true);
        }
        else if (pskor.statuslevel4 == "selesai")
        {
            but5.interactable = true;
            go5.SetActive(false);
        }
        if (pskor.statuslevel5 == "belum")
        {
            but6.interactable = false;
            go6.SetActive(true);
        }
        else if (pskor.statuslevel5 == "selesai")
        {
            but6.interactable = true;
            go6.SetActive(false);
        }
        if (pskor.statuslevel6 == "belum")
        {
            but7.interactable = false;
            go7.SetActive(true);
        }
        else if (pskor.statuslevel6 == "selesai")
        {
            but7.interactable = true;
            go7.SetActive(false);
        }
    }
}
