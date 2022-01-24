using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GetCommunicationWeb : MonoBehaviour
{
    public Text teksUsername;
    public string username;
    void Awake()
    {
        //this.ListHREFParameters();
    }
    
    void ListHREFParameters()
    {
        Dictionary<string, string> prms = ParamParse.GetBrowserParameters();
        if (prms.Count == 0)
            return;

        string output = "Listing Link Parameters:\n";
        foreach (KeyValuePair<string, string> kvp in prms)
        {
            if (string.IsNullOrEmpty(kvp.Value) == true)
                output += "Maaf Anda Belum Login, Hasil Permainan Anda Tidak Akan Tersimpan Dengan Baik.";
            else if (kvp.Key == "username")
            {
                output += "Selamat Datang " + kvp.Value; username = kvp.Value;
            }
            else
                output += "Paramater yang anda masukan salah, parameter yang diterima adalah '/?username=value'";
        }
        teksUsername.text = output;
    }

}