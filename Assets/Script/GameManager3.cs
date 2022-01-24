using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager3 : MonoBehaviour
{
	public static List<Question3> questionList;
	public Question3[] questions;
	public GameObject[] questionBubbles;
	public Text correctText, wrongText, questionText;
	public GameObject winPanel, losePanel, correctPanel, wrongPanel;
	public string status1, status2;
  public AudioSource backsoundAudio;
	
	private AudioSource questionAudio;
	private GameObject[] bubbles;
	private int currentQuestion = 0;
	private int randomQuestionPosition, correct, wrong;
	private string answerLatin, answerHijaiyah, answerMark;
	private Sprite answerHijaiyahImage, answerHijaiyahImageAn;
	
    void Start()
    {
		questionAudio = GetComponent<AudioSource>();
		
        if(questionList == null || questionList.Count == 0) {
			questionList = questions.ToList<Question3>();
		}
		
		RandomQuestion();
    }
	
	void Update()
	{
		if(status1 != "" && status2 != "") {
			CheckAnswer();
			ResetStatus();
		}
	}

	/** 
	 * Function to check if status1 and status2 have the same value
	 * If it is, execute GetAnswer() with true value as argument and vice versa
	**/
	public void CheckAnswer()
    {
		if(status1 == answerLatin && status2 == answerMark)
			GetAnswer(true);
		else 
			GetAnswer(false);
    }
	
	IEnumerator CheckState()
	{
		yield return new WaitForSeconds(2f);
		correctPanel.SetActive(false);
		wrongPanel.SetActive(false);
		
		if(correct >= 14) {
			  winPanel.SetActive(true);
        backsoundAudio.Stop();
        Time.timeScale = 0;
      }
		else if(wrong >= 5){
			losePanel.SetActive(true);
      backsoundAudio.Stop(); 
      Time.timeScale = 0;
    }       
		else 
			RandomQuestion();
		
		ResetBubblePosition();
	}
	
	/** 
	 * Add score for every answer, correct or wrong
	 * If correct, move the first questionList value to the last
	 * 0[Aa] - 1[Da] - 2[Ra]   =>   0[Da] - 1[Ra] - 2[Aa]
	 *
	 * @param isCorrect : the answer generated from CheckAnswer()
	**/
	private void GetAnswer(bool isCorrect)
	{
    int randomCorrect = Random.Range(0, 4);
    int randomWrong = Random.Range(0, 2);
		if(isCorrect) {
			questionList.Add(questionList[currentQuestion]);
			correctPanel.SetActive(true);
      correctPanel.transform.GetChild(randomCorrect).gameObject.SetActive(true);
			correct += 1;
			questionList.RemoveAt(currentQuestion);
		} else {
			wrongPanel.SetActive(true);
      wrongPanel.transform.GetChild(randomWrong).gameObject.SetActive(true);
			wrong += 1;
		}
		
		SetScoreText();
		StartCoroutine(CheckState());
	}

	/**
	 * Get random soal from questionList
	 * and then pick random tanwin from soal.
	 * Set real answer and distractor into bubbles.
	**/
    private void RandomQuestion()
	{
		ResetStatus();
    DisablePanel();
		
		randomQuestionPosition = Random.Range(0, questionBubbles.Length);
		
		answerLatin = questionList[currentQuestion].soalLatin[randomQuestionPosition];
		answerHijaiyah = questionList[currentQuestion].soalHijaiyah;
		answerHijaiyahImage = questionList[currentQuestion].imageHijaiyah;
		answerHijaiyahImageAn = questionList[currentQuestion].imageHijaiyahAn;
		answerMark = FindVowel(questionList[currentQuestion].soalLatin[randomQuestionPosition]).ToString();

		questionAudio.clip = questionList[currentQuestion].soalAudio[randomQuestionPosition];
		questionText.text = questionList[currentQuestion].soalLatin[randomQuestionPosition];
		
		questionAudio.Play();
		
		for(int i = 0; i < questionBubbles.Length; i++) {
			if(i == randomQuestionPosition) {
				questionBubbles[i].name = answerLatin;
        if(answerMark == "a") {
          questionBubbles[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(35, 50);
          questionBubbles[i].transform.GetChild(0).GetComponent<Image>().sprite = answerHijaiyahImageAn;
        } else {
          questionBubbles[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(25, 50);
				  questionBubbles[i].transform.GetChild(0).GetComponent<Image>().sprite = answerHijaiyahImage;
        }
				//questionBubbles[i].transform.GetChild(0).GetComponent<Text>().text = answerHijaiyah;
			} else {
        if(answerMark == "a") {
				  questionBubbles[i].transform.GetChild(0).GetComponent<Image>().sprite = questionList[Random.Range(1, questionList.Count - i) + i].imageHijaiyahAn;
          questionBubbles[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(35, 50);
        } else {
				  questionBubbles[i].transform.GetChild(0).GetComponent<Image>().sprite = questionList[Random.Range(1, questionList.Count - i) + i].imageHijaiyah;
          questionBubbles[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(25, 50);
			}
		}
	}
  }
	/**
	 * Get the first vowel of word. O(n) Time complexity
	 * 
	 * @param word : string to evaluate
	**/
	private char FindVowel(string word)
	{
		if(word == null || word.Length == 0) return '\0';
		HashSet<char> vowels = new HashSet<char>() {'a', 'i', 'u', 'e', 'o'};
		foreach(char alphabet in word) {
			if (vowels.Contains(System.Char.ToLower(alphabet))) return System.Char.ToLower(alphabet);
		}
		return '\0';
	}
	
	private void ResetBubblePosition()
	{
		bubbles = GameObject.FindGameObjectsWithTag("Objek3");
		foreach(GameObject bubble in bubbles)
		{
			bubble.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
		}
	}
	
	private void ResetStatus()
	{
		status1 = "";
		status2 = "";
	}
	
	private void SetScoreText()
	{
		correctText.text = correct.ToString();
		wrongText.text = wrong.ToString();
	}

  private void DisablePanel()
  {
    for(int i = 0; i < correctPanel.transform.childCount; i++)
    {
      correctPanel.transform.GetChild(i).gameObject.SetActive(false);
    }

    for(int i = 0; i < wrongPanel.transform.childCount; i++)
    {
      wrongPanel.transform.GetChild(i).gameObject.SetActive(false);
    }
  } 
	
}
