using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp6
{
    public class Buttons : Button
    {
        public int row { get; set; }
        public int column { get; set; }
        public int point { get; set; }
        public bool mine { get; set; }
        public bool flag { get; set; }

        public Buttons(int row, int column)
        {
            this.row = row;
            this.column = column;
            this.mine = false;
            this.flag = false;
            this.point = 0;
        }

        public Buttons()
        {
            this.mine = false;
            this.flag = false;
            this.point = 0;
        }

        public bool IsMine()
        {
            return mine;
        }
    }
}