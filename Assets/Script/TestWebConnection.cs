using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class TestWebConnection : MonoBehaviour
{
    public string username,progressURL,status;
    public int scenetoload;
 
    IEnumerator GetProgress()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);

        var download = UnityWebRequest.Post(progressURL, form);


        yield return download.SendWebRequest();

        if (download.isNetworkError || download.isHttpError)
        {
            Debug.Log(download.error);
        }
        else
        {
            Debug.Log(download.downloadHandler.text);
            status = download.downloadHandler.text;
            SceneManager.LoadScene(scenetoload);
        }
    }

    // Start is called before the first frame update
    public void Mulai()
    {
        StartCoroutine(GetProgress());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
