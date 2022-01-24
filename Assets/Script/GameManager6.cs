using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager6 : MonoBehaviour
{
    public static List<Question6> questionList;
	public Question6[] questions;
	
	[Tooltip("Drag all camel gameobjects here")]
	public GameObject[] camelObjects;
	[Tooltip("Drag all hukma gameobjects here")]
	public GameObject[] hukmaObjects;
	[Tooltip("Drag all Hukma Container objects here ")]
	public RectTransform[] hukmaContainers;
	[Tooltip("Drag object under Camel > Bubble here")]
	public Image[] hijaiyahImages;
	
	[Tooltip("Text for displaying the amount of correct and wrong answers")]
	public Text correctText, wrongText;
	public GameObject correctPanel, wrongPanel, winPanel, losePanel;
	
	private int[] randomNumbers;
	private int correct, wrong, counter = 0, temp_correct;
	private string jawaban;
	private GameObject temp_currentButton, temp_hukmaSelected, temp_hukmaPlaceholder;
	
    void Start()
    {
		Time.timeScale = 1;
        if(questionList == null || questionList.Count == 0) {
			questionList = questions.ToList();
			ShuffleList(questionList);
		}
		
		GenerateNumber();
		RandomQuestion();
		ResetAnimation();
		ResetHukmaPosition();
    }
	
	// function for gameobject camel onclick
	public void GetAnswer()
	{
		temp_currentButton = EventSystem.current.currentSelectedGameObject;
		temp_hukmaPlaceholder = temp_currentButton.transform.Find("Placeholder Hukma").gameObject;
		
		if(temp_currentButton.name == jawaban) {
			AnimateHukma(ref temp_hukmaSelected, true, temp_hukmaPlaceholder, true);
			
			temp_currentButton.GetComponent<Animator>().SetBool("isCorrect", true);
			temp_correct += 1;
			
			if(temp_correct == hukmaObjects.Length) {
				Correct();
			}
		} else {
			AnimateHukma(ref temp_hukmaSelected, false, null, true);
			Wrong();
		}
		
		ToggleButtons(camelObjects, false);
	}
	
	// function for hukma onclick
	public void SetAnswer()
	{
		temp_currentButton = EventSystem.current.currentSelectedGameObject;
		jawaban = temp_currentButton.name;
		AnimateHukma(ref temp_currentButton, false, null, false);
		ToggleButtons(camelObjects, true);
	}
	
	// Send selected hukma reference to temp_hukmaSelected
	// Move temp_hukmaSelected to the camel if correct
	void AnimateHukma(ref GameObject hukmaSelected, bool isCorrect, GameObject destination = null, bool animateNow = false)
	{	
		if(animateNow) {
		RectTransform hukmaSelectedTransform = temp_hukmaSelected.GetComponent<RectTransform>();
			if(isCorrect) {
				hukmaSelectedTransform.parent = destination.GetComponent<RectTransform>();
				hukmaSelectedTransform.anchoredPosition = new Vector2(0, 0);
				hukmaSelectedTransform.sizeDelta = destination.GetComponent<RectTransform>().sizeDelta;
				hukmaSelected.GetComponent<Image>().raycastTarget = false;
				temp_hukmaSelected.GetComponent<Animator>().SetBool("isCorrect", isCorrect);
			} else {
				temp_hukmaSelected.GetComponent<Animator>().SetTrigger("isWrong");
			}
			
		} else {
			temp_hukmaSelected = hukmaSelected;
		}
	}
	
	void CheckEndState(string answer)
	{
		if(answer == "correct" && correct == 10) {
			winPanel.SetActive(true);
			Time.timeScale = 0;
		} else if(answer == "correct") {
			correctPanel.SetActive(true);
			StartCoroutine(WaitNextQuestion(true));
			RandomQuestion();
			ToggleButtons(hukmaObjects, true);
		}
		
		if(answer == "wrong" && wrong == 5) {
			losePanel.SetActive(true);
			Time.timeScale = 0;
		} else if(answer == "wrong") {
			wrongPanel.SetActive(true);
			StartCoroutine(WaitNextQuestion(false));
		}
	}
	
	void Correct()
	{
		correct += 1;
		CheckEndState("correct");
		correctText.text = correct.ToString();
		temp_correct = 0;
	}
	
	void GenerateNumber()
	{
		randomNumbers = new int[hukmaObjects.Length];
		for(int i = 0; i < randomNumbers.Length; i++) {
			randomNumbers[i] = i;
		}
	}
	
    void RandomQuestion()
	{	
		ShuffleArray(randomNumbers);
		
		if(counter == questionList.Count) counter = 0;
		
		for(int i = 0; i < hijaiyahImages.Length; i++) {
			hijaiyahImages[randomNumbers[i]].sprite = questionList[counter].questionHijaiyah;
			camelObjects[randomNumbers[i]].name = questionList[counter].questionLatin;
			hukmaObjects[randomNumbers[randomNumbers.Length - 1 - i]].name = questionList[counter].questionLatin;
			hukmaObjects[randomNumbers[randomNumbers.Length - 1 - i]].GetComponent<AudioSource>().clip = questionList[counter].questionAudio;
			counter++;
		}
	}
	
	void ResetAnimation()
	{
		for(int i = 0; i < hukmaObjects.Length; i++) {
			hukmaObjects[i].GetComponent<Animator>().SetBool("isCorrect", false);
			camelObjects[i].GetComponent<Animator>().SetBool("isCorrect", false);
		}
	}
	
	void ResetHukmaPosition()
	{
		for(int i = 0; i < hukmaObjects.Length; i++) {
			hukmaObjects[i].GetComponent<RectTransform>().parent = hukmaContainers[i];
			hukmaObjects[i].GetComponent<RectTransform>().sizeDelta = new Vector2(70, 180);
			hukmaObjects[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
		}
	}
	
	void ShuffleArray<T>(T[] array)
	{
		int n = array.Length;
		for(int i = 0; i < n; i++) {
			int r = i + Random.Range(0, n - i);
			T t = array[r];
			array[r] = array[i];
			array[i] = t;
		}
	}
	
	void ShuffleList<T>(List<T> list)
	{
		int n = list.Count;
		for(int i = 0; i < n; i++) {
			int r = i + Random.Range(0, n - i);
			T t = list[r];
			list[r] = list[i];
			list[i] = t;
		}
	}
	
	void ToggleButtons(GameObject[] buttons, bool condition)
	{
		for(int i = 0; i < buttons.Length; i++) {
			buttons[i].GetComponent<Image>().raycastTarget = condition;
		}
	}
	
	void Wrong()
	{
		wrong += 1;
		CheckEndState("wrong");
		wrongText.text = wrong.ToString();
		temp_hukmaSelected.GetComponent<Animator>().SetTrigger("isWrong");			
	}
	
	IEnumerator WaitNextQuestion(bool reset)
    {
		yield return new WaitForSeconds(2.0f);
		correctPanel.SetActive(false);
		wrongPanel.SetActive(false);
		if(reset) {
			ResetAnimation();
			ResetHukmaPosition();			
		}
    }
	
}
