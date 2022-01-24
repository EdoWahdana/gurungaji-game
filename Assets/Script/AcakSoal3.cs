using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AcakSoal3 : MonoBehaviour
{}
    /*
    public List<string> hijaiyah, tekslatl;
    public List<string> jawaban, pengecoh1, pengecoh2, soal1,soal2,soal3;
    public List<AudioClip> audioSoal;
    public AudioClip audioClipInstruksi, audioClipBGM;
    public AudioSource audioInstruksi,audioSource,sourceSoal;
	public GameObject tandaAtas, tandaTengah, tandaBawah;
	
    //Get Unity Reference
    public GameObject gos1, gos2, go3,goKosong, canvas1, canvas2, goSelesai,goKurang,goSalah,goHasil;
    public Text teks1, teks2, teks3;
    public Text teksSoal, teksSkor,teksStatus, teksTotalSalah;
    public Image karakter,teksPendukung;
    public Sprite[] karakters, teksPendukungs;
    public AudioClip[] audioPendukung;

    //Komunikasi Web
    public PostScore ps;

    //Referensi Drag Drop Dll
    public GetAllAnswer3 gta3;
    public Drag[] dras;
    public GameObject[] goDrag;

    //Public Variable
    public float timeSelanjutnya, timeSelanjutnyaSalah, timeAwal,timeAwalSalah,timerNow,timerStart;

    //Private Variable
    public int lokasisoal,saatini,saatinix,totalbenar,totalsalah,totalsoal, levelload,skor;
    public string apakahbenar;
    public bool isCountDown,isCountDownSalah,isStart;
    void Start()
    {
        gos1 = GameObject.FindGameObjectWithTag("WebManager");
        gos2 = GameObject.FindGameObjectWithTag("RequestManager");
        ps = gos1.GetComponent<PostScore>();
        StartCoroutine(CekState());

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
        teksSkor.text = skor + "/14";
        teksStatus.text = ps.status;
        //teksTotalBenar.text = totalbenar.ToString();
        teksTotalSalah.text = totalsalah.ToString();
        //teksTotal.text = "Total Soal : " + saatini.ToString();

        if (totalbenar == 14 || apakahbenar == "selesai" && !audioSource.isPlaying)
        {
			audioSource.Stop();
            goSelesai.SetActive(true);
            ps.status = "selesai";
            // ps.SetHasil();
        }
        else
        {
            goSelesai.SetActive(false);
            /*if ((totalsalah == 1 || totalsalah == 2 || totalsalah == 3 || totalsalah == 4) && (totalbenar == 13 || totalbenar == 12 || totalbenar == 11 || totalbenar == 10) && saatini == 14 && !audioSource.isPlaying)
            {
                saatini = 1;
                audioSource.volume = 0f;
                goKurang.SetActive(true);
                ps.status = "belum";
                // ps.SetHasil();
            }
            else
            {
                goKurang.SetActive(false);
            }
            if (totalsalah == 5 && !audioSource.isPlaying)
            {
                goSalah.SetActive(true);
                ps.status = "belum";
                // ps.SetHasil();
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
            skor += 1;
            totalbenar += 1;

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
        // ps.GetHasil();
        apakahbenar = ps.status;
        yield return new WaitForSeconds(1.25f);
        goKosong.SetActive(false);
        saatini = 0;
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

    public void Benar()
    {

        int karaktermuncul = Random.Range(0, 1);
        int audioteks = Random.Range(0, 2);

        karakter.sprite = karakters[karaktermuncul];
        audioSource.clip = audioPendukung[audioteks];
        audioSource.Play();
        teksPendukung.sprite = teksPendukungs[audioteks];
        isCountDown = true;
        goHasil.SetActive(true);
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
        saatinix = Random.Range(0, 13);
        lokasisoal = Random.Range(0, 5);
        Debug.Log("Acak soal saatinix : " + saatinix);
        switch (saatinix)
        {
            case 0:
                saatini = Random.Range(0, 3);
                if (saatini == 0)
                {
                    gta3.jawabansesungguhnya = hijaiyah[0];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[0]);
					gta3.tandaBaca = FindVowel(tekslatl[0]).ToString();

                    teksSoal.text = tekslatl[0];
                    sourceSoal.clip = audioSoal[0];
                    sourceSoal.Play();
                }
                else if (saatini == 1)
                {
                    gta3.jawabansesungguhnya = hijaiyah[1];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[1]);
					gta3.tandaBaca = FindVowel(tekslatl[1]).ToString();
					
                    teksSoal.text = tekslatl[1];
                    sourceSoal.clip = audioSoal[1];
                    sourceSoal.Play();
                }
                else
                {
                    gta3.jawabansesungguhnya = hijaiyah[2];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[2]);
					gta3.tandaBaca = FindVowel(tekslatl[2]).ToString();

                    teksSoal.text = tekslatl[2];
                    sourceSoal.clip = audioSoal[2];
                    sourceSoal.Play();
                }
                break;
            case 1:
                saatini = Random.Range(0, 3);
                if (saatini == 0)
                {
                    gta3.jawabansesungguhnya = hijaiyah[3];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[3]);
					gta3.tandaBaca = FindVowel(tekslatl[3]).ToString();

                    teksSoal.text = tekslatl[3];
                    sourceSoal.clip = audioSoal[3];
                    sourceSoal.Play();

                }
                else if (saatini == 1)
                {
                    gta3.jawabansesungguhnya = hijaiyah[4];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[4]);
					gta3.tandaBaca = FindVowel(tekslatl[4]).ToString();

                    teksSoal.text = tekslatl[4];
                    sourceSoal.clip = audioSoal[4];
                    sourceSoal.Play();

                }
                else
                {
                    gta3.jawabansesungguhnya = hijaiyah[5];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[5]);
					gta3.tandaBaca = FindVowel(tekslatl[5]).ToString();

                    teksSoal.text = tekslatl[5];
                    sourceSoal.clip = audioSoal[5];
                    sourceSoal.Play();

                }
                break;
            case 2:
                saatini = Random.Range(0, 3);
                if (saatini == 0)
                {
                    gta3.jawabansesungguhnya = hijaiyah[6];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[6]);
					gta3.tandaBaca = FindVowel(tekslatl[6]).ToString();

                    teksSoal.text = tekslatl[6];
                    sourceSoal.clip = audioSoal[6];
                    sourceSoal.Play();

                }
                else if (saatini == 1)
                {
                    gta3.jawabansesungguhnya = hijaiyah[7];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[7]);
					gta3.tandaBaca = FindVowel(tekslatl[7]).ToString();

                    teksSoal.text = tekslatl[7];
                    sourceSoal.clip = audioSoal[7];
                    sourceSoal.Play();

                }
                else
                {
                    gta3.jawabansesungguhnya = hijaiyah[8];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[8]);
					gta3.tandaBaca = FindVowel(tekslatl[8]).ToString();

                    teksSoal.text = tekslatl[8];
                    sourceSoal.clip = audioSoal[8];
                    sourceSoal.Play();

                }
                break;
            case 3:
                saatini = Random.Range(0, 3);
                if (saatini == 0)
                {
                    gta3.jawabansesungguhnya = hijaiyah[9];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[9]);
					gta3.tandaBaca = FindVowel(tekslatl[9]).ToString();

                    teksSoal.text = tekslatl[9];
                    sourceSoal.clip = audioSoal[9];
                    sourceSoal.Play();

                }
                else if (saatini == 1)
                {
                    gta3.jawabansesungguhnya = hijaiyah[10];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[10]);
					gta3.tandaBaca = FindVowel(tekslatl[10]).ToString();

                    teksSoal.text = tekslatl[10];
                    sourceSoal.clip = audioSoal[10];
                    sourceSoal.Play();

                }
                else
                {
                    gta3.jawabansesungguhnya = hijaiyah[11];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[11]);
					gta3.tandaBaca = FindVowel(tekslatl[11]).ToString();

                    teksSoal.text = tekslatl[11];
                    sourceSoal.clip = audioSoal[11];
                    sourceSoal.Play();

                }
                break;
            case 4:
                saatini = Random.Range(0, 3);
                if (saatini == 0)
                {
                    gta3.jawabansesungguhnya = hijaiyah[12];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[12]);
					gta3.tandaBaca = FindVowel(tekslatl[12]).ToString();

                    teksSoal.text = tekslatl[12];
                    sourceSoal.clip = audioSoal[12];
                    sourceSoal.Play();

                }
                else if (saatini == 1)
                {
                    gta3.jawabansesungguhnya = hijaiyah[13];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[13]);
					gta3.tandaBaca = FindVowel(tekslatl[13]).ToString();

                    teksSoal.text = tekslatl[13];
                    sourceSoal.clip = audioSoal[13];
                    sourceSoal.Play();

                }
                else
                {
                    gta3.jawabansesungguhnya = hijaiyah[14];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[14]);
					gta3.tandaBaca = FindVowel(tekslatl[14]).ToString();

                    teksSoal.text = tekslatl[14];
                    sourceSoal.clip = audioSoal[14];
                    sourceSoal.Play();

                }
                break;
            case 5:
                saatini = Random.Range(0, 3);
                if (saatini == 0)
                {
                    gta3.jawabansesungguhnya = hijaiyah[15];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[15]);
					gta3.tandaBaca = FindVowel(tekslatl[15]).ToString();

                    teksSoal.text = tekslatl[15];
                    sourceSoal.clip = audioSoal[15];
                    sourceSoal.Play();

                }
                else if (saatini == 1)
                {
                    gta3.jawabansesungguhnya = hijaiyah[16];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[16]);
					gta3.tandaBaca = FindVowel(tekslatl[16]).ToString();

                    teksSoal.text = tekslatl[16];
                    sourceSoal.clip = audioSoal[16];
                    sourceSoal.Play();

                }
                else
                {
                    gta3.jawabansesungguhnya = hijaiyah[17];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[17]);
					gta3.tandaBaca = FindVowel(tekslatl[17]).ToString();

                    teksSoal.text = tekslatl[17];
                    sourceSoal.clip = audioSoal[17];
                    sourceSoal.Play();

                }
                break;
            case 6:
                saatini = Random.Range(0, 3);
                if (saatini == 0)
                {
                    gta3.jawabansesungguhnya = hijaiyah[18];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[18]);
					gta3.tandaBaca = FindVowel(tekslatl[18]).ToString();

                    teksSoal.text = tekslatl[18];
                    sourceSoal.clip = audioSoal[18];
                    sourceSoal.Play();


                }
                else if (saatini == 1)
                {
                    gta3.jawabansesungguhnya = hijaiyah[19];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[19]);
					gta3.tandaBaca = FindVowel(tekslatl[19]).ToString();

                    teksSoal.text = tekslatl[19];
                    sourceSoal.clip = audioSoal[19];
                    sourceSoal.Play();

                }
                else
                {
                    gta3.jawabansesungguhnya = hijaiyah[20];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[20]);
					gta3.tandaBaca = FindVowel(tekslatl[20]).ToString();

                    teksSoal.text = tekslatl[20];
                    sourceSoal.clip = audioSoal[20];
                    sourceSoal.Play();

                }
                break;
            case 7:
                saatini = Random.Range(0, 3);
                if (saatini == 0)
                {
                    gta3.jawabansesungguhnya = hijaiyah[21];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[21]);
					gta3.tandaBaca = FindVowel(tekslatl[21]).ToString();

                    teksSoal.text = tekslatl[21];
                    sourceSoal.clip = audioSoal[21];
                    sourceSoal.Play();

                }
                else if (saatini == 1)
                {
                    gta3.jawabansesungguhnya = hijaiyah[22];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[22]);
					gta3.tandaBaca = FindVowel(tekslatl[22]).ToString();

                    teksSoal.text = tekslatl[22];
                    sourceSoal.clip = audioSoal[22];
                    sourceSoal.Play();

                }
                else
                {
                    gta3.jawabansesungguhnya = hijaiyah[23];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[23]);
					gta3.tandaBaca = FindVowel(tekslatl[23]).ToString();

                    teksSoal.text = tekslatl[23];
                    sourceSoal.clip = audioSoal[23];
                    sourceSoal.Play();

                }
                break;
            case 8:
                saatini = Random.Range(0, 3);
                if (saatini == 0)
                {
                    gta3.jawabansesungguhnya = hijaiyah[24];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[24]);
					gta3.tandaBaca = FindVowel(tekslatl[24]).ToString();

                    teksSoal.text = tekslatl[24];
                    sourceSoal.clip = audioSoal[24];
                    sourceSoal.Play();

                }
                else if (saatini == 1)
                {
                    gta3.jawabansesungguhnya = hijaiyah[25];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[25]);
					gta3.tandaBaca = FindVowel(tekslatl[25]).ToString();

                    teksSoal.text = tekslatl[25];
                    sourceSoal.clip = audioSoal[25];
                    sourceSoal.Play();

                }
                else
                {
                    gta3.jawabansesungguhnya = hijaiyah[26];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[26]);
					gta3.tandaBaca = FindVowel(tekslatl[26]).ToString();

                    teksSoal.text = tekslatl[26];
                    sourceSoal.clip = audioSoal[26];
                    sourceSoal.Play();

                }
                break;
            case 9:
                saatini = Random.Range(0, 3);
                if (saatini == 0)
                {
                    gta3.jawabansesungguhnya = hijaiyah[27];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[27]);
					gta3.tandaBaca = FindVowel(tekslatl[27]).ToString();

                    teksSoal.text = tekslatl[27];
                    sourceSoal.clip = audioSoal[27];
                    sourceSoal.Play();

                }
                else if (saatini == 1)
                {
                    gta3.jawabansesungguhnya = hijaiyah[28];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[28]);
					gta3.tandaBaca = FindVowel(tekslatl[28]).ToString();

                    teksSoal.text = tekslatl[28];
                    sourceSoal.clip = audioSoal[28];
                    sourceSoal.Play();

                }
                else
                {
                    gta3.jawabansesungguhnya = hijaiyah[29];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[29]);
					gta3.tandaBaca = FindVowel(tekslatl[29]).ToString();

                    teksSoal.text = tekslatl[29];
                    sourceSoal.clip = audioSoal[29];
                    sourceSoal.Play();

                }
                break;
            case 10:
                saatini = Random.Range(0, 3);
                if (saatini == 0)
                {
                    gta3.jawabansesungguhnya = hijaiyah[30];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[30]);
					gta3.tandaBaca = FindVowel(tekslatl[30]).ToString();

                    teksSoal.text = tekslatl[30];
                    sourceSoal.clip = audioSoal[30];
                    sourceSoal.Play();

                }
                else if (saatini == 1)
                {
                    gta3.jawabansesungguhnya = hijaiyah[31];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[31]);
					gta3.tandaBaca = FindVowel(tekslatl[31]).ToString();

                    teksSoal.text = tekslatl[31];
                    sourceSoal.clip = audioSoal[31];
                    sourceSoal.Play();

                }
                else
                {
                    gta3.jawabansesungguhnya = hijaiyah[32];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[32]);
					gta3.tandaBaca = FindVowel(tekslatl[32]).ToString();

                    teksSoal.text = tekslatl[32];
                    sourceSoal.clip = audioSoal[32];
                    sourceSoal.Play();

                }
                break;
            case 11:
                saatini = Random.Range(0, 3);
                if (saatini == 0)
                {
                    gta3.jawabansesungguhnya = hijaiyah[33];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[33]);
					gta3.tandaBaca = FindVowel(tekslatl[33]).ToString();

                    teksSoal.text = tekslatl[33];
                    sourceSoal.clip = audioSoal[33];
                    sourceSoal.Play();

                }
                else if (saatini == 1)
                {
                    gta3.jawabansesungguhnya = hijaiyah[34];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[34]);
					gta3.tandaBaca = FindVowel(tekslatl[34]).ToString();

                    teksSoal.text = tekslatl[34];
                    sourceSoal.clip = audioSoal[34];
                    sourceSoal.Play();

                }
                else
                {
                    gta3.jawabandibalik = ReverseXor(hijaiyah[35]);
                    gta3.jawabansesungguhnya = hijaiyah[35];
					gta3.tandaBaca = FindVowel(tekslatl[35]).ToString();
					
                    teksSoal.text = tekslatl[35];
                    sourceSoal.clip = audioSoal[35];
                    sourceSoal.Play();

                }
                break;
            case 12:
                saatini = Random.Range(0, 3);
                if (saatini == 0)
                {
                    gta3.jawabansesungguhnya = hijaiyah[36];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[36]);
					gta3.tandaBaca = FindVowel(tekslatl[36]).ToString();

                    teksSoal.text = tekslatl[36];
                    sourceSoal.clip = audioSoal[36];
                    sourceSoal.Play();

                }
                else if (saatini == 1)
                {
                    gta3.jawabansesungguhnya = hijaiyah[37];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[37]);
					gta3.tandaBaca = FindVowel(tekslatl[37]).ToString();

                    teksSoal.text = tekslatl[37];
                    sourceSoal.clip = audioSoal[37];
                    sourceSoal.Play();

                }
                else
                {
                    gta3.jawabansesungguhnya = hijaiyah[38];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[38]);
					gta3.tandaBaca = FindVowel(tekslatl[38]).ToString();
					
                    teksSoal.text = tekslatl[38];
                    sourceSoal.clip = audioSoal[38];
                    sourceSoal.Play();

                }
                break;
            case 13:
                saatini = Random.Range(0, 3);
                if (saatini == 0)
                {
                    gta3.jawabansesungguhnya = hijaiyah[39];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[39]);
					gta3.tandaBaca = FindVowel(tekslatl[39]).ToString();

                    teksSoal.text = tekslatl[39];
                    sourceSoal.clip = audioSoal[39];
                    sourceSoal.Play();

                }
                else if (saatini == 1)
                {
                    gta3.jawabansesungguhnya = hijaiyah[40];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[40]);
					gta3.tandaBaca = FindVowel(tekslatl[40]).ToString();

                    teksSoal.text = tekslatl[40];
                    sourceSoal.clip = audioSoal[40];
                    sourceSoal.Play();

                }
                else
                {
                    gta3.jawabansesungguhnya = hijaiyah[41];
                    gta3.jawabandibalik = ReverseXor(hijaiyah[41]);
					gta3.tandaBaca = FindVowel(tekslatl[41]).ToString();

                    teksSoal.text = tekslatl[41];
                    sourceSoal.clip = audioSoal[41];
                    sourceSoal.Play();

                }
                break;
            default:
                break;
        }
		
		tandaAtas.transform.GetChild(0).gameObject.name = 'a'.ToString();
		tandaTengah.transform.GetChild(0).gameObject.name = 'i'.ToString();
		tandaBawah.transform.GetChild(0).gameObject.name = 'u'.ToString();
		
        if (lokasisoal == 0 || lokasisoal == 3)
        {
			teks1.transform.parent.gameObject.name = "Correct";
			teks2.transform.parent.gameObject.name = "Wrong";
			teks3.transform.parent.gameObject.name = "Wrong";
		
            
			teks1.text = jawaban[saatinix];
            teks2.text = pengecoh1[saatinix];
            teks3.text = pengecoh2[saatinix];
        }
        else if (lokasisoal == 1 || lokasisoal == 4)
        {
			teks1.transform.parent.gameObject.name = "Wrong";
			teks2.transform.parent.gameObject.name = "Correct";
			teks3.transform.parent.gameObject.name = "Wrong";
            
			teks1.text = pengecoh1[saatinix];
            teks2.text = jawaban[saatinix];
            teks3.text = pengecoh2[saatinix];

        }
        else if (lokasisoal == 2 || lokasisoal == 5)
        {
			teks1.transform.parent.gameObject.name = "Wrong";
			teks2.transform.parent.gameObject.name = "Wrong";
			teks3.transform.parent.gameObject.name = "Correct";
			
            teks1.text = pengecoh2[saatinix];
            teks2.text = pengecoh1[saatinix];
            teks3.text = jawaban[saatinix];
        }
    }
	
    public static string ReverseXor(string s)
    {
        if (s == null) return null;
        char[] charArray = s.ToCharArray();
        int len = s.Length - 1;

        for (int i = 0; i < len; i++, len--)
        {
            charArray[i] ^= charArray[len];
            charArray[len] ^= charArray[i];
            charArray[i] ^= charArray[len];
        }

        return new string(charArray);
    }

	private char FindVowel(string word)
	{
		if(word == null || word.Length == 0) return '\0';
		HashSet<char> vowels = new HashSet<char>() {'a', 'i', 'u', 'e', 'o'};
		foreach(char alphabet in word) {
			if (vowels.Contains(System.Char.ToLower(alphabet))) return System.Char.ToLower(alphabet);
		}
		return '\0';
	}

}*/