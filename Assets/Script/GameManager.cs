using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
	public AudioClip correctAudio, wrongAudio;
  public GameObject correctPanel, wrongPanel, winPanel, losePanel;
  public Question[] questions;
	public Text correctScoreText, wrongScoreText;
  public static List<Question> questionList;
  public AudioSource backSound;

	private AudioSource audioSource;
  private GameObject[] buttonOptions;
	private bool isOver = false;
  private int correct, wrong, currentCounter = 0;

    void Start()
    {
		audioSource = GetComponent<AudioSource>();
		buttonOptions = GameObject.FindGameObjectsWithTag("Option");		

        if(questionList == null || questionList.Count == 0) 
        {
            questionList = questions.ToList<Question>();
        }
		
		ShuffleList(questionList);
        RandomQuestion();
    }

  public void AnswerCheck() {
		var button = EventSystem.current.currentSelectedGameObject;
		audioSource.Stop();
		
    if(button.name == "Correct") {
			button.gameObject.GetComponent<Animator>().SetTrigger("Terbang");
      CorrectAnswer(true);
			StartCoroutine("WaitNextQuestion");
    } else {
      CorrectAnswer(false);
			StartCoroutine("WaitNextQuestion");
    }
  }
	
  public void EnableButton() 
  {
    StopCoroutine("WaitEnableButton");
    StartCoroutine("WaitEnableButton");
  }

	void RandomQuestion()
	{
		CheckState();
		
    for(int i = 0; i < buttonOptions.Length; i++) {
      buttonOptions[i].GetComponent<Button>().interactable = false;
    }

		if(!isOver) {			
			int randomAnswerPosition = Random.Range(0, buttonOptions.Length);

			audioSource.clip = questionList[0].audioQuestion;

			for(int i = 0; i < buttonOptions.Length; i++)
			{
				if(i == randomAnswerPosition) {
					buttonOptions[randomAnswerPosition].name = "Correct";
					buttonOptions[randomAnswerPosition].transform.GetChild(0).GetComponent<Image>().sprite = questionList[0].question;
				} else {
					buttonOptions[i].name = "Wrong";
					buttonOptions[i].transform.GetChild(0).GetComponent<Image>().sprite = questionList[Random.Range(1, questionList.Count - i) + i].question;
				}
			}
		}
	}
	
	void CheckState()
	{
		if(questionList == null || questionList.Count == 0 || correct == 14) {
      winPanel.SetActive(true);
      backSound.Stop();
			audioSource.Stop();
			isOver = true;
		}
		
		if(wrong >= 5) {
			losePanel.SetActive(true);
      backSound.Stop();
			audioSource.Stop();
			isOver = true;
		}
	}
	
	void CorrectAnswer(bool isCorrect)
	{
		if(isCorrect) {
      int randomCorrect = Random.Range(0, correctPanel.transform.childCount);
			correct += 1;
			correctScoreText.text = correct.ToString();
      StartCoroutine(ShowPanel(randomCorrect, "correct"));
			
			ShiftList(questionList, currentCounter);
			currentCounter++;
		} else {
      int randomWrong = Random.Range(0, wrongPanel.transform.childCount);
			wrong += 1;
			wrongScoreText.text = wrong.ToString();
      StartCoroutine(ShowPanel(randomWrong, "wrong"));
		}
	}
	
	void ShiftList<T>(List<T> list, int counter)
	{
		int n = list.Count;
		int beforeLast = n - (counter + 1);
		T t = list[0];
		list[0] = list[beforeLast];
		list[beforeLast] = t;
	}

    void ShuffleArray<T>(T[] array)
    {
        int n = array.Length;
        for(int i = 0; i < n; i++) 
        {
            int r = i + Random.Range(0, n - i);
            T t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }
	
	void ShuffleList<T>(List<T> list)
	{
		int n = list.Count;
		for(int i = 0; i < n; i++)
		{
			int r = i + Random.Range(0, n - i);
			T t = list[r];
			list[r] = list[i];
			list[i] = t;
		}
	}

  void ToggleButtonOption(bool condition)
  {
  }

    IEnumerator WaitNextQuestion()
    {
        yield return new WaitForSeconds(2.0f);
		correctPanel.SetActive(false);
		wrongPanel.SetActive(false);
        RandomQuestion();
    }

  IEnumerator ShowPanel(int child, string condition)
  {
    if(condition == "correct") {
      correctPanel.SetActive(true);
      correctPanel.transform.GetChild(child).gameObject.SetActive(true);
    } else {
      wrongPanel.SetActive(true);
      wrongPanel.transform.GetChild(child).gameObject.SetActive(true);
    }
    yield return new WaitForSeconds(2f);
    if(condition == "correct") {
      correctPanel.SetActive(false);
      correctPanel.transform.GetChild(child).gameObject.SetActive(false); 
    } else {
      wrongPanel.SetActive(false);
      wrongPanel.transform.GetChild(child).gameObject.SetActive(false);
    }
  }

  IEnumerator WaitEnableButton()
  {
    for(int i = 0; i < buttonOptions.Length; i++) {
      buttonOptions[i].GetComponent<Button>().interactable = false;
    }
    yield return new WaitForSeconds(3f);
    for(int i = 0; i < buttonOptions.Length; i++) {
      buttonOptions[i].GetComponent<Button>().interactable = true;
    }
  }
}
