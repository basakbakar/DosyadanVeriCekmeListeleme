/****************************************************************************
**					SAKARYA ÜNİVERSİTESİ
**			BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
**			    BİLGİSAYAR MÜHENDİSLİĞİ BÖLÜMÜ
**			   NESNEYE DAYALI PROGRAMLAMA DERSİ
**					2018-2019 BAHAR DÖNEMİ
**	
**				ÖDEV NUMARASI..........:1
**				ÖĞRENCİ ADI............:Başak Bakar
**				ÖĞRENCİ NUMARASI.......:B171210075
**              DERSİN ALINDIĞI GRUP...:D
****************************************************************************/
using System;
using System.Windows.Forms;
using System.IO;

namespace DosyadanOku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        class KisiBilgileri
        {
            public string TC;//I defined the informations which are going to be taken from the file in the class I named KisiBilgileri.
            public string Ad;
            public string Soyad;
            public int Yas;
            public double CalismaSuresi;
            public string EvlilikDurumu;
            public string EsiCalisiyormu;
            public int CocukSayisi;
            public double TabanMaas;
            public double MakamTazminati;
            public double IdariGorevTazminati;
            public double FazlaMesaiSaati;
            public double FazlaMesaiSaatUcreti;
            public double VergiMatrahi;
            public string PersonelResmiYolu;

            public double BurutMaas;//I defined the results which are going to be calculated.
            public double DamgaVergisi;
            public double GelirVergisi;
            public double EmekliKesintisi;
            public double NetMaas;
        }

        private void button1_Click(object sender, EventArgs e)//I wrote the code for choosing a file after clicking button1 named "Seç".
        {
            string d;
            OpenFileDialog odf = new OpenFileDialog();
            if(odf.ShowDialog()==DialogResult.OK)
            {
                d = File.ReadAllText(odf.FileName);
                richTextBox1.Text = d;
            }
        }

        KisiBilgileri k1 = new KisiBilgileri();//I created an object from the class KisiBilgileri to use the elements of that class.  

        private void button2_Click(object sender, EventArgs e)
        {
            string[] satirlar;//I defined an array to have the informations in each lines.
            string hepsi = richTextBox1.Text;
            satirlar = hepsi.Split('\n');

            foreach (string s in satirlar) 
            {
                string[] kelimeler = s.Split(' ');//I created an array to get all the informations of one person.
               
                if (TcKutusu.Text=="")//I printed an if line not to be able to click button2 when it is empty.
                    MessageBox.Show("Lütfen TC'nizi giriniz.");
                
                else if (TcKutusu.Text == kelimeler[0])//I made the user see her/his information with entering her/his TC. 
                {
                    k1.TC = kelimeler[0];//I transfered the elements of kelimeler array to the elements of the KisiBilgileri  class.
                    k1.Ad = kelimeler[1];
                    k1.Soyad = kelimeler[2];
                    k1.Yas = Convert.ToInt32(kelimeler[3]);
                    k1.CalismaSuresi = Convert.ToInt32(kelimeler[4]);
                    if (k1.EvlilikDurumu == "E")//I wanted the marital status of the person to be printed.
                        kelimeler[5] = "Evli";
                    else
                        kelimeler[5]="Bekar";
                    k1.EvlilikDurumu = kelimeler[5];
                    if (k1.EsiCalisiyormu == "E")//I wanted the working status of the person's husband/wife to be printed.
                        kelimeler[6] = "Evet";
                    else
                        kelimeler[6] = "Hayır";
                    k1.EsiCalisiyormu = kelimeler[6];
                    k1.CocukSayisi = Convert.ToInt32(kelimeler[7]);
                    k1.TabanMaas = Convert.ToDouble(kelimeler[8]);
                    k1.MakamTazminati = Convert.ToDouble(kelimeler[9]);
                    k1.IdariGorevTazminati = Convert.ToDouble(kelimeler[10]);
                    k1.FazlaMesaiSaati = Convert.ToDouble(kelimeler[11]);
                    k1.FazlaMesaiSaatUcreti = Convert.ToDouble(kelimeler[12]);
                    k1.VergiMatrahi = Convert.ToDouble(kelimeler[13]);
                    k1.PersonelResmiYolu = kelimeler[14];
                    
                    if (k1.EvlilikDurumu == "B")//I calculated the bürüt maaş information to the marital status.
                        k1.BurutMaas = k1.TabanMaas + k1.MakamTazminati + k1.IdariGorevTazminati + k1.CocukSayisi * 30 + k1.FazlaMesaiSaati * k1.FazlaMesaiSaatUcreti;
                    else
                    {
                        if (k1.EsiCalisiyormu == "E")//If the person is married and his/her husband/wife is working, bürüt maaş information is going to be calculated to here.
                            k1.BurutMaas = k1.TabanMaas + k1.MakamTazminati + k1.IdariGorevTazminati +
                                k1.CocukSayisi * 30 + k1.FazlaMesaiSaati * k1.FazlaMesaiSaatUcreti;
                        else
                            k1.BurutMaas = k1.TabanMaas + k1.MakamTazminati + k1.IdariGorevTazminati +
                                200 + k1.CocukSayisi * 30 + k1.FazlaMesaiSaati * k1.FazlaMesaiSaatUcreti;
                    }

                    k1.DamgaVergisi = k1.BurutMaas * 10 / 100;//I calculated the Damga Vergisi information here.

                    if (k1.VergiMatrahi < 10000)//ı calculated the Vergi Matrahı information to its amount.
                        k1.GelirVergisi = k1.BurutMaas * 15 / 100;
                    else if (10000 <= k1.VergiMatrahi && k1.VergiMatrahi < 20000)
                        k1.GelirVergisi = k1.BurutMaas * 20 / 100;
                    else if (20000 <= k1.VergiMatrahi && k1.VergiMatrahi < 30000)
                        k1.GelirVergisi = k1.BurutMaas * 25 / 100;
                    else
                        k1.GelirVergisi = k1.BurutMaas * 30 / 100;

                    k1.EmekliKesintisi = k1.BurutMaas * 15 / 100;//I calculated the Emekli Kesintisi information here.

                    k1.NetMaas = k1.BurutMaas - (k1.EmekliKesintisi + k1.GelirVergisi + k1.DamgaVergisi);//I calculated the Net Maaş information here.

                    label7.Text = Convert.ToString(k1.BurutMaas);//I made the calculations to be printed to the form.
                    label8.Text = Convert.ToString(k1.DamgaVergisi);
                    label9.Text = Convert.ToString(k1.GelirVergisi);
                    label10.Text = Convert.ToString(k1.EmekliKesintisi);
                    label11.Text = Convert.ToString(k1.NetMaas);

                    label30.Text = k1.Ad;//I made the informations of the person printed to the form.
                    label31.Text = k1.Soyad;
                    label32.Text = Convert.ToString(k1.Yas);
                    label33.Text = Convert.ToString(k1.CalismaSuresi);
                    label34.Text = k1.EvlilikDurumu;
                    label35.Text = k1.EsiCalisiyormu;
                    label36.Text = Convert.ToString(k1.CocukSayisi);
                    label17.Text = Convert.ToString(k1.TabanMaas);
                    label18.Text = Convert.ToString(k1.MakamTazminati);
                    label19.Text = Convert.ToString(k1.IdariGorevTazminati);
                    label20.Text = Convert.ToString(k1.FazlaMesaiSaati);
                    label21.Text = Convert.ToString(k1.FazlaMesaiSaatUcreti);
                    label39.Text = Convert.ToString(k1.VergiMatrahi);
                    pictureBox1.ImageLocation = k1.PersonelResmiYolu; //I made the image of the person to be seemed in the form.
                }
            }
        }
    }
}