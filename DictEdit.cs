using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace AnagramFinder {
    public partial class DictEdit : Form {
        public DictEdit() {
            if (Form1.filename == null) throw new Exception("Словарь не обнаружен. Не забудьте открыть словарь");
            InitializeComponent();
        }

        private void DictEdit_Load(object sender, EventArgs e) {
            listBox1.Items.AddRange(File.ReadAllLines(Form1.filename, Encoding.UTF8));
        }

        private void button2_Click(object sender, EventArgs e) {
            if (listBox1.SelectedIndex != -1) {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            TextWriter writer = new StreamWriter(Form1.filename);
            foreach (var item in listBox1.Items)
                writer.WriteLine(item.ToString());
            writer.Close();
            MessageBox.Show("Успешно!");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (!listBox1.Items.Contains(textBox1.Text))
                listBox1.Items.Add(textBox1.Text);
            else MessageBox.Show("Слово уже есть в словаре");
        }
    }
}

