using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AcakSoal5 : MonoBehaviour
{
    //Soal & Pengecoh
    public List<Image> lokasihuruf;
    public List<Sprite> hurufhijaiyah;
    public List<Sprite> jawaban;

    public List<int> list1s = new List<int>() { 6, 60, 55, 25, 29, 73, 62, 16, 50, 78, 20, 11, 28, 41, 3, 13, 35, 12, 69, 21, 23, 83, 61, 36, 63, 38, 34, 14, 82, 26, 75, 5, 18, 48, 8, 42, 56, 72, 17, 1, 43, 4, 32, 30, 33, 70, 77, 58, 81, 37, 0, 31, 7, 65, 40, 66, 39, 52, 19, 79, 54, 53, 74, 10, 68, 44, 46, 45, 67, 47, 80, 71, 51, 27, 22, 57, 64, 59, 2, 9, 76, 15, 49, 24 };
    public List<int> list2s = new List<int>() { 40, 55, 76, 54, 71, 31, 1, 37, 77, 36, 38, 11, 30, 5, 52, 60, 33, 26, 72, 32, 14, 70, 35, 59, 42, 22, 74, 4, 78, 17, 8, 27, 13, 0, 28, 81, 39, 49, 50, 56, 51, 44, 21, 18, 19, 41, 34, 3, 66, 83, 62, 43, 24, 16, 47, 79, 46, 2, 75, 68, 29, 69, 65, 63, 45, 82, 67, 53, 58, 15, 6, 57, 80, 48, 64, 73, 10, 23, 20, 7, 12, 61, 9, 25 };
    public List<int> list3s = new List<int>() { 33, 51, 36, 60, 27, 77, 79, 61, 75, 22, 42, 83, 17, 62, 46, 73, 11, 67, 55, 49, 44, 40, 10, 57, 1, 63, 69, 15, 74, 58, 76, 38, 47, 8, 21, 52, 24, 82, 35, 56, 0, 12, 25, 72, 53, 2, 65, 7, 37, 78, 32, 66, 28, 71, 54, 80, 30, 5, 19, 68, 48, 13, 6, 50, 43, 18, 3, 39, 45, 34, 16, 64, 70, 31, 20, 59, 29, 23, 81, 26, 41, 4, 14, 9 };
    public List<AudioClip> audioSoal;
    public AudioClip audioClipInstruksi, audioClipBGM;
    public AudioSource audioInstruksi,audioSource,sourceSoal;
    //Get Unity Reference
    public GameObject gos1, gos2, go3,goKosong, canvas1, canvas2, goSelesai,goKurang,goSalah,goHasil,gambar1,gambar2,gambar3;
    public Text teks1, teks2, teks3,teks11,teks22,teks33;
    public Text teksSoal, teksTotalBenar,teksStatus, teksTotalSalah;
    public Image karakter,teksPendukung;
    public Sprite[] karakters, teksPendukungs;
    public AudioClip[] audioPendukung;

    //Komunikasi Web
    public PostScore ps;
    public GetCommunicationWeb getCommunicationWeb;

    //Public Variable
    public float timeSelanjutnya, timeSelanjutnyaSalah, timeAwal,timeAwalSalah,timerNow,timerStart;

    //Private Variable
    public int lokasisoal,saatini,saatinix,totalbenar,totalsalah,totalsoal, levelload,skor,list1saatini,list2saatini,list3saatini;
    public string apakahbenar;
    public bool isCountDown,isCountDownSalah,isStart;
    void Start()
    {
        gos1 = GameObject.FindGameObjectWithTag("WebManager");
        gos2 = GameObject.FindGameObjectWithTag("RequestManager");
        ps = gos1.GetComponent<PostScore>();
        getCommunicationWeb = gos2.GetComponent<GetCommunicationWeb>();
        StartCoroutine(CekState());
        jawaban = hurufhijaiyah;

    }

    public void Jawab1()
    {
        saatini += 1;
        if (gambar1.GetComponent<Image>().sprite.name == teksSoal.text)
        {
            Benar();
        }
        else
        {
            Salah();
        }
    }
    public void Jawab2()
    {
        saatini += 1;
        if (gambar2.GetComponent<Image>().sprite.name == teksSoal.text)
        {
            Benar();
        }
        else
        {
            Salah();
        }
    }
    public void Jawab3()
    {
        saatini += 1;
        if (gambar3.GetComponent<Image>().sprite.name == teksSoal.text)
        {
            Benar();
        }
        else
        {
            Salah();
        }
    }


    public void Benar()
    {
        isCountDown = true;
        goHasil.SetActive(true);
        skor += 10;
        totalbenar += 1;


        int karaktermuncul = Random.Range(0, 1);
        int audioteks = Random.Range(0, 2);

        karakter.sprite = karakters[karaktermuncul];
        audioSource.clip = audioPendukung[audioteks];
        audioSource.Play();
        teksPendukung.sprite = teksPendukungs[audioteks];
    }

    public void Salah()
    {
        skor = skor;
        int karaktermuncul = Random.Range(2, 3);
        karakter.sprite = karakters[karaktermuncul];
        audioSource.clip = audioPendukung[3];
        audioSource.Play();
        teksPendukung.sprite = teksPendukungs[3];
        isCountDownSalah = true;
        goHasil.SetActive(true);
    }

    public void Acak()
    {
        list1saatini = list1s[saatini];
        list2saatini = list2s[saatini];
        list3saatini = list3s[saatini];
        teksSoal.text = hurufhijaiyah[list1saatini].name;
        lokasisoal = Random.Range(0, 3);

        if (lokasisoal == 0)
        {
            lokasihuruf[0].sprite = hurufhijaiyah[list1saatini];
            lokasihuruf[1].sprite = hurufhijaiyah[list2saatini];
            lokasihuruf[2].sprite = hurufhijaiyah[list3saatini];
        }
        else if (lokasisoal == 1)
        {
            lokasihuruf[1].sprite = hurufhijaiyah[list2saatini];
            lokasihuruf[0].sprite = hurufhijaiyah[list1saatini];
            lokasihuruf[2].sprite = hurufhijaiyah[list3saatini];
        }
        else if (lokasisoal == 2)
        {
            lokasihuruf[2].sprite = hurufhijaiyah[list3saatini];
            lokasihuruf[1].sprite = hurufhijaiyah[list2saatini];
            lokasihuruf[0].sprite = hurufhijaiyah[list1saatini];

        }
    }

    public void CobaLagi(int loadLevel)
    {
        levelload = loadLevel;
        ps.status = "belum";
        apakahbenar = "belum";
        ps.SetHasil();
        if (ps.suksesSimpan)
            isStart = true;
    }

    public void MuatLevel(int loadLevel)
    {
        SceneManager.LoadScene(loadLevel);
    }


    public void Update()
    {
        teksStatus.text = ps.status;
        teksTotalBenar.text = totalbenar.ToString();
        teksTotalSalah.text = totalsalah.ToString();
        //teksTotal.text = "Total Soal : " + saatini.ToString();

        if (totalbenar == 10 || apakahbenar == "selesai" && !audioSource.isPlaying)
        {
            goSelesai.SetActive(true);
            ps.status = "selesai";
            ps.SetHasil();
        }
        else
        {
            goSelesai.SetActive(false);
            if ((totalsalah == 1 || totalsalah == 2 || totalsalah == 3 || totalsalah == 4) && (totalbenar == 13 || totalbenar == 12 || totalbenar == 11 || totalbenar == 10) && saatini == 14 && !audioSource.isPlaying)
            {
                saatini = 1;
                audioSource.volume = 0f;
                goKurang.SetActive(true);
                ps.status = "belum";
                ps.SetHasil();
            }
            else
            {
                goKurang.SetActive(false);
            }
            if (totalsalah == 5 && !audioSource.isPlaying)
            {
                goSalah.SetActive(true);
                ps.status = "belum";
                ps.SetHasil();
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
            totalsalah += 1;
            MulaiSalah();
        }

        if (!isCountDown)
        {
            timeSelanjutnya = timeAwal;
        }
        else
        {
            timeSelanjutnya -= Time.deltaTime * Time.timeScale;
        }

        if (timeSelanjutnya < 0)
        {
            Mulai();
        }
        if (ps.status == "belum")
        {
            apakahbenar = "belum";
        }
        else if (ps.status == "selesai")
        {
            apakahbenar = "selesai";
        }
        else if (apakahbenar == "")
        {
            apakahbenar = "";
        }
    }

    public void MulaiSalah()
    {
        isCountDownSalah = false;
        goHasil.SetActive(false);
    }

    public void Mulai()
    {
        Acak();
        isCountDown = false;
        goHasil.SetActive(false);
    }

    IEnumerator CekState()
    { 
        ps.GetHasil();
        apakahbenar = ps.status;
        yield return new WaitForSeconds(1.25f);
        goKosong.SetActive(false);
        saatinix = Random.Range(1, 6);

        if (saatinix == 1)
        {
            saatini = 0;
        }
        else if (saatinix == 2)
        {
            saatini = 14;
        }
        else if (saatinix == 3)
        {
            saatini = 28;
        }
        else if (saatinix == 4)
        {
            saatini = 42;
        }
        else if (saatinix == 5)
        {
            saatini = 56;
        }
        else if (saatinix == 6)
        {
            saatini = 70;
        }

        StartCoroutine(Instruksi());
    }

    IEnumerator Instruksi()
    {
        if (ps.status == "belum")
        {
            canvas1.SetActive(false);
            canvas2.SetActive(true);
            audioInstruksi.clip = audioClipInstruksi;
            audioInstruksi.Play();
            yield return new WaitForSeconds(audioClipInstruksi.length);
            audioInstruksi.clip = audioClipBGM;
            audioInstruksi.loop = true;
            audioInstruksi.Play();
            canvas1.SetActive(true);
            canvas2.SetActive(false);
        }
        else
        {
            canvas1.SetActive(true);
            canvas2.SetActive(false);
            audioInstruksi.clip = audioClipBGM;
            audioInstruksi.Play();

        }
        Acak();
    }


}
