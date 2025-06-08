using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Odev2
{
    // HSDButton sınıfı
    public class HSDButton : Button
    {
        public int row { get; set; }
        public int column { get; set; }
        public int point { get; set; }
        public bool mine { get; set; }
        public bool flag { get; set; }
        public bool revealed { get; set; } // Açılmış mı kontrolü için

        public HSDButton(int row, int column)
        {
            this.row = row;
            this.column = column;
            this.mine = false;
            this.flag = false;
            this.point = 0;
            this.revealed = false;
        }

        public HSDButton()
        {
            this.mine = false;
            this.flag = false;
            this.point = 0;
            this.revealed = false;
        }

        public bool IsMine()
        {
            return mine;
        }
    }

    // Ana Form sınıfı
    public partial class Form1 : Form
    {
        public const int GENISLIK = 10;
        public const int YUKSEKLIK = 10;
        public const int MAYIN_SAYISI = 10;

        private HSDButton[,] hsDbutons = new HSDButton[YUKSEKLIK, GENISLIK];
        private bool oyunBitti = false;

        public Form1()
        {
            //InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Form ayarları
            this.Size = new Size(800, 800);
            this.Text = "HSD Mayın Tarlası";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // TableLayoutPanel oluştur
            TableLayoutPanel tableLayout = new TableLayoutPanel();
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.RowCount = YUKSEKLIK;
            tableLayout.ColumnCount = GENISLIK;

            // Satır ve sütun boyutları
            for (int i = 0; i < YUKSEKLIK; i++)
            {
                tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / YUKSEKLIK));
            }
            for (int j = 0; j < GENISLIK; j++)
            {
                tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / GENISLIK));
            }

            // Butonları oluştur ve ekle
            for (int i = 0; i < YUKSEKLIK; i++)
            {
                for (int j = 0; j < GENISLIK; j++)
                {
                    hsDbutons[i, j] = new HSDButton(i, j);
                    hsDbutons[i, j].Dock = DockStyle.Fill;
                    hsDbutons[i, j].Font = new Font("Arial", 12, FontStyle.Bold);
                    hsDbutons[i, j].BackColor = Color.LightGray;
                    hsDbutons[i, j].Text = "";

                    // Sol tık olayı
                    hsDbutons[i, j].Click += ButonTiklandi;

                    // Sağ tık olayı (bayrak için)
                    hsDbutons[i, j].MouseDown += ButonSagTiklandi;

                    tableLayout.Controls.Add(hsDbutons[i, j], j, i);
                }
            }

            this.Controls.Add(tableLayout);

            // Oyunu başlat
            MayinYerlestir();
            PuanlariEkle();
            // HerSeyiGoster(); // Bu satırı kaldırdık çünkü başlangıçta gizli olmalı
        }

        private void ButonTiklandi(object sender, EventArgs e)
        {
            if (oyunBitti) return;

            HSDButton tiklananButon = sender as HSDButton;

            // Eğer bayrak varsa veya zaten açılmışsa işlem yapma
            if (tiklananButon.flag || tiklananButon.revealed) return;

            // Butonu aç
            ButonuAc(tiklananButon);

            // Oyun durumunu kontrol et
            OyunDurumuKontrol();
        }

        private void ButonSagTiklandi(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && !oyunBitti)
            {
                HSDButton tiklananButon = sender as HSDButton;

                // Eğer buton açılmışsa bayrak koyma
                if (tiklananButon.revealed) return;

                // Bayrak koy/kaldır
                if (tiklananButon.flag)
                {
                    tiklananButon.flag = false;
                    tiklananButon.Text = "";
                    tiklananButon.BackColor = Color.LightGray;
                }
                else
                {
                    tiklananButon.flag = true;
                    tiklananButon.Text = "🚩";
                    tiklananButon.BackColor = Color.Yellow;
                }
            }
        }

        private void ButonuAc(HSDButton buton)
        {
            if (buton.revealed) return;

            buton.revealed = true;

            if (buton.mine)
            {
                // Mayına bastı - oyun bitti
                buton.Text = "💣";
                buton.BackColor = Color.Red;
                OyunuBitir(false);
            }
            else
            {
                // Güvenli alan
                if (buton.point > 0)
                {
                    buton.Text = buton.point.ToString();
                    buton.BackColor = Color.LightBlue;
                }
                else
                {
                    buton.Text = "";
                    buton.BackColor = Color.White;

                    // Eğer etrafında mayın yoksa, komşu alanları da aç
                    KomsuAlanlariAc(buton.row, buton.column);
                }
            }
        }

        private void KomsuAlanlariAc(int x, int y)
        {
            int baslangic_x = Math.Max(x - 1, 0);
            int baslangic_y = Math.Max(y - 1, 0);
            int bitis_x = Math.Min(x + 1, YUKSEKLIK - 1);
            int bitis_y = Math.Min(y + 1, GENISLIK - 1);

            for (int i = baslangic_x; i <= bitis_x; i++)
            {
                for (int j = baslangic_y; j <= bitis_y; j++)
                {
                    if (i == x && j == y) continue;
                    if (!hsDbutons[i, j].revealed && !hsDbutons[i, j].flag)
                    {
                        ButonuAc(hsDbutons[i, j]);
                    }
                }
            }
        }

        private void OyunDurumuKontrol()
        {
            int acikAlanSayisi = 0;

            for (int i = 0; i < YUKSEKLIK; i++)
            {
                for (int j = 0; j < GENISLIK; j++)
                {
                    if (hsDbutons[i, j].revealed && !hsDbutons[i, j].mine)
                    {
                        acikAlanSayisi++;
                    }
                }
            }

            // Tüm güvenli alanlar açıldıysa oyunu kazan
            if (acikAlanSayisi == (YUKSEKLIK * GENISLIK - MAYIN_SAYISI))
            {
                OyunuBitir(true);
            }
        }

        private void OyunuBitir(bool kazandi)
        {
            oyunBitti = true;

            if (kazandi)
            {
                this.Text = "HSD Mayın Tarlası - TEBRİKLER! KAZANDINIZ!";
                MessageBox.Show("Tebrikler! Oyunu kazandınız!", "Kazandınız!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.Text = "HSD Mayın Tarlası - KAYBETTINIZ!";
                // Tüm mayınları göster
                for (int i = 0; i < YUKSEKLIK; i++)
                {
                    for (int j = 0; j < GENISLIK; j++)
                    {
                        if (hsDbutons[i, j].mine && !hsDbutons[i, j].flag)
                        {
                            hsDbutons[i, j].Text = "💣";
                            hsDbutons[i, j].BackColor = Color.Red;
                        }
                    }
                }
                MessageBox.Show("Mayına bastınız! Oyunu kaybettiniz.", "Kaybettiniz!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MayinYerlestir()
        {
            Random rastgele = new Random();

            for (int i = 0; i < MAYIN_SAYISI; i++)
            {
                int x, y;
                do
                {
                    x = rastgele.Next(0, YUKSEKLIK);
                    y = rastgele.Next(0, GENISLIK);
                }
                while (hsDbutons[x, y].mine);

                hsDbutons[x, y].mine = true;
            }
        }

        private void PuanlariEkle()
        {
            for (int i = 0; i < YUKSEKLIK; i++)
            {
                for (int j = 0; j < GENISLIK; j++)
                {
                    if (!hsDbutons[i, j].mine)
                    {
                        int puan = CevreKontrol(hsDbutons[i, j], i, j);
                        hsDbutons[i, j].point = puan;
                    }
                }
            }
        }

        private int CevreKontrol(HSDButton merkezButon, int x, int y)
        {
            int baslangic_x = Math.Max(x - 1, 0);
            int baslangic_y = Math.Max(y - 1, 0);
            int bitis_x = Math.Min(x + 1, YUKSEKLIK - 1);
            int bitis_y = Math.Min(y + 1, GENISLIK - 1);
            int puan = 0;

            for (int i = baslangic_x; i <= bitis_x; i++)
            {
                for (int j = baslangic_y; j <= bitis_y; j++)
                {
                    if (i == x && j == y) continue;
                    if (hsDbutons[i, j].mine)
                    {
                        puan++;
                    }
                }
            }
            return puan;
        }
    }
}