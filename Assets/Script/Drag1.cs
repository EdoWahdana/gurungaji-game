using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Drag1 : MonoBehaviour
{

    public int jumlahjawaban;
    public RectTransform rt, rt2;
    public Vector3 trans;
    public bool selesai,masuk,dragable;
    public string jawaban1;
    public Animator animat;
    public Canvas parentCanvas;
    public RawImage mouseCursor;


    public void MulaiDrag()
    {
        dragable = true;
        animat.enabled = false;
    }

    public void Dragging()
    {
        if (dragable)
        {
            Vector2 movePos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentCanvas.transform as RectTransform,
                Input.mousePosition, parentCanvas.worldCamera,
                out movePos);

            Vector3 mousePos = parentCanvas.transform.TransformPoint(movePos);

            //Set fake mouse Cursor
            //mouseCursor.transform.position = mousePos;

            //Move the Object/Panel
            transform.position = mousePos;

            /*  animat.enabled = false;
              rt.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

              rt.transform.position = new Vector3(rt.transform.position.x, rt.transform.position.y, 0f);*/
        }
    }

    public void EndDrags()
    {
        dragable = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        rt2 = gameObject.GetComponent<RectTransform>();
        animat = gameObject.GetComponent<Animator>();
        trans = rt.transform.position;
        jumlahjawaban = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (masuk)
        {
        }
        else
        {
        }

        if (selesai)
        {
            rt.transform.position = trans;
            animat.enabled = true;
            animat.Play(0);
            selesai = false;
            dragable = false;
        }
    }
}
