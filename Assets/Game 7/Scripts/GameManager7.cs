using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager7 : MonoBehaviour
{
  public static List<Question7> questionList;
	public Question7[] questions;
	
	[Tooltip("Drag all camel gameobjects here")]
	public GameObject[] camelObjects;
	[Tooltip("Drag all hukma gameobjects here")]
	public GameObject[] hukmaObjects;
	[Tooltip("Drag all Result objects here ")]
	public GameObject[] resultObjects;
	[Tooltip("Drag all Hukma Container objects here ")]
	public RectTransform[] hukmaContainers;
	[Tooltip("Drag object under Camel > Bubble here")]
	public Image[] hijaiyahImages;
	
	[Tooltip("Text for displaying the amount of correct and wrong answers")]
	public Text correctText, wrongText;
	public GameObject correctPanel, wrongPanel, winPanel, losePanel;
	
	private int[] randomNumbers, temp_index;
	private int correct, wrong, counter = 0, temp_correct;
	private string jawaban;
  private bool clickAnswer = false;
	private GameObject temp_currentButton, temp_hukmaSelected, temp_hukmaPlaceholder;

  void Start()
  {
    temp_index = new int[hukmaObjects.Length];
    Time.timeScale = 1;
    if(questionList == null || questionList.Count == 0) {
      questionList = questions.ToList();
      ShuffleList(questionList);
    }
  
    GenerateNumber();
    RandomQuestion();
  }
	
	// function for gameobject camel onclick
	public void GetAnswer(string animation)
	{
    if(clickAnswer) {
      ToggleButtons(hukmaObjects, false);

      string temp_targetButton = EventSystem.current.currentSelectedGameObject.name;
      temp_currentButton.GetComponent<Animator>().SetBool("isSelected", false);
      temp_currentButton.GetComponent<Animator>().SetTrigger(animation);
      StartCoroutine(WaitGetAnswer(temp_targetButton));
      clickAnswer = false;
    }
	}

  public void Pause(bool pause)
  {
    if(pause) Time.timeScale = 0;
    else Time.timeScale = 1;
  }

  public void Quit() 
  {
    Application.Quit();
  }

  public void Restart() 
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
	
	// function for hukma onclick
	public void SetAnswer()
	{
    clickAnswer = true;
    if(temp_currentButton)
      temp_currentButton.GetComponent<Animator>().SetBool("isSelected", false);

		temp_currentButton = EventSystem.current.currentSelectedGameObject;
    temp_currentButton.GetComponent<Animator>().SetBool("isSelected", true);
    temp_currentButton.GetComponent<Animator>().ResetTrigger("Back");
		jawaban = temp_currentButton.name;
		ToggleButtons(camelObjects, true);
	}
	
	// Send selected hukma reference to temp_hukmaSelected
	// Move temp_hukmaSelected to the camel if correct
	void AnimateHukma(ref GameObject hukmaSelected, string animation)
	{	
		RectTransform hukmaSelectedTransform = temp_hukmaSelected.GetComponent<RectTransform>();
    hukmaSelected.GetComponent<Animator>().SetTrigger(animation);
	}
	
	void CheckEndState(string answer)
	{
    int randomCorrect = Random.Range(0, 2);
    DisableResult();

		if(answer == "correct" && correct == 10) {
			winPanel.SetActive(true);
			Time.timeScale = 0;
		} else if(answer == "correct") {
			correctPanel.SetActive(true);
      correctPanel.transform.GetChild(randomCorrect).gameObject.SetActive(true);
			StartCoroutine(WaitNextQuestion(true));
			RandomQuestion();
			ToggleButtons(hukmaObjects, true);
		}
		
		if(answer == "wrong" && wrong == 5) {
			losePanel.SetActive(true);
			Time.timeScale = 0;
		} else if(answer == "wrong") {
			wrongPanel.SetActive(true);
      wrongPanel.transform.GetChild(randomCorrect).gameObject.SetActive(true);
			StartCoroutine(WaitNextQuestion(false));

		}
	}
	
	void Correct()
	{
		correct += 1;
		CheckEndState("correct");
		correctText.text = correct.ToString();
		temp_correct = 0;
    ToggleButtons(hukmaObjects, true);
	}

  void DisableResult()
  {
    for(int i = 0; i < resultObjects.Length; i++) {
			resultObjects[i].gameObject.SetActive(false);
		} 
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
			hukmaObjects[i].GetComponent<Animator>().ResetTrigger("Left");
			hukmaObjects[i].GetComponent<Animator>().ResetTrigger("Middle");
			hukmaObjects[i].GetComponent<Animator>().ResetTrigger("Right");
		}
	}

  void BackAnimation()
  {
    for(int i = 0; i < hukmaObjects.Length; i++) {
      hukmaObjects[i].GetComponent<Animator>().SetTrigger("Back");
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

    if(temp_index.Length > 0) {
      for(int i = 0; i < temp_index.Length; i++) {
        // buttons[temp_index[i]].GetComponent<Image>().raycastTarget = !condition;
        Debug.Log(i);
      }
    }
	}
	
	void Wrong()
	{
    temp_correct = 0;
		wrong += 1;
    BackAnimation();
    CheckEndState("wrong");
		wrongText.text = wrong.ToString();
	}
	
	IEnumerator WaitNextQuestion(bool reset)
  {
    BackAnimation();
    yield return new WaitForSeconds(2.0f);
    correctPanel.SetActive(false);
    wrongPanel.SetActive(false);
  }	

  IEnumerator WaitGetAnswer(string temp_targetButton)
  {
    yield return new WaitForSeconds(2.5f);

    var arr = hukmaObjects.Select(gameObject => gameObject.name).ToArray();
    temp_index[temp_index.Length] = System.Array.IndexOf(arr, jawaban);
    if(temp_targetButton == jawaban) {
      temp_correct += 1;
      if(temp_correct == hukmaObjects.Length) {
        Correct();
      }
    } else {
      Wrong();
    }

    ToggleButtons(hukmaObjects, true);
    ToggleButtons(camelObjects, false);
  }

}
