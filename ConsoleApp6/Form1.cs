using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp6
{
    public partial class Form1: Form
    {
    public const int GENISLIK = 10;
    public const int YUKSEKLIK = 10;
    public const int MAYIN_SAYISI = 10;

    private Buttons[,] butons = new Buttons[YUKSEKLIK, GENISLIK];

    public Form1()
    {
        InitializeComponent();
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
                butons[i, j] = new Buttons(i, j);
                butons[i, j].Dock = DockStyle.Fill;
                butons[i, j].Font = new Font("Arial", 8, FontStyle.Bold);
                tableLayout.Controls.Add(butons[i, j], j, i);
            }
        }

        this.Controls.Add(tableLayout);

        // Oyunu başlat
        MayinYerlestir();
        PuanlariEkle();
        HerSeyiGoster();
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
                int puan = CevreKontrol(butons[i, j], i, j);
                butons[i, j].point = puan;
            }
        }
    }

    private int CevreKontrol(Buttons merkezButon, int x, int y)
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
                if (butons[i, j].mine)
                {
                    puan++;
                }
            }
        }
        return puan;
    }

    //private void HerSeyiGoster()
    //{
    //    for (int i = 0; i < YUKSEKLIK; i++)
    //    {
    //        for (int j = 0; j < GENISLIK; j++)
    //        {
    //            if (butons[i, j].mine)
    //            {
    //                butons[i, j].Text = "💣";
    //                butons[i, j].BackColor = Color.Red;
    //            }
    //            else
    //            {
    //                if (butons[i, j].point > 0)
    //                {
    //                    butons[i, j].Text = butons[i, j].point.ToString();
    //                    butons[i, j].BackColor = Color.LightBlue;
    //                }
    //                else
    //                {
    //                    butons[i, j].Text = "";
    //                    butons[i, j].BackColor = Color.LightGray;
    //                }
    //            }
    //        }
    //    }
    //}
}
}
