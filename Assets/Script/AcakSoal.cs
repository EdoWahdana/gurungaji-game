using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AcakSoal : MonoBehaviour
{
    [Header("Animasi")]
    public Text teksar1, teksar2, teksar3;
    public RawImage raws1,raws2,raws3;
    public Image balons1, balons2, balons3,tali1,tali2,tali3;

    [Header("Reference Soal")]
    public string[] soal;
    public Sprite[] hijaiyahImage;
    public string[] jawaban;
    public AudioClip[] audioClip, audioPendukung;
    public AudioSource audioSource;
    public Sprite[] balon, karakters, teksPendukungs, bintangs;
    public int pengecoh1acak, pengecoh2acak;
    public AudioSource ostAudio;
    public string apakahbenar;

    public int soalkeacak, soalsebelumnya, lokasisoal, balonacak1, balonacak2, balonacak3, saatini, totalbenar, totalsalah;
    public string jawabannya;

    public int skor, keacak1, keacak2, levelload;

    public int Rand1, Rand2, Rand3;
    public int panjang;
    public List<int> list1 = new List<int>() { 0, 1, 15, 11, 4, 12, 3, 6, 8, 5, 2, 7, 13, 9, 10 };
    public List<int> list2 = new List<int>() { 4, 11, 13, 1, 6, 5, 0, 2, 7, 9, 8, 10, 14, 1, 4 };
    public List<int> list3 = new List<int>() { 1, 2, 10, 3, 5, 7, 9, 2, 15, 12, 0, 14, 4, 6, 7 };
    public List<string> soalnya, pengecohnya1, pengecohnya2 = new List<string>();
    public List<int> soalsaatini, pengecoh1, pengecoh2, pengecoh3, pengecoh4, audionya;
    public List<AudioClip> audioClips;
    public List<Sprite> hijaiyahImagePengecoh1, hijaiyahImagePengecoh2;

    public float timeAwal, timeSelanjutnya, timerStart, timerNow;
    public bool isCountDown, isStart;

    [Header("Reference Unity")]
    public GameObject go1, go2, go3, goSelesai,goSalah,goHasil,goKurang,goKosong;
    public Animator anim1, anim2, anim3;
    public Text teks1, teks2, teks3, teksTotal, teksTotalBenar, teksTotalSalah,teksStatus;
    public Image hijaiyahKiri, hijaiyahTengah, hijaiyahKanan;
    public Image balon1, balon2, balon3, karakter, teksPendukung, bintang;
    public AudioSource audioBenar, audioSalah;
    public TextMeshProUGUI winScoreCorrect, winScoreWrong, loseScoreCorrect, loseScoreWrong;

    [Header("Database/Web")]
    public string progressURL, setProgressURL;
    public string username;
    public string status;
    public bool suksesSimpan;
    public GameObject gos2;
    public GetCommunicationWeb getCommunicationWeb;

    private bool m_stateJawabBenar = true;

    void Start()
    {
        Time.timeScale = 1;
        gos2 = GameObject.FindGameObjectWithTag("RequestManager");
        getCommunicationWeb = gos2.GetComponent<GetCommunicationWeb>();
        username = getCommunicationWeb.username;
        saatini = 0;
        saatini = 0;
        audioClips = new List<AudioClip>(new AudioClip[panjang]);
        soalnya = new List<string>(new string[panjang]);
        soalsaatini = new List<int>(new int[panjang]);
        pengecohnya1 = new List<string>(new string[panjang]);
        pengecohnya2 = new List<string>(new string[panjang]);
        hijaiyahImagePengecoh1 = new List<Sprite>();
        hijaiyahImagePengecoh2 = new List<Sprite>();

        for (int j = 0; j < panjang; j++)
        {
            soalsaatini[j] = list1[j];
            pengecoh1[j] = list2[j];
            pengecoh2[j] = list3[j];
        }

        for (int k = 0; k < panjang; k++)
        {
            soalkeacak = soalsaatini[k];
            pengecoh1acak = pengecoh1[k];
            pengecoh2acak = pengecoh2[k];
            audioClips[k] = audioClip[soalkeacak];
            soalnya[k] = soal[soalkeacak];
            pengecohnya1[k] = soal[pengecoh1acak];
            pengecohnya2[k] = soal[pengecoh2acak];
        }
        Acak();
        //GetHasil();
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
            Debug.Log(www.downloadHandler);
            suksesSimpan = true;
        }
    }
    public void Selanjutnya()
    {
        isCountDown = true;
    }

    public void CobaLagi(int loadLevel)
    {
        levelload = loadLevel;
        status = "belum";
        apakahbenar = "belum";
        UlangLevel();
        //SetHasil();
    }

    public void UlangLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MuatLevel(int loadLevel)
    {
        SceneManager.LoadScene(loadLevel);
    }


    public void Jawab1()
    {
            jawabannya = go1.name;
        if (jawabannya == soalnya[saatini])
        {
            skor += 10;
            int karaktermuncul = Random.Range(0, 2);
            int audioteks;
            if(karaktermuncul == 0) {
              audioteks = Random.Range(0, 3);
            } else {
              audioteks = Random.Range(3, 6);
            }
            audioBenar.Play();

            karakter.sprite = karakters[karaktermuncul];
            audioSource.clip = audioPendukung[audioteks];
            audioSource.Play();
            teksPendukung.sprite = teksPendukungs[audioteks];
            totalbenar += 1;
            m_stateJawabBenar = true;
        }
        else
        {
            skor = skor;
            int karaktermuncul = Random.Range(2, 4);
            karakter.sprite = karakters[karaktermuncul];
            if(karaktermuncul == 2) {
              audioSource.clip = audioPendukung[6];
            } else {
              audioSource.clip = audioPendukung[7];
            }
            audioSalah.Play();
            audioSource.Play();
            teksPendukung.sprite = teksPendukungs[6];
            totalsalah += 1;
            m_stateJawabBenar = false;
        }
        balons1.enabled = false;
        raws1.enabled = true;
        teksar1.enabled = false;
        tali1.enabled = false;
    }

    public void Jawab2()
    {
            jawabannya = go2.name;
        if (jawabannya == soalnya[saatini])
        {
            skor += 10;
            int karaktermuncul = Random.Range(0, 2);
            int audioteks;
            audioBenar.Play();
            if(karaktermuncul == 0) {
              audioteks = Random.Range(0, 3);
            } else {
              audioteks = Random.Range(3, 6);
            }

            karakter.sprite = karakters[karaktermuncul];
            audioSource.clip = audioPendukung[audioteks];
            audioSource.Play();
            totalbenar += 1;
            teksPendukung.sprite = teksPendukungs[audioteks];
            m_stateJawabBenar = true;
        }
        else
        {
            skor = skor;
            int karaktermuncul = Random.Range(2, 4);
            karakter.sprite = karakters[karaktermuncul];
            if(karaktermuncul == 2) {
              audioSource.clip = audioPendukung[6];
            } else {
              audioSource.clip = audioPendukung[7];
            }

            audioSalah.Play();
            audioSource.Play();
            totalsalah += 1;
            teksPendukung.sprite = teksPendukungs[6];
            m_stateJawabBenar = false;
        }
        balons2.enabled = false;
        raws2.enabled = true;
        teksar2.enabled = false;
        tali2.enabled = false;

    }
    public void Jawab3()
    {
            jawabannya = go3.name;
        if (jawabannya == soalnya[saatini])
        {
            skor += 10;
            int karaktermuncul = Random.Range(0, 2);
            int audioteks;
            audioBenar.Play();
            if(karaktermuncul == 0) {
              audioteks = Random.Range(0, 3);
            } else {
              audioteks = Random.Range(3, 6);
            }

            karakter.sprite = karakters[karaktermuncul];
            audioSource.clip = audioPendukung[audioteks];
            audioSource.Play();
            totalbenar += 1;
            teksPendukung.sprite = teksPendukungs[audioteks];
            m_stateJawabBenar = true;
        }
        else
        {
            skor = skor;
            int karaktermuncul = Random.Range(2, 4);
            karakter.sprite = karakters[karaktermuncul];
            if(karaktermuncul == 2) {
              audioSource.clip = audioPendukung[6];
            } else {
              audioSource.clip = audioPendukung[7];
            }

            audioSalah.Play();
            audioSource.Play();
            totalsalah += 1;
            teksPendukung.sprite = teksPendukungs[6];
            m_stateJawabBenar = false;
        }
        balons3.enabled = false;
        raws3.enabled = true;
        teksar3.enabled = false;
        tali3.enabled = false;

    }
    public void Mulai()
    {
        // if ((totalsalah == 5) || ((totalsalah == 1 && totalbenar == 13 )|| (totalsalah == 3 &&totalbenar==11) || (totalsalah == 4 && totalbenar==10) || (totalbenar == 12 &&totalsalah==2))) { 
        //     saatini = 1;
        //     audioSource.volume = 0f;
        //     anim1.enabled = false;
        //     anim2.enabled = false;
        //     anim3.enabled = false;
        //     goKurang.SetActive(true);
        //     status = "belum";       
        // } else{
            // Cek jika state jawaban: 
            // benar: tambah saatini pada fungsi Acak() 
            // salah: saatini pada fungsi Acak() tidak bertambah
            if(m_stateJawabBenar)
                Acak(true);
            else 
                Acak(false);

            goHasil.SetActive(false);
            bintang.sprite = bintangs[1];
            isCountDown = false;
        // }
    }

    public void Acak(bool benar = true)
    {
        balons1.enabled = true;
        raws1.enabled = false;
        teksar1.enabled = true;
        tali1.enabled = true;
        balons2.enabled = true;
        raws2.enabled = false;
        teksar2.enabled = true;
        tali2.enabled = true;
        balons3.enabled = true;
        raws3.enabled = false;
        teksar3.enabled = true;
        tali3.enabled = true;

        // Hanya pergi ke soal selanjutnya jika benar
        if(!benar)
            saatini = saatini;
        else 
            saatini += 1;
        
        // saatini += 1;
        balonacak1 = Random.Range(0, 11);
        balonacak2 = Random.Range(0, 11);
        balonacak3 = Random.Range(0, 11);
        if (saatini != 15 || status!="selesai")
        {
            lokasisoal = Random.Range(0, 3);
            if (lokasisoal == 0)
            {
                go1.name = soalnya[saatini];
                go2.name = pengecohnya1[saatini];
                go3.name = pengecohnya2[saatini];
                teks1.text = soalnya[saatini];
                teks2.text = pengecohnya1[saatini];
                teks3.text = pengecohnya2[saatini];
            }
            else if (lokasisoal == 1)
            {
                go1.name = pengecohnya1[saatini];
                go2.name = soalnya[saatini];
                go3.name = pengecohnya2[saatini];
                teks1.text = pengecohnya1[saatini];
                teks2.text = soalnya[saatini];
                teks3.text = pengecohnya2[saatini];
            }
            else if (lokasisoal == 2)
            {
                go1.name = pengecohnya2[saatini];
                go2.name = pengecohnya1[saatini];
                go3.name = soalnya[saatini];
                teks1.text = pengecohnya2[saatini];
                teks2.text = pengecohnya1[saatini];
                teks3.text = soalnya[saatini];
            }
            balon1.sprite = balon[balonacak1];
            balon2.sprite = balon[balonacak2];
            balon3.sprite = balon[balonacak3];
            anim1.Play("Mulai", -1, 0f);
            anim2.Play("Mulai", -1, 0f);
            anim3.Play("Mulai", -1, 0f);
            audioSource.clip = audioClips[saatini];
            audioSource.Play();
        }
    }


    // Update is called once per frame
    void Update()
    {
        teksStatus.text = status;
        teksTotalBenar.text = totalbenar.ToString();
        teksTotalSalah.text = totalsalah.ToString();
        teksTotal.text = "Total Soal : " + saatini.ToString();
        if (suksesSimpan)
        {
            suksesSimpan = false;
        }

        if (totalbenar == 14 && !audioSource.isPlaying)
        {
            goSelesai.SetActive(true);
            ostAudio.Stop();
            status = "selesai";
            winScoreCorrect.text = totalbenar.ToString();
            winScoreWrong.text = totalsalah.ToString();
        }
        else
        {
            goSelesai.SetActive(false);
            // if ((totalsalah == 1 || totalsalah == 2 || totalsalah == 3 || totalsalah == 4) && (totalbenar == 13 || totalbenar == 12 || totalbenar == 11 || totalbenar == 10)&&saatini==14 && !audioSource.isPlaying)
            // {
            //     saatini = 1;
            //     audioSource.volume = 0f;
            //     anim1.enabled = false;
            //     anim2.enabled = false;
            //     anim3.enabled = false;
            //     goKurang.SetActive(true);
            //     status = "belum";
            //     //SetHasil();
            // }
            // else
            // {
            //     goKurang.SetActive(false);
            // }
            if (totalsalah == 5 && !audioSource.isPlaying)
            {
                goSalah.SetActive(true);
                ostAudio.Stop();
                status = "belum";
                loseScoreCorrect.text = totalbenar.ToString();
                loseScoreWrong.text = totalsalah.ToString();
                Time.timeScale = 0;
                //SetHasil();
            }
            else
            {
                goSalah.SetActive(false);
            }

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
            isStart = false;
        }

        if (!isCountDown)
        {
            timeSelanjutnya = timeAwal;
        }
        else
        {
            timeSelanjutnya -= Time.deltaTime * Time.timeScale;
        }

        if (status == "belum")
        {
            apakahbenar = "belum";
        }
        else if (status == "selesai")
        {
            apakahbenar = "selesai";
        }else if (apakahbenar == "")
        {
            apakahbenar = "";
        }

        if (timeSelanjutnya < 0 && apakahbenar == "")
        {
            if (!audioSource.isPlaying)
            {
                Mulai();
            }
        }
    }
}
