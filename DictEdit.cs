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
            if (Form1.dark == true) {
                this.BackColor = Color.FromArgb(22, 26, 33);
                label1.ForeColor = Color.White;
                textBox1.BackColor = Color.FromArgb(28, 36, 47);
                textBox1.BorderStyle = BorderStyle.FixedSingle;
                textBox1.ForeColor = Color.White;
                listBox1.BackColor = Color.FromArgb(28, 36, 47);
                listBox1.BorderStyle = BorderStyle.FixedSingle;
                listBox1.ForeColor = Color.White;
                button1.FlatStyle = FlatStyle.Flat;
                button1.BackColor = Color.FromArgb(22, 26, 33);
                button1.ForeColor = Color.FromArgb(39, 83, 102);

                button2.FlatStyle = FlatStyle.Flat;
                button2.BackColor = Color.FromArgb(22, 26, 33);
                button2.ForeColor = Color.FromArgb(39, 83, 102);

                button3.FlatStyle = FlatStyle.Flat;
                button3.BackColor = Color.FromArgb(22, 26, 33);
                button3.ForeColor = Color.FromArgb(39, 83, 102);
            } else {
                this.BackColor = SystemColors.Control;
                label1.ForeColor = SystemColors.ControlText;

                textBox1.BackColor = SystemColors.Window;
                textBox1.BorderStyle = BorderStyle.Fixed3D;
                textBox1.ForeColor = SystemColors.ControlText;

                listBox1.BackColor = SystemColors.Window;
                listBox1.BorderStyle = BorderStyle.Fixed3D;
                listBox1.ForeColor = SystemColors.ControlText;

                button1.FlatStyle = FlatStyle.Standard;
                button1.BackColor = SystemColors.Control;
                button1.ForeColor = SystemColors.ControlText;

                button2.FlatStyle = FlatStyle.Standard;
                button2.BackColor = SystemColors.Control;
                button2.ForeColor = SystemColors.ControlText;

                button3.FlatStyle = FlatStyle.Standard;
                button3.BackColor = SystemColors.Control;
                button3.ForeColor = SystemColors.ControlText;
            }
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

