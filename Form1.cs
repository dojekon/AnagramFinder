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
using System.Drawing.Printing;

namespace AnagramFinder {
    public partial class Form1 : Form {
        private string dicFile; // Содержимое словаря
        List<string> sourceWords; // Список исходных слов
        Dictionary<int, string> Dic = new Dictionary<int, string>(); // Словарь для поиска
        public static string filename;
        public static bool dark = false;
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
            filename = openFileDialog1.FileName;
            // читаем файл в строку
            dicFile = System.IO.File.ReadAllText(filename, Encoding.UTF8);
            int k = dicFile.Split('\n').Count();
            toolStripStatusLabel1.Text = "Объём словаря: " + k.ToString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {
            if (dicFile == null) throw new Exception("Словарь не обнаружен. Не забудьте открыть словарь");
            textBox2.Text = "";
            sourceWords = new List<string>(textBox1.Lines); // Заполняем список строками из textBox
            string[] words = dicFile.Split('\n'); //Разделяем строки
            int i = 0; // Счётчик ключей
            foreach (string s in words) { // Заполняем словарь словами из файла
                string temp = s.Trim('\r');
                Dic.Add(i, temp);
                i++;
            }
            List<int> keys = new List<int>(); // Создаём список ключей слов, являющихся анаграммами
            foreach (var word in sourceWords) { // Получение элемента списка исходных слов
                keys.Clear();
                string key = new string(word.OrderBy(c => c).ToArray()); // Сортируем в алфавитном порядке исходное слово
                foreach (int compareWordKey in Dic.Keys) { // Получение элемента словаря
                    if (word == Dic[compareWordKey]) continue; // Необходимо для пропуска исходного слова, при наличии его в словаре
                    string compareWord = new string(Dic[compareWordKey].OrderBy(c => c).ToArray()); // Сортируем в алфавитном порядке слово из словаря
                    if (key == compareWord) keys.Add(compareWordKey); // Сравниваем исходное слово и слово из словаря. В случае совпадения, заносим порядковый номер этого слова в список
                }
                if (keys.Count > 0) {  // Вывод найденных слов
                    textBox2.Text += word + " = ";
                    foreach (int WordKey in keys)
                        textBox2.Text += Dic[WordKey] + "; ";
                    textBox2.Text += Environment.NewLine;
                }        
            }
            Dic.Clear();
            sourceWords.Clear();
        }

        private void редактироватьСловарьToolStripMenuItem_Click(object sender, EventArgs e) {
            DictEdit dictEdit = new DictEdit();
            dictEdit.ShowDialog();
        }

        private void сохранитьРезультатToolStripMenuItem_Click(object sender, EventArgs e) {
            saveFileDialog1.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                StreamWriter streamWriter = new StreamWriter(saveFileDialog1.FileName);
                streamWriter.WriteLine(textBox2.Text);
                streamWriter.Close();
            }
        }

        void PrintPageHandler(object sender, PrintPageEventArgs e) {
            e.Graphics.DrawString(textBox2.Text, new Font("Arial", 14), Brushes.Black, 0, 0);
        }

        private void печатьРезультатаToolStripMenuItem_Click(object sender, EventArgs e) {
            
            PrintDocument printDocument = new PrintDocument(); // объект для печати
            printDocument.PrintPage += PrintPageHandler; // обработчик события печати
            PrintDialog printDialog = new PrintDialog(); // диалог настройки печати
            printDialog.Document = printDocument;// установка объекта печати для его настройки
            
            if (printDialog.ShowDialog() == DialogResult.OK) // если в диалоге было нажато ОК
                printDialog.Document.Print(); // печатаем
        }

        private void тёмнаяToolStripMenuItem_Click(object sender, EventArgs e) {
            dark = true;
            this.BackColor = Color.FromArgb(22, 26, 33);
            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;
            textBox1.BackColor = Color.FromArgb(28, 36, 47);
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.ForeColor = Color.White;
            textBox2.BackColor = Color.FromArgb(28, 36, 47);
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.ForeColor = Color.White;
            statusStrip1.BackColor = Color.FromArgb(28, 36, 47);
            statusStrip1.ForeColor = Color.White;
        }

        private void светлаяToolStripMenuItem_Click(object sender, EventArgs e) {
            dark = false;
            this.BackColor = SystemColors.Control;
            label1.ForeColor = SystemColors.ControlText;
            label2.ForeColor = SystemColors.ControlText;
            textBox1.BackColor = SystemColors.Window;
            textBox1.BorderStyle = BorderStyle.Fixed3D;
            textBox1.ForeColor = SystemColors.ControlText;
            textBox2.BackColor = SystemColors.ControlLight;
            textBox2.BorderStyle = BorderStyle.Fixed3D;
            textBox2.ForeColor = SystemColors.ControlText;
            statusStrip1.BackColor = SystemColors.Control;
            statusStrip1.ForeColor = SystemColors.ControlText;
        }
    }
}
