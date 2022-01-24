using UnityEngine;
using System.Collections;
using ArabicSupport;
using UnityEngine.UI;
public class FixGUITextCS : MonoBehaviour {
	
	public string text;
	public Text teks;
	public bool tashkeel = true;
	public bool hinduNumbers = true;
	
	// Use this for initialization
	void Start () {
		teks = gameObject.GetComponent<Text>();
		teks.text = ArabicFixer.Fix(text, tashkeel, hinduNumbers);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
