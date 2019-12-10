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
        private string dicFile; // Содержимое словаря
        List<string> sourceWords; // Список исходных слов
        Dictionary<int, string> Dic = new Dictionary<int, string>(); // Словарь для поиска
        public Form1() {
            InitializeComponent();
        }

        private void открытьСловарьToolStripMenuItem_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            // читаем файл в строку
            dicFile = System.IO.File.ReadAllText(filename, Encoding.Default);
            int k = dicFile.Split('\n').Count();
            toolStripStatusLabel1.Text = "Объём словаря: " + k.ToString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {
            if (dicFile == null) throw new Exception("Словарь не обнаружен. Не забудьте открыть словарь");
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

        }
    }
}
