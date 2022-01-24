using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    public GameManager3 gta3;
	public Drag dragA, dragI, dragU;
	
	public void OnDrop(PointerEventData eventData)
	{		
		DisableDragTanda();
		if(gameObject.name == "PlaceHolder1" && (eventData.pointerDrag.name == "a" || eventData.pointerDrag.name == "u")) {
			gta3.status2 = eventData.pointerDrag.name;
			
		}
		else if(gameObject.name == "PlaceHolder1" && (eventData.pointerDrag.name != "a" || eventData.pointerDrag.name != "u")) {
			gta3.status2 = "Wrong";
			
		}
		
		if(gameObject.name == "PlaceHolder2") {
			gta3.status1 = eventData.pointerDrag.name;
			EnableDragTanda();
		}
		
		if(gameObject.name == "PlaceHolder3" && eventData.pointerDrag.name == "i") {
			gta3.status2 = eventData.pointerDrag.name;
			
		}
		else if(gameObject.name == "PlaceHolder3" && eventData.pointerDrag.name != "i") {
			gta3.status2 = "Wrong";
			
		}
		
		//eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
	}
	
	private void EnableDragTanda()
	{
		dragA.isDrag = true;
		dragI.isDrag = true;
		dragU.isDrag = true;
	}
	
	private void DisableDragTanda()
	{
		dragA.isDrag = false;
		dragI.isDrag = false;
		dragU.isDrag = false;
	}
}
