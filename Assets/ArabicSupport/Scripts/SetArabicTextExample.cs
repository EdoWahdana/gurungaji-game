using UnityEngine;
using System.Collections;
using ArabicSupport;
using UnityEngine.UI;
public class SetArabicTextExample : MonoBehaviour {
	
	public string text;
	public Text teks;
	// Use this for initialization
	void Start () {
		teks = gameObject.GetComponent<Text>();
		teks.text = "This sentence (wrong display):\n" + text +
			"\n\nWill appear correctly as:\n" + ArabicFixer.Fix(text, false, false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
