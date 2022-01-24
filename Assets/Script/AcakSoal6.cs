using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AcakSoal6 : MonoBehaviour
{
    public List<Sprite> huruf;
    public Slider slid;
    public int[] list1, list2, list3;

    public string[] jawaban;

    public Sprite[] karakters, teksPendukungs;
    public Image karakter, teksPendukung;

    public Image[] lokasihuruf;

    public Text teksSoal,teksStatus,teksTotalBenar,teksTotalSalah,teksTotal;

    public float soalCountdown, soalCountdownBegin,timerSoal,timerSoalAwal, timeSelanjutnyaSalah,timeAwalSalah,timerNow,timerStart, timeSelanjutnya, timeAwal;
    public int skor, soalsaaatini,totalbenar,totalsalah,saatini, levelload,soalnya;

    public AudioSource audioSource;

    public string status,apakahbenar,username,progressURL,setProgressURL;

    public bool isCountdownSoal, isCountDownSalah,suksesSimpan,isStart,isCountDown;

    public Animator animall;

    public GameObject gos1, gos2, go3, goKosong, canvas1, canvas2, goSelesai, goKurang, goSalah, goHasil;

    public CheckSwipeState ofs;

    public AudioClip[] audioPendukung;


    // Start is called before the first frame update
    void Start()
    {
        soalCountdown = soalCountdownBegin;
        Acak();
    }


    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Resumd()
    {
        Time.timeScale = 1;
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
            Acak();
        }
    }
    IEnumerator SetProgress()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("status", status);
        var www = UnityWebRequest.Post(setProgressURL, form);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            suksesSimpan = false;
        }
        else
        {
            Debug.Log("Berhasil Menyimpan Data!");
            suksesSimpan = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCountdownSoal)
        {
            soalCountdown -= Time.deltaTime * 1f;
            slid.value = soalCountdown;
        }
        else
        {
            soalCountdown = soalCountdownBegin;
        }
        if (soalCountdown < 0)
        {
            CekJawaban();
        }

        teksStatus.text = status;
        teksTotalBenar.text = totalbenar.ToString();
        teksTotalSalah.text = totalsalah.ToString();
        teksTotal.text = "Total Soal : " + saatini.ToString();

        if (totalbenar == 10 || apakahbenar == "selesai" && !audioSource.isPlaying)
        {
            goSelesai.SetActive(true);
            status = "selesai";
            Pause();
            SetHasil();
        }
        else
        {
            goSelesai.SetActive(false);
            
            if (totalsalah == 5)
            {
                goSalah.SetActive(true);
                status = "belum";
                Pause();
                SetHasil();
            }
            else
            {
                goSalah.SetActive(false);
            }

        }

        if (timerSoal < 0 && totalsalah < 5)
        {
            SelanjutnyaSalah();
        }

        if (!isCountDownSalah)
        {
            timeSelanjutnyaSalah = timeAwalSalah;
        }
        else
        {
            timeSelanjutnyaSalah -= Time.deltaTime * Time.timeScale;
        }

        if (timeSelanjutnyaSalah < 0)
        {
            //totalsalah += 1;
            MulaiSalah();
        }

        if (!isStart)
        {
            timerNow = timerStart;
        }
        else
        {
            timerNow -= Time.deltaTime * Time.timeScale;
        }

        if (timerNow < 0)
        {
            MuatLevel(levelload);
        }

        if (isCountDown)
        {
            timeSelanjutnya -= Time.deltaTime * Time.timeScale;
            goHasil.SetActive(true);
        }
        else
        {
            timeSelanjutnya = timeAwal;
        }

        if (timeSelanjutnya < 0)
        {
            isCountDown = false;
            if (!audioSource.isPlaying)
            {
                Mulai();
            }
        }
        if (status == "belum")
        {
            apakahbenar = "belum";
        }
        else if (status == "selesai")
        {
            apakahbenar = "selesai";
        }
        else if (apakahbenar == "")
        {
            apakahbenar = "";
        }


    }

    public void CobaLagi(int loadLevel)
    {
        levelload = loadLevel;
        status = "belum";
        apakahbenar = "belum";
        SetHasil();
        if (suksesSimpan)
            isStart = true;
    }

    public void MuatLevel(int loadLevel)
    {
        SceneManager.LoadScene(loadLevel);
    }



    public void SelanjutnyaSalah()
    {
        isCountdownSoal = false;
        isCountDown = true;
        goHasil.SetActive(true);
        skor = skor;
        int karaktermuncul = Random.Range(2, 3);
        karakter.sprite = karakters[karaktermuncul];
        audioSource.clip = audioPendukung[3];
        audioSource.Play();
        totalsalah += 1;
        teksPendukung.sprite = teksPendukungs[3];
    }

    public void Benar()
    {
        skor += 1;

        int karaktermuncul = Random.Range(0, 1);
        int audioteks = Random.Range(0, 2);
        soalsaaatini += 1;
        totalbenar += 1;
        karakter.sprite = karakters[karaktermuncul];
        audioSource.clip = audioPendukung[audioteks];
        audioSource.Play();
        teksPendukung.sprite = teksPendukungs[audioteks];
        isCountDown = true;
        goHasil.SetActive(true);
    }

    public void Salah()
    {
        totalsalah += 1;
        soalsaaatini += 1;
        skor = skor;
        int karaktermuncul = Random.Range(2, 3);
        karakter.sprite = karakters[karaktermuncul];
        audioSource.clip = audioPendukung[3];
        audioSource.Play();
        teksPendukung.sprite = teksPendukungs[3];
        isCountDownSalah = true;
        goHasil.SetActive(true);
    }

    public void CekJawaban()
    {
        
        isCountdownSoal = false;
        
        if (jawaban[soalsaaatini] == ofs.jawabandia)
        {
            Benar();
        }
        else
        {
            Salah();
        }
    }

    public void Mulai()
    {
        goHasil.SetActive(false);
        isCountDown = false;
        isCountdownSoal = true;
        Acak();
    }

    public void MulaiSalah()
    {
        isCountDownSalah = false;
        goHasil.SetActive(false);
        isCountdownSoal = true;
        Acak();
    }

    public void Acak()
    {
        int list1saatini = list1[soalsaaatini];
        int list2saatini = list2[soalsaaatini];
        int list3saatini = list3[soalsaaatini];
        teksSoal.text = huruf[list1saatini].name.ToUpper();
        jawaban[soalsaaatini] = huruf[list1saatini].name;
        int lokasisoal = Random.Range(0, 3);

        if (lokasisoal == 0)
        {
            lokasihuruf[0].sprite = huruf[list1saatini];
            lokasihuruf[1].sprite = huruf[list2saatini];
            lokasihuruf[2].sprite = huruf[list3saatini];
            lokasihuruf[0].name = huruf[list1saatini].name;
            lokasihuruf[1].name = huruf[list2saatini].name;
            lokasihuruf[2].name = huruf[list3saatini].name;
        }
        else if (lokasisoal == 1)
        {
            lokasihuruf[0].sprite = huruf[list2saatini];
            lokasihuruf[1].sprite = huruf[list1saatini];
            lokasihuruf[2].sprite = huruf[list3saatini];
            lokasihuruf[0].name = huruf[list2saatini].name;
            lokasihuruf[1].name = huruf[list1saatini].name;
            lokasihuruf[2].name = huruf[list3saatini].name;
        }
        else if (lokasisoal == 2)
        {
            lokasihuruf[0].sprite = huruf[list3saatini];
            lokasihuruf[1].sprite = huruf[list2saatini];
            lokasihuruf[2].sprite = huruf[list1saatini];
            lokasihuruf[0].name = huruf[list3saatini].name;
            lokasihuruf[1].name = huruf[list2saatini].name;
            lokasihuruf[2].name = huruf[list1saatini].name;
        }
    }
}
