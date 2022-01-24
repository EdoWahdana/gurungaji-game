using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{	
    public GameManager3 gta3;
	public CanvasGroup placeHolder1, placeHolder2, placeHolder3;
	public Animator animatorPlaceholder1, animatorPlaceholder2, animatorPlaceholder3;
	public bool isDrag = true;
	
	[SerializeField]
	private Canvas canvas;
	
	private Animation animation;
	private CanvasGroup canvasGroup;
	private RectTransform rectTransform;

	void Start()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		rectTransform = GetComponent<RectTransform>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		canvasGroup.alpha = .5f;
		canvasGroup.blocksRaycasts = false;
		
		if(gameObject.name == "a" || gameObject.name == "i" || gameObject.name == "u") {
			placeHolder2.alpha = .5f;
			animatorPlaceholder1.SetBool("isDrag", true);
			animatorPlaceholder3.SetBool("isDrag", true);
		} else {
			placeHolder1.alpha = .5f;
			placeHolder3.alpha = .5f;
			animatorPlaceholder2.SetBool("isDrag", true);
		}
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		if(isDrag){
		rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
		}
	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
		canvasGroup.alpha = 1f;
		canvasGroup.blocksRaycasts = true;
		
		placeHolder1.alpha = 1f;
		placeHolder2.alpha = 1f;
		placeHolder3.alpha = 1f;
		
		animatorPlaceholder1.SetBool("isDrag", false);
		animatorPlaceholder2.SetBool("isDrag", false);
		animatorPlaceholder3.SetBool("isDrag", false);
	}
}
