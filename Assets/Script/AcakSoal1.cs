using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AcakSoal1 : MonoBehaviour
{
    [Header("Button")]
    public Button but1, but2, but3;

    [Header("Other")]
    public string[] soal;
    public string[] soal1;
    public string[] soal2;
    public string[] jawaban;
    public string[] jawaban2;
    public string[] jawaban3;
    public AudioClip[] audioClip,audioClip1,audioClip2, audioPendukung;
    public AudioSource audioSource,audioKatak1,audioKatak2,audioKatak3,audioBGM,audioInstruksi,audioBenar,audioSalah;
    public Sprite[] karakters, teksPendukungs, bintangs;
    public Color[] colorFont;
    public GameObject canvas1, canvas2;
    public AudioClip audioClipBGM, audioClipInstruksi;
    public GameObject[] hijaiyahObjects;

    [Header("Katak")]
    public GameObject kat1, kat2, kat3;
    [Header("Tanda Baca")]
    public GameObject go1, go2, go3;
    [Header("Popup")]
    public GameObject goSelesai, goSalah, goHasil, goKurang, goKosong;
    [Header("Animator Katak")]
    public Animator kata, kata2, kata3;
    [Header("Animator Gelembung")]
    public Animator gel1, gel2, gel3;
    [Header("Kombinasi Klik")]
    public bool bkatak1, bkatak2, bkatak3, bgel1, bgel2, bgel3;


    public Text teks1, teks2, teks3, teksTotal, teksTotalBenar, teksTotalSalah, teksStatus, teksSkor;
    public Image balon1, balon2, balon3, karakter, teksPendukung, imageSoalVocal;

    public int pengecoh1acak, pengecoh2acak;

    public string apakahbenar,klik1,klik2;

    public int soalkeacak, soalsebelumnya, lokasisoal, balonacak1, balonacak2, balonacak3, saatini, totalbenar, totalsalah;
    public string jawabannya,username,progressURL,setProgressURL,status;
    public bool suksesSimpan;

    public int skor, levelload,jawabberapa;

    public int Rand1;
    public int panjang;
    public List<int> list1 = new List<int>() { 0, 1, 11, 4, 12, 3, 6, 8, 5, 2, 7, 13, 9, 10 };
    public List<string> soalnya, pengecohnya1, pengecohnya2 = new List<string>();
    public List<int> soalsaatini, audionya;
    public List<Sprite> latinhijaiyah;
    public List<AudioClip> audioClips;

    public float timeAwal, timeSelanjutnya, timerStart, timerNow,timeSelanjutnyaSalah,timeAwalSalah;
    public bool isCountDown, isStart,benar1,benar2,benar3,isCountDownSalah;

    public PostScore ps;
    //public GameObject gos1, gos2;
    //public GetCommunicationWeb getCommunicationWeb;

    void Start()
    {

        //gos1 = GameObject.FindGameObjectWithTag("WebManager");
        //gos2 = GameObject.FindGameObjectWithTag("RequestManager");

        Time.timeScale = 1;
        //getCommunicationWeb = gos2.GetComponent<GetCommunicationWeb>();
        StartCoroutine(CekState());
        //audioSource.clip = audioClips[saatini];
        //audioSource.Play();
    }


    public void GetHasil()
    {
        StartCoroutine(GetProgress());
    }

    public void SetHasil()
    {
        StartCoroutine(SetProgress());
    }

    public void ToggleHijaiyahButton(bool condition)
    {
      for(int i = 0; i < hijaiyahObjects.Length; i++) {
        hijaiyahObjects[i].GetComponent<Button>().interactable = condition;
      }
    }

    IEnumerator WaitToggleButton()
    {
      yield return new WaitForSeconds(3.2f);
      ToggleHijaiyahButton(true);
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

    IEnumerator CekState()
    {
        //GetHasil();
        apakahbenar = status;
        yield return new WaitForSeconds(1.25f);
        goKosong.SetActive(false);
        saatini = 0;
        audioClips = new List<AudioClip>(new AudioClip[panjang]);
        soalnya = new List<string>(new string[panjang]);
        soalsaatini = new List<int>(new int[panjang]);
        pengecohnya1 = new List<string>(new string[panjang]);
        pengecohnya2 = new List<string>(new string[panjang]);

        for (int j = 0; j < panjang; j++)
        {
            soalsaatini[j] = j;
        }

        for (int k = 0; k < panjang; k++)
        {
            soalkeacak = soalsaatini[k];
            pengecoh1acak = soalsaatini[k];
            pengecoh2acak = soalsaatini[k];
            //audioClips[k] = audioClip[soalkeacak];
            soalnya[k] = soal[soalkeacak];
            pengecohnya1[k] = soal1[soalkeacak];
            pengecohnya2[k] = soal2[soalkeacak];
        }

        balonacak1 = Random.Range(0, 5);
        balonacak2 = Random.Range(0, 5);
        balonacak3 = Random.Range(0, 5);

        lokasisoal = Random.Range(0, 5);
        if (lokasisoal == 0 || lokasisoal == 3)
        {
            go1.name = soalnya[saatini];
            go2.name = pengecohnya1[saatini];
            go3.name = pengecohnya2[saatini];
            kat3.name = soalnya[saatini];
            kat1.name = pengecohnya1[saatini];
            kat2.name = pengecohnya2[saatini];
            audioKatak3.clip = audioClip[saatini];
            audioKatak1.clip = audioClip1[saatini];
            audioKatak2.clip = audioClip2[saatini];

            imageSoalVocal.sprite = latinhijaiyah[saatini];

            teks1.text = soalnya[saatini];
            teks2.text = pengecohnya1[saatini];
            teks3.text = pengecohnya2[saatini];
            teks1.color = colorFont[balonacak1];
            teks2.color = colorFont[balonacak2];
            teks3.color = colorFont[balonacak3];
        }
        else if (lokasisoal == 1 || lokasisoal == 4)
        {
            go1.name = pengecohnya1[saatini];
            go2.name = soalnya[saatini];
            go3.name = pengecohnya2[saatini];
            audioKatak1.clip = audioClip[saatini];
            audioKatak2.clip = audioClip1[saatini];
            audioKatak3.clip = audioClip2[saatini];
            kat1.name = soalnya[saatini];
            kat2.name = pengecohnya1[saatini];
            kat3.name = pengecohnya2[saatini];
            teks1.text = pengecohnya1[saatini];
            teks2.text = soalnya[saatini];
            teks3.text = pengecohnya2[saatini];
            teks1.color = colorFont[balonacak1];
            teks2.color = colorFont[balonacak2];
            teks3.color = colorFont[balonacak3];
            imageSoalVocal.sprite = latinhijaiyah[saatini];

        }
        else if (lokasisoal == 2 || lokasisoal == 5)
        {
            go1.name = pengecohnya2[saatini];
            go2.name = pengecohnya1[saatini];
            go3.name = soalnya[saatini];
            audioKatak2.clip = audioClip[saatini];
            audioKatak3.clip = audioClip1[saatini];
            audioKatak1.clip = audioClip2[saatini];
            kat2.name = soalnya[saatini];
            kat3.name = pengecohnya1[saatini];
            kat1.name = pengecohnya2[saatini];
            teks1.text = pengecohnya2[saatini];
            teks2.text = pengecohnya1[saatini];
            teks3.text = soalnya[saatini];
            teks1.color = colorFont[balonacak1];
            teks2.color = colorFont[balonacak2];
            teks3.color = colorFont[balonacak3];
            imageSoalVocal.sprite = latinhijaiyah[saatini];
        }

        StartCoroutine(Instruksi());
    }

    public void Selanjutnya()
    {
        isCountDown = true;
    }

    public void CobaLagi(int loadLevel)
    {
        levelload = loadLevel;
        ps.status = "belum";
        apakahbenar = "belum";
        //SetHasil();
    }

    IEnumerator WaitAudio(bool condition)
    {
      yield return new WaitForSeconds(.7f);
      if(condition == true) {
        audioBenar.Play();
      } else {
        audioSalah.Play();
      }
    }

    public void MuatLevel(int loadLevel)
    {
        SceneManager.LoadScene(loadLevel);
    }

    public void Hijay1()
    {
        bgel1 = true;
        bgel2 = false;
        bgel3 = false;

        klik1 = go1.name;
        if(klik2 != "")
        {
            if (klik1 == klik2)
            {
                benar1 = true;
                jawabberapa += 1;
                klik1 = "";
                klik2 = "";
                StartCoroutine(WaitAudio(true));
            }
            else
            {
                /*                benar1 = false;
                                jawabberapa += 1;
                                klik1 = "";
                                klik2 = "";
                */
                skor = skor;
                int karaktermuncul = Random.Range(2, 4);
                karakter.sprite = karakters[karaktermuncul];
                if (karaktermuncul == 2) {
                  audioSource.clip = audioPendukung[6];
                } else {
                  audioSource.clip = audioPendukung[7];
                }
                audioSource.Play();
                teksPendukung.sprite = teksPendukungs[6];
                totalsalah += 1;
                jawabberapa = 0;
                benar1 = false;
                benar2 = false;
                benar3 = false;
                isCountDownSalah = true;
                goHasil.SetActive(true);
                klik1 = "";
                klik2 = "";
                audioSalah.Play();                                 
            }

            if (benar1 && benar2 && benar3)
            {
                skor += 1;
                int karaktermuncul = Random.Range(0, 2);
                int audioteks;
                if(karaktermuncul == 0) {
                  audioteks = Random.Range(0, 3);
                } else {
                  audioteks = Random.Range(3, 6);
                }
                jawabberapa = 0;
                karakter.sprite = karakters[karaktermuncul];
                audioSource.clip = audioPendukung[audioteks];
                audioSource.Play();
                teksPendukung.sprite = teksPendukungs[audioteks];
                totalbenar += 1;
                benar1 = false;
                benar2 = false;
                benar3 = false;
                isCountDown = true;
                goHasil.SetActive(true);
            }
        }
        /*if (jawabberapa >= 3 && ((!benar1 && !benar2 && !benar3) || (benar1 && benar2 && !benar3) || (benar1 && !benar2 && benar3) || (benar1 && benar2 && !benar3) || (benar1 && !benar2 && !benar3) || (!benar1 && benar2 && benar3) || (!benar1 && benar2 && !benar3) || (benar1 && benar2 && !benar3) ||(!benar1 && !benar2 && benar3)))
        {
            skor = skor;
            int karaktermuncul = Random.Range(2, 3);
            karakter.sprite = karakters[karaktermuncul];
            audioSource.clip = audioPendukung[3];
            audioSource.Play();
            teksPendukung.sprite = teksPendukungs[3];
            totalsalah += 1;
            jawabberapa = 0;
            benar1 = false;
            benar2 = false;
            benar3 = false;
            isCountDown = true;
            goHasil.SetActive(true);


        }*/

    }

    public void Hijay2()
    {
        bgel1 = false;
        bgel2 = true;
        bgel3 = false;

        klik1 = go2.name;
        if (klik2 != "")
        {
            if (klik1 == klik2)
            {
                benar2 = true;
                jawabberapa += 1;
                klik1 = "";
                klik2 = "";
                StartCoroutine(WaitAudio(true));
            }
            else
            {
                /* benar2 = false;
                 jawabberapa += 1;
                 klik1 = "";
                 klik2 = "";*/
                skor = skor;
                int karaktermuncul = Random.Range(2, 4);
                karakter.sprite = karakters[karaktermuncul];
                if (karaktermuncul == 2) {
                  audioSource.clip = audioPendukung[6];
                } else {
                  audioSource.clip = audioPendukung[7];
                }

                audioSource.Play();
                teksPendukung.sprite = teksPendukungs[6];
                totalsalah += 1;
                jawabberapa = 0;
                benar1 = false;
                benar2 = false;
                benar3 = false;
                isCountDownSalah = true;
                goHasil.SetActive(true);
                klik1 = "";
                klik2 = "";
                audioSalah.Play();
            }

            if (benar1 && benar2 && benar3)
            {
                skor += 1;
                int karaktermuncul = Random.Range(0, 2);
                int audioteks;
                if(karaktermuncul == 0) {
                  audioteks = Random.Range(0, 3);
                } else {
                  audioteks = Random.Range(3, 6);
                }
                jawabberapa = 0;

                karakter.sprite = karakters[karaktermuncul];
                audioSource.clip = audioPendukung[audioteks];
                audioSource.Play();
                teksPendukung.sprite = teksPendukungs[audioteks];
                totalbenar += 1;
                isCountDown = true;
                goHasil.SetActive(true);
            }
        }
        /*if (jawabberapa >= 3 && ((!benar1 && !benar2 && !benar3) || (benar1 && benar2 && !benar3) || (benar1 && !benar2 && benar3) || (benar1 && benar2 && !benar3) || (benar1 && !benar2 && !benar3) || (!benar1 && benar2 && benar3) || (!benar1 && benar2 && !benar3) || (benar1 && benar2 && !benar3) || (!benar1 && !benar2 && benar3)))
        {
            skor = skor;
            int karaktermuncul = Random.Range(2, 3);
            karakter.sprite = karakters[karaktermuncul];
            audioSource.clip = audioPendukung[3];
            audioSource.Play();
            teksPendukung.sprite = teksPendukungs[3];
            totalsalah += 1;
            jawabberapa = 0;
            benar1 = false;
            benar2 = false;
            benar3 = false;
            isCountDown = true;
            goHasil.SetActive(true);


        }*/


    }

    public void Hijay3()
    {
        bgel1 = false;
        bgel2 = false;
        bgel3 = true;

        klik1 = go3.name;
        if (klik2 != "")
        {
            if (klik1 == klik2)
            {
                benar3 = true;
                jawabberapa += 1;
                klik1 = "";
                klik2 = "";
                StartCoroutine(WaitAudio(true));
            }
            else
            {
                /* benar3 = false;
                 jawabberapa += 1;
                 klik1 = "";
                 klik2 = "";*/
                skor = skor;
                int karaktermuncul = Random.Range(2, 4);
                if (karaktermuncul == 2) {
                  audioSource.clip = audioPendukung[6];
                } else {
                  audioSource.clip = audioPendukung[7];
                }
                karakter.sprite = karakters[karaktermuncul];
                audioSource.Play();
                teksPendukung.sprite = teksPendukungs[6];
                totalsalah += 1;
                jawabberapa = 0;
                benar1 = false;
                benar2 = false;
                benar3 = false;
                isCountDownSalah = true;
                goHasil.SetActive(true);
                klik1 = "";
                klik2 = "";
                audioSalah.Play();

            }

            if (benar1 && benar2 && benar3)
            {
                skor += 1;
                int karaktermuncul = Random.Range(0, 2);
                int audioteks;
                if(karaktermuncul == 0) {
                  audioteks = Random.Range(0, 3);
                } else {
                  audioteks = Random.Range(3, 6);
                }

                jawabberapa = 0;

                karakter.sprite = karakters[karaktermuncul];
                audioSource.clip = audioPendukung[audioteks];
                audioSource.Play();
                teksPendukung.sprite = teksPendukungs[audioteks];
                totalbenar += 1;
                isCountDown = true;
                goHasil.SetActive(true);
            }
            /*if (jawabberapa >= 3 && ((!benar1 && !benar2 && !benar3) || (benar1 && benar2 && !benar3) || (benar1 && !benar2 && benar3) || (benar1 && benar2 && !benar3) || (benar1 && !benar2 && !benar3) || (!benar1 && benar2 && benar3) || (!benar1 && benar2 && !benar3) || (benar1 && benar2 && !benar3) || (!benar1 && !benar2 && benar3)))
            {
                skor = skor;
                int karaktermuncul = Random.Range(2, 3);
                karakter.sprite = karakters[karaktermuncul];
                audioSource.clip = audioPendukung[3];
                audioSource.Play();
                teksPendukung.sprite = teksPendukungs[3];
                totalsalah += 1;
                jawabberapa = 0;
                benar1 = false;
                benar2 = false;
                benar3 = false;
                isCountDown = true;
                goHasil.SetActive(true);
            }*/

        }
    }

    public void Katak1()
    {
        StopCoroutine("WaitToggleButton");
        ToggleHijaiyahButton(false);
        bkatak1 = true;
        bkatak2 = false;
        bkatak3 = false;

        StartCoroutine("WaitToggleButton");
        klik2 = kat1.name;
        if(klik1 != "")
        {
            if (klik1 == klik2)
            {
                benar1 = true;
                klik1 = "";
                klik2 = "";

            }
            else
            {
                benar1 = false;
                klik1 = "";
                klik2 = "";

            }
        }
    }

    public void Katak2()
    {

        StopCoroutine("WaitToggleButton");
        ToggleHijaiyahButton(false);
        bkatak1 = false;
        bkatak2 = true;
        bkatak3 = false;

        StartCoroutine("WaitToggleButton");
        klik2 = kat2.name;
        if (klik1 != "")
        {
            if (klik1 == klik2)
            {

                benar2 = true;
                klik1 = "";
                klik2 = "";
            }
            else
            {
                benar2 = false;
                klik1 = "";
                klik2 = "";
            }
        }
    }

    public void Katak3()
    {
        StopCoroutine("WaitToggleButton");
        ToggleHijaiyahButton(false);
        bkatak1 = false;
        bkatak2 = false;
        bkatak3 = true;

        StartCoroutine("WaitToggleButton");
        klik2 = kat3.name;
        if (klik1 != "")
        {
            if (klik1 == klik2)
            {
                benar3 = true;
                klik1 = "";
                klik2 = "";
            }
            else
            {
                benar3 = false;
                klik1 = "";
                klik2 = "";
            }
        }
    }

    public void MulaiSalah()
    {
        kata.SetTrigger("Balik");
        gel1.SetTrigger("Balik");
        kata2.SetTrigger("Balik");
        gel2.SetTrigger("Balik");
        kata3.SetTrigger("Balik");
        gel3.SetTrigger("Balik");
        bkatak1 = false;
        bkatak2 = false;
        bkatak3 = false;
        bgel1 = false;
        bgel2 = false;
        bgel3 = false;
        jawabberapa = 0;
        benar1 = false;
        benar2 = false;
        benar3 = false;
        isCountDownSalah = false;
        goHasil.SetActive(false);
    }

    public void Mulai()
    {
            kata.SetTrigger("Balik");
            gel1.SetTrigger("Balik");
            kata2.SetTrigger("Balik");
            gel2.SetTrigger("Balik");
            kata3.SetTrigger("Balik");
            gel3.SetTrigger("Balik");
            jawabberapa = 0;
            benar1 = false;
            benar2 = false;
            benar3 = false;
            bkatak1 = false;
            bkatak2 = false;
            bkatak3 = false;
            bgel1 = false;
            bgel2 = false;
            bgel3 = false;
            Acak();
            isCountDown = false;
            goHasil.SetActive(false) ;
            

     
    }

    public void Acak()
    {
        saatini += 1;
        balonacak1 = Random.Range(0, 5);
        balonacak2 = Random.Range(0, 5);
        balonacak3 = Random.Range(0, 5);

        
        if (saatini < 14)
        {
            lokasisoal = Random.Range(0, 5);
            if (lokasisoal == 0 || lokasisoal == 3)
            {
                go1.name = soalnya[saatini];
                go2.name = pengecohnya1[saatini];
                go3.name = pengecohnya2[saatini];
                kat3.name = soalnya[saatini];
                kat1.name = pengecohnya1[saatini];
                kat2.name = pengecohnya2[saatini];
                audioKatak3.clip = audioClip[saatini];
                audioKatak1.clip = audioClip1[saatini];
                audioKatak2.clip = audioClip2[saatini];

                imageSoalVocal.sprite = latinhijaiyah[saatini];

                teks1.text = soalnya[saatini];
                teks2.text = pengecohnya1[saatini];
                teks3.text = pengecohnya2[saatini];
                teks1.color = colorFont[balonacak1];
                teks2.color = colorFont[balonacak2];
                teks3.color = colorFont[balonacak3];
            }
            else if (lokasisoal == 1 || lokasisoal == 4)
            {
                go1.name = pengecohnya1[saatini];
                go2.name = soalnya[saatini];
                go3.name = pengecohnya2[saatini];
                audioKatak1.clip = audioClip[saatini];
                audioKatak2.clip = audioClip1[saatini];
                audioKatak3.clip = audioClip2[saatini];
                kat1.name = soalnya[saatini];
                kat2.name = pengecohnya1[saatini];
                kat3.name = pengecohnya2[saatini];
                teks1.text = pengecohnya1[saatini];
                teks2.text = soalnya[saatini];
                teks3.text = pengecohnya2[saatini];
                teks1.color = colorFont[balonacak1];
                teks2.color = colorFont[balonacak2];
                teks3.color = colorFont[balonacak3];
                imageSoalVocal.sprite = latinhijaiyah[saatini];

            }
            else if (lokasisoal == 2 || lokasisoal == 5)
            {
                go1.name = pengecohnya2[saatini];
                go2.name = pengecohnya1[saatini];
                go3.name = soalnya[saatini];
                audioKatak2.clip = audioClip[saatini];
                audioKatak3.clip = audioClip1[saatini];
                audioKatak1.clip = audioClip2[saatini];
                kat2.name = soalnya[saatini];
                kat3.name = pengecohnya1[saatini];
                kat1.name = pengecohnya2[saatini];
                teks1.text = pengecohnya2[saatini];
                teks2.text = pengecohnya1[saatini];
                teks3.text = soalnya[saatini];
                teks1.color = colorFont[balonacak1];
                teks2.color = colorFont[balonacak2];
                teks3.color = colorFont[balonacak3];
                imageSoalVocal.sprite = latinhijaiyah[saatini];
            }
        }
    }

    IEnumerator Instruksi()
    {
            yield return new WaitForSeconds(0);
            audioInstruksi.clip = audioClipBGM;
            audioInstruksi.Play();
            audioInstruksi.loop = true;
            canvas1.SetActive(true);
    }


    public void Resetkeun()
    {
        ResetTriggerNya.ResetAllTrigger(kata);
        ResetTriggerNya.ResetAllTrigger(kata2);
        ResetTriggerNya.ResetAllTrigger(kata3);
        ResetTriggerNya.ResetAllTrigger(gel1);
        ResetTriggerNya.ResetAllTrigger(gel2);
        ResetTriggerNya.ResetAllTrigger(gel3);
        but1.interactable = false;
        but2.interactable = false;
        but3.interactable = false;
    }

    IEnumerator WaitToggleMenu() 
    {
      yield return new WaitForSeconds(1.5f);
      audioInstruksi.Stop();
      Time.timeScale = 0;
      goSelesai.SetActive(true);
    }

    public void Pause(bool condition)
    {
      if(condition == true) {
        audioInstruksi.Pause();
        Time.timeScale = 0;
      } else {
        audioInstruksi.UnPause();
        Time.timeScale = 1;
      }
    }

    // Update is called once per frame
    void Update()
    {
        teksSkor.text = skor+"/14";
        //teksTotalBenar.text = totalbenar.ToString();
        teksTotalSalah.text = totalsalah.ToString();
        //teksTotal.text = "Total Soal : " + saatini.ToString();

        if (totalbenar == 14 || apakahbenar == "selesai" && !audioSource.isPlaying)
        {
            StartCoroutine("WaitToggleMenu");
            Debug.Log("Selesai");
            status = "selesai";
            audioInstruksi.Stop();
            //SetHasil();
        }
        else
        {
            goSelesai.SetActive(false);
            // if ((totalsalah == 1 || totalsalah == 2 || totalsalah == 3 || totalsalah == 4) && (totalbenar == 13 || totalbenar == 12 || totalbenar == 11 || totalbenar == 10) && saatini == 14 && !audioSource.isPlaying)
            // {
            //     saatini = 1;
            //     audioSource.volume = 0f;
            //     goKurang.SetActive(true);
            //     status = "belum";
            //     SetHasil();
            // }
            // else
            // {
            //     goKurang.SetActive(false);
            // }
            if (totalsalah == 5 && !audioSource.isPlaying)
            {
                goSalah.SetActive(true);
                status = "belum";
                audioInstruksi.Stop();
                //SetHasil();
            }
            else
            {
                goSalah.SetActive(false);
            }

        }
        if (suksesSimpan)
        {
            suksesSimpan = false;
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
        

        if (bkatak1 && bgel1)
        {
            kata.SetTrigger("Loncat");
            gel1.SetTrigger("Meletup");
            bkatak1 = false;
            bgel1 = false;
            but1.interactable = false;
            but2.interactable = false;
            but3.interactable = false;
        }
        else if (bkatak1 && bgel2)
        {
            kata.SetTrigger("LoncatTengah");
            gel2.SetTrigger("Meletup");
            bkatak1 = false;
            bgel2 = false;
            but1.interactable = false;
            but2.interactable = false;
            but3.interactable = false;
        }
        else if (bkatak1 && bgel3)
        {
            kata.SetTrigger("LoncatKanan");
            gel3.SetTrigger("Meletup");
            bkatak1 = false;
            bgel3 = false;
            but1.interactable = false;
            but2.interactable = false;
            but3.interactable = false;
        }
        else if (bkatak2 && bgel1)
        {
            kata2.SetTrigger("LoncatKiri");
            gel1.SetTrigger("Meletup");
            bkatak2 = false;
            bgel1 = false;
            but1.interactable = false;
            but2.interactable = false;
            but3.interactable = false;


        }
        else if (bkatak2 && bgel2)
        {
            kata2.SetTrigger("Loncat");
            gel2.SetTrigger("Meletup");
            bkatak2 = false;
            bgel2 = false;
            but1.interactable = false;
            but2.interactable = false;
            but3.interactable = false;

        }
        else if (bkatak2 && bgel3)
        {
            kata2.SetTrigger("LoncatKanan");
            gel3.SetTrigger("Meletup");
            bkatak2 = false;
            bgel3 = false;
            but1.interactable = false;
            but2.interactable = false;
            but3.interactable = false;

        }
        else if (bkatak3 && bgel1)
        {
            kata3.SetTrigger("LoncatKiri");
            gel1.SetTrigger("Meletup");
            bkatak3 = false;
            bgel1 = false;
            but1.interactable = false;
            but2.interactable = false;
            but3.interactable = false;

        }
        else if(bkatak3 && bgel2)
        {
            kata3.SetTrigger("LoncatTengah");
            gel2.SetTrigger("Meletup");
            bkatak3 = false;
            bgel2 = false;
            but1.interactable = false;
            but2.interactable = false;
            but3.interactable = false;

        }
        else if(bkatak3 && bgel3)
        {
            kata3.SetTrigger("Loncat");
            gel3.SetTrigger("Meletup");
            bkatak3 = false;
            bgel3 = false;
            but1.interactable = false;
            but2.interactable = false;
            but3.interactable = false;

        }


    }
}
