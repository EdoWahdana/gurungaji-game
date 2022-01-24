using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class GetAllAnswer3 : MonoBehaviour
{}
    /*public List<Drag> drax;
    public GameObject[] goDrag;
	public GameObject placeholder1, placeholder2, placeholder3;
    public Drag[] dras;
    public List<string> jawaban1,jawaban2;
    public string jawabannya;
    public string jawabansesungguhnya;
    public string jawabandibalik;
	public string status1, status2, tandaBaca;
    public int jumlahjawaban,count1,count2,totalcount;
    public bool benar, salah;
    public AcakSoal3 acs3;
    public AcakSoal3 ac5;
	
	private string pilihan1, pilihan2;

    
    public void GetJawaban()
    {
        drax = new List<Drag>();
        goDrag = GameObject.FindGameObjectsWithTag("Objek3");
        int panjang = goDrag.Length;
        
        if (jumlahjawaban == 2)
        {
        

            //if (jawabansesungguhnya == jawabannya || jawabandibalik == jawabannya || jawabansesungguhnya == balikjawaban || jawabandibalik == balikjawaban)
			if(status1 == "Correct" && status2 == tandaBaca)
            {
                ClearState();
                acs3.Benar();
            }
            else 
            {
				ClearState();
                acs3.Salah();
            }
        }
        else if(jumlahjawaban == 1)
        {
            for (int l = 0; l < panjang; l++)
            {
                if (dras[l].name=="Tanda Baca Atas"||dras[l].name=="Tanda Baca Bawah")
                {
                    dras[l].dragable = true;
                }
            }
        }
        else if(jumlahjawaban ==0)
        {
            for (int l = 0; l < panjang; l++)
            {
                if (dras[l].name == "Tanda Baca Atas" || dras[l].name == "Tanda Baca Bawah")
                {
                    dras[l].dragable = false;
                }
            }

            jawabannya = "";
        }
    }
	
    void Update()
    {
        int panjang = dras.Length;
        if (jumlahjawaban == 0)
        {
            for (int l = 0; l < panjang; l++)
            {
                if (dras[l].name == "Tanda Baca Atas" || dras[l].name == "Tanda Baca Bawah")
                {
                    dras[l].dragable = false;
                }
            }
        }
    }
	
	void ClearState() 
	{
		dras[0].selesai = true;
		dras[1].selesai = true;
		dras[2].selesai = true;
		dras[3].selesai = true;
		dras[4].selesai = true;
		dras[5].selesai = true;
		jawaban1[0] = "";
		jawaban1[1] = "";
		jawaban1[2] = "";
		jawaban1[3] = "";
		jawaban1[4] = "";
		jawaban1[5] = "";
		jawaban2[0] = "";
		jawaban2[1] = "";
		jawaban2[2] = "";
		jawaban2[3] = "";
		jawaban2[4] = "";
		jawaban2[5] = "";
		jumlahjawaban = 0;
		jawabannya = "";
		status1 = "";
		status2 = "";
	}
}*/
