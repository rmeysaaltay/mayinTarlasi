using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace odevmayinTarlasi
{
     class OyunCercevesi : Form
    {
        public const int GENISLIK = 10;
        public const int YUKSEKLIK = 10;
        public const int MAYIN_SAYISI = 10;

        private Butoons[,] buton = new Butoons[YUKSEKLIK, GENISLIK];

        public OyunCercevesi()
        {
            InitializeComponent();
            MayinYerlestir();
            PuanlariEkle();
            HerSeyiGoster();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(800, 800);
            this.Text = " Mayın";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // TableLayoutPanel kullanarak grid layout oluşturuyoruz
            TableLayoutPanel tableLayout = new TableLayoutPanel();
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.RowCount = YUKSEKLIK;
            tableLayout.ColumnCount = GENISLIK;

            // Satır ve sütun boyutlarını eşit olarak ayarlıyoruz
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
                    buton[i, j] = new Butoons(i, j);
                    buton[i, j].Dock = DockStyle.Fill;
                    buton[i, j].Font = new Font("Arial", 8, FontStyle.Bold);
                    tableLayout.Controls.Add(buton[i, j], j, i);
                }
            }

            this.Controls.Add(tableLayout);
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
                while (buton[x, y].mine); // Aynı yere iki kez mayın yerleştirme

                buton[x, y].mine = true;
            }
        }

        private void PuanlariEkle()
        {
            for (int i = 0; i < YUKSEKLIK; i++)
            {
                for (int j = 0; j < GENISLIK; j++)
                {
                    int puan = CevreKontrol(buton[i, j], i, j);
                    buton[i, j].point = puan;
                }
            }
        }

        private int CevreKontrol(Butoon merkezButon, int x, int y)
        {
            int baslangic_x = Math.Max(x - 1, 0);
            int baslangic_y = Math.Max(y - 1, 0);
            int bitis_x = Math.Min(x + 1, GENISLIK - 1);
            int bitis_y = Math.Min(y + 1, YUKSEKLIK - 1);
            int puan = 0;

            for (int i = baslangic_x; i <= bitis_x; i++)
            {
                for (int j = baslangic_y; j <= bitis_y; j++)
                {
                    if (i == x && j == y) continue;
                    if (buton[i, j].mine)
                    {
                        puan++;
                    }
                }
            }
            return puan;
        }

        private void HerSeyiGoster()
        {
            for (int i = 0; i < YUKSEKLIK; i++)
            {
                for (int j = 0; j < GENISLIK; j++)
                {
                    if (buton[i, j].mine)
                    {
                        buton[i, j].Text = "mayın";
                        buton[i, j].BackColor = Color.Red;
                    }
                    else
                    {
                        buton[i, j].Text = buton[i, j].point.ToString();
                        if (buton[i, j].point > 0)
                        {
                            buton[i, j].BackColor = Color.LightBlue;
                        }
                    }
                }
            }
        }
    }
}