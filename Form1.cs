using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace AnagramFinder {
    public partial class Form1 : Form {
        string dic;
        public Form1() {
            InitializeComponent();
        }

        private void открытьТекстToolStripMenuItem_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            // читаем файл в строку
            textBox1.Text = System.IO.File.ReadAllText(filename, Encoding.Default);
        }

        private void открытьСловарьToolStripMenuItem_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            // читаем файл в строку
            string dic = System.IO.File.ReadAllText(filename, Encoding.Default);
            int k = dic.Split('\n').Count();
            toolStripStatusLabel1.Text = "Объём словаря: " + k.ToString();
        }
    }
}
