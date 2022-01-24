using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PostScore : MonoBehaviour
{

    public string username;
    public string status,statusnya,statuslevel1,statuslevel2,statuslevel3,statuslevel4,statuslevel5,statuslevel6,statuslevel7;
    public bool suksesSimpan;
    public string progressURL,setProgressURL;
    public GetCommunicationWeb getCommunicationWeb;
    public GameObject gos1;

    void Start()
    {
        gos1 = GameObject.FindGameObjectWithTag("RequestManager");
        getCommunicationWeb = gos1.GetComponent<GetCommunicationWeb>();
        username = getCommunicationWeb.username;
        suksesSimpan = false;
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            GetHasilAll();
        }
        else
        {
            GetHasil();
        }
    }

    public void GetHasil()
    {
        StartCoroutine(GetProgress());
    }

    public void SetHasil()
    {
        StartCoroutine(SetProgress());
    }

    
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
        }
    }
    IEnumerator SetProgress()
    {

        WWWForm form = new WWWForm();
        form.AddField("id", 5);
        form.AddField("title", "Tes 5");
        form.AddField("author", "Tester 5");
        var www  = UnityWebRequest.Post("http://localhost:3000/posts", form);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Berhasil Menyimpan Data!");
                suksesSimpan = true;
            }


        // WWWForm form = new WWWForm();
        // form.AddField("username", username);
        // form.AddField("status", status);
        // var www  = UnityWebRequest.Post(setProgressURL, form);
        //     yield return www.SendWebRequest();
        //     if (www.isNetworkError || www.isHttpError)
        //     {
        //         Debug.Log(www.error);
        //     }
        //     else
        //     {
        //         Debug.Log("Berhasil Menyimpan Data!");
        //         suksesSimpan = true;
        //     }
    }
    public void GetHasilAll()
    {
        StartCoroutine(GetAllProgress());
    }

    IEnumerator GetAllProgress()
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
            string results = download.downloadHandler.text;
            statuslevel1 = StatusLevelUser.CreateFromJSON(results).level1;
            statuslevel2 = StatusLevelUser.CreateFromJSON(results).level2;
            statuslevel3 = StatusLevelUser.CreateFromJSON(results).level3;
            statuslevel4 = StatusLevelUser.CreateFromJSON(results).level4;
            statuslevel5 = StatusLevelUser.CreateFromJSON(results).level5;
            statuslevel6 = StatusLevelUser.CreateFromJSON(results).level6;
            statuslevel7 = StatusLevelUser.CreateFromJSON(results).level7;
        }
    }

}