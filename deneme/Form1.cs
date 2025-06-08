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
    public class Buttons : Button
    {
        public int row { get; set; }
        public int column { get; set; }
        public int point { get; set; }
        public bool mine { get; set; }
        public bool flag { get; set; }
        public bool revealed { get; set; }

        public Buttons(int row, int column)
        {
            this.row = row;
            this.column = column;
            this.mine = false;
            this.flag = false;
            this.point = 0;
            this.revealed = false;
        }

        public Buttons()
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

    public partial class Form1 : Form
    {
        public const int GENISLIK = 10;
        public const int YUKSEKLIK = 10;
        public const int MAYIN_SAYISI = 10;

        private Buttons[,] butons = new Buttons[YUKSEKLIK, GENISLIK];
        private bool oyunBitti = false;

        public Form1()
        {
            //InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            this.Size = new Size(800, 800);
            this.Text = "Mayın Tarlası";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            TableLayoutPanel tableLayout = new TableLayoutPanel();
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.RowCount = YUKSEKLIK;
            tableLayout.ColumnCount = GENISLIK;

            for (int i = 0; i < YUKSEKLIK; i++)
            {
                tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / YUKSEKLIK));
            }
            for (int j = 0; j < GENISLIK; j++)
            {
                tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / GENISLIK));
            }

            for (int i = 0; i < YUKSEKLIK; i++)
            {
                for (int j = 0; j < GENISLIK; j++)
                {
                    butons[i, j] = new Buttons(i, j);
                    butons[i, j].Dock = DockStyle.Fill;
                    butons[i, j].Font = new Font("Arial", 12, FontStyle.Bold);
                    butons[i, j].BackColor = Color.LightGray;
                    butons[i, j].Text = "";

                    butons[i, j].Click += ButonTiklandi;
                    butons[i, j].MouseDown += ButonSagTiklandi;

                    tableLayout.Controls.Add(butons[i, j], j, i);
                }
            }

            this.Controls.Add(tableLayout);

            MayinYerlestir();
            PuanlariEkle();
        }

        private void ButonTiklandi(object sender, EventArgs e)
        {
            if (oyunBitti) return;

            Buttons tiklananButon = sender as Buttons;

            if (tiklananButon.flag || tiklananButon.revealed) return;

            ButonuAc(tiklananButon);

            OyunDurumuKontrol();
        }

        private void ButonSagTiklandi(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && !oyunBitti)
            {
                Buttons tiklananButon = sender as Buttons;

                if (tiklananButon.revealed) return;

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

        private void ButonuAc(Buttons buton)
        {
            if (buton.revealed) return;

            buton.revealed = true;

            if (buton.mine)
            {
                buton.Text = "💣";
                buton.BackColor = Color.Red;
                OyunuBitir(false);
            }
            else
            {
                if (buton.point > 0)
                {
                    buton.Text = buton.point.ToString();
                    buton.BackColor = Color.LightBlue;
                }
                else
                {
                    buton.Text = "";
                    buton.BackColor = Color.White;

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
                    if (!butons[i, j].revealed && !butons[i, j].flag)
                    {
                        ButonuAc(butons[i, j]);
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
                    if (butons[i, j].revealed && !butons[i, j].mine)
                    {
                        acikAlanSayisi++;
                    }
                }
            }

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

                for (int i = 0; i < YUKSEKLIK; i++)
                {
                    for (int j = 0; j < GENISLIK; j++)
                    {
                        if (butons[i, j].mine && !butons[i, j].flag)
                        {
                            butons[i, j].Text = "💣";
                            butons[i, j].BackColor = Color.Red;
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
                while (butons[x, y].mine);

                butons[x, y].mine = true;
            }
        }

        private void PuanlariEkle()
        {
            for (int i = 0; i < YUKSEKLIK; i++)
            {
                for (int j = 0; j < GENISLIK; j++)
                {
                    if (!butons[i, j].mine)
                    {
                        int puan = CevreKontrol(butons[i, j], i, j);
                        butons[i, j].point = puan;
                    }
                }
            }
        }

        private int CevreKontrol(Buttons merkezButon, int x, int y)
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
                    if (butons[i, j].mine)
                    {
                        puan++;
                    }
                }
            }
            return puan;
        }
    }
}
