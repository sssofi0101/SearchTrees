using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Trees
{
    public partial class Form1 : Form
    {        
        public static bool StringIsValid(string str)
        {
            return !string.IsNullOrEmpty(str) && !Regex.IsMatch(str, @"[^a-z]");
        }

        private bool check = false; //проверка добавлять в массив или нет
        private string[] stringArray = new string[0]; // массив слов
        public void Insertion(string i)
        {
            if (check == false) // если проверка false, то слово будет добавляться в массив
            {
                Array.Resize(ref stringArray, stringArray.Length + 1);
                stringArray[stringArray.Length - 1] = i;
                pt1.TrieInsert(i.ToLower());
                pt2.TSTInsert(i);
                pt3.BSTInsert(i);
            }
            if (check == true) // если true, то слово не добавляется в массив
            {
                pt1.TrieInsert(i.ToLower());
                pt2.TSTInsert(i);
                pt3.BSTInsert(i);
            }
        }
        public void FileInsertion(string a)
        {
            string text;
            using (StreamReader sr = new StreamReader(a))
            {
                text = sr.ReadToEnd();
            }
            string[] t = text.Split(new char[] { ' ', '\n', '_', '\t', '\r', '-' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < t.Length; i++)
            {
                Insertion(t[i].ToLower());
            }
        }
        private readonly string filename1 = "../../Tests/Python.txt";
        private readonly string filename2 = "../../Tests/C.txt";
        private readonly string filename3 = "../../Tests/CSH.txt";
        private readonly string filename4 = "../../Tests/Java.txt";
        private readonly string filename5 = "../../Tests/100000.txt";
        private readonly string filename6 = "../../Tests/10000.txt";
        private readonly string filename7 = "../../Tests/1000.txt";
        private readonly string filename8 = "../../Tests/100.txt";
        private readonly string filename9 = "..\\..\\Вывод"; //путь к папке вывода 
        private readonly string filename10 = "..\\..\\Вывод\\Вывод.txt"; //путь на вывод
        private int fals;
        public Form1()
        {
            Directory.CreateDirectory(filename9); //создаёт папку где будет храниться вывод
            MessageBox.Show("Добро пожаловать в программу, предназначенную для работы с деревьями. Вы можете добавлять, удалять и искать слова, загружать готовые библиотеки и словари, и работать с ними. Если у вас появился вопрос, связанный с работой программы, или вы хотите посмотреть все добавленные слова, вы можете найти на него ответ в 'Справке', кнопка вызова которой находится в правой верхней часте программы. Внимание! Помните, программа может взаимодействовать только со словами, написанными только английскими буквами, без любых знаков и пробелов.");
            InitializeComponent();

        }
        private Trie pt1 = new Trie();
        private TST pt2 = new TST();
        private BST pt3 = new BST();
        private void Button1_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            label2.Visible = false;
            if (StringIsValid(richTextBox1.Text.ToLower()))
            {
                MessageBox.Show("Данное слово успешно добавлено");
                Insertion(richTextBox1.Text);
            }
            else { MessageBox.Show("Вводимая строка должна содержать только буквы латинского алфавита"); }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            label2.Visible = false;
            if (StringIsValid(richTextBox1.Text.ToLower()))
            {
                List<string> list = stringArray.Cast<string>().ToList();//удаление слова из листа и перевод в массив
                list.Remove(richTextBox1.Text);                //удаление слова из листа и перевод в массив
                stringArray = list.ToArray();                  //удаление слова из листа и перевод в массив
                fals = 0;
                fals += pt1.TrieDelete(richTextBox1.Text);
                fals += pt2.TSTDelete(richTextBox1.Text);
                fals += pt3.BSTDelete(richTextBox1.Text);
                if (fals == 0) { MessageBox.Show("Данное слово отсутствует в дереве"); }
                else { MessageBox.Show("Данное слово успешно удалено"); }
            }
            else
            {
                MessageBox.Show("Вводимая строка должна содержать только буквы латинского алфавита");
            }

        }
        private void Button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                System.Diagnostics.Stopwatch elapsedTime = System.Diagnostics.Stopwatch.StartNew();
                pt1.TrieSearch(richTextBox1.Text);
                for (int i = 0; i < 2500; i++)
                {
                    fals = pt1.TrieSearch(richTextBox1.Text);
                }
                elapsedTime.Stop();
                textBox1.Visible = true;
                label2.Visible = true;
                textBox1.Text = ((elapsedTime.Elapsed.TotalMilliseconds / 2500).ToString());
            }
            if (radioButton2.Checked)
            {
                System.Diagnostics.Stopwatch elapsedTime = System.Diagnostics.Stopwatch.StartNew();
                pt2.TSTSearch(richTextBox1.Text);
                for (int i = 0; i < 2500; i++)
                {
                    fals = pt2.TSTSearch(richTextBox1.Text);
                }
                elapsedTime.Stop();
                textBox1.Visible = true;
                label2.Visible = true;
                textBox1.Text = ((elapsedTime.Elapsed.TotalMilliseconds / 2500).ToString());
            }
            if (radioButton4.Checked)
            {
                System.Diagnostics.Stopwatch elapsedTime = System.Diagnostics.Stopwatch.StartNew();
                pt3.BSTSearch(richTextBox1.Text);
                for (int i = 0; i < 2500; i++)
                {
                    fals = pt3.BSTSearch(richTextBox1.Text);
                }
                elapsedTime.Stop();
                textBox1.Visible = true;
                label2.Visible = true;
                textBox1.Text = ((elapsedTime.Elapsed.TotalMilliseconds / 2500).ToString());
            }
            if (fals == 0) { MessageBox.Show("Данное слово отсутсвует в дереве"); }
            else { MessageBox.Show("Данное слово присутствует в дереве"); }
        }
        private void LoadPythonLibToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check = true; //устанавливает true, чтобы слова не добавлялись в массив
            Array.Resize<string>(ref stringArray, 0); //очистит массив
            pt1 = new Trie();
            pt2 = new TST();
            pt3 = new BST();
            stringArray = File.ReadAllLines(filename1); //занесёт все слова из файла в массив
            FileInsertion(filename1);
            check = false;
        }
        private void LoadCSharpLibToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check = true;
            Array.Resize<string>(ref stringArray, 0);
            pt1 = new Trie();
            pt2 = new TST();
            pt3 = new BST();
            stringArray = File.ReadAllLines(filename3);
            FileInsertion(filename3);
            check = false;
        }
        private void LoadCLibToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check = true;
            Array.Resize<string>(ref stringArray, 0);
            pt1 = new Trie();
            pt2 = new TST();
            pt3 = new BST();
            stringArray = File.ReadAllLines(filename2);
            FileInsertion(filename2);
            check = false;
        }
        private void LoadJavaLibToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check = true;
            Array.Resize<string>(ref stringArray, 0);
            pt1 = new Trie();
            pt2 = new TST();
            pt3 = new BST();
            stringArray = File.ReadAllLines(filename4);
            FileInsertion(filename4);
            check = false;
        }
        private void AddWordToPythonLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (StringIsValid(richTextBox1.Text.ToLower()))
            {
                string newRecord = richTextBox1.Text.ToLower();
                using (StreamWriter sw = new StreamWriter(filename1, true))
                {
                    sw.WriteLine(newRecord);
                }
            }
            else { MessageBox.Show("Вводимая строка должна содержать только буквы латинского алфавита"); }
        }
        private void AddWordToCSharpLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (StringIsValid(richTextBox1.Text.ToLower()))
            {
                string newRecord = richTextBox1.Text;
                using (StreamWriter sw = new StreamWriter(filename3, true))
                {
                    sw.WriteLine(newRecord);
                }
            }
            else { MessageBox.Show("Вводимая строка должна содержать только буквы латинского алфавита"); }
        }
        private void AddWordToCLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (StringIsValid(richTextBox1.Text.ToLower()))
            {
                string newRecord = richTextBox1.Text;
                using (StreamWriter sw = new StreamWriter(filename2, true))
                {
                    sw.WriteLine(newRecord);
                }
            }
            else { MessageBox.Show("Вводимая строка должна содержать только буквы латинского алфавита"); }
        }
        private void AddWordToJavaLibraryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (StringIsValid(richTextBox1.Text.ToLower()))
            {
                string newRecord = richTextBox1.Text;
                using (StreamWriter sw = new StreamWriter(filename4, true))
                {
                    sw.WriteLine(newRecord);
                }
            }
            else { MessageBox.Show("Вводимая строка должна содержать только буквы латинского алфавита"); }
        }
        private void DeleteWordFromPythonLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text;
            using (StreamReader sr = new StreamReader(filename1)) //считываем файл в строку
            {
                text = sr.ReadToEnd();
            }
            List<string> list = (text.Split(new char[] { ' ', '\n', '_', '\t', '\r' }, StringSplitOptions.RemoveEmptyEntries)).Cast<string>().ToList(); //сплитим ее в массив
            list.Remove(richTextBox1.Text.Replace(" ", ""));
            using (StreamWriter sw = new StreamWriter(filename1, false))
            {
                foreach (string stroka in list)
                {
                    sw.WriteLine(stroka);
                }

            }
        }
        private void DeleteWordFromCSharpLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            string text;
            using (StreamReader sr = new StreamReader(filename3)) //считываем файл в строку
            {
                text = sr.ReadToEnd();
            }
            List<string> list = (text.Split(new char[] { ' ', '\n', '_', '\t', '\r' }, StringSplitOptions.RemoveEmptyEntries)).Cast<string>().ToList(); //сплитим ее в массив
            list.Remove(richTextBox1.Text.Replace(" ", ""));
            using (StreamWriter sw = new StreamWriter(filename3, false))
            {
                foreach (string str in list)
                {
                    sw.WriteLine(str);
                }

            }
        }
        private void DeleteWordFromCLibraryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string text;
            using (StreamReader sr = new StreamReader(filename2)) //считываем файл в строку
            {
                text = sr.ReadToEnd();
            }
            List<string> list = (text.Split(new char[] { ' ', '\n', '_', '\t', '\r' }, StringSplitOptions.RemoveEmptyEntries)).Cast<string>().ToList(); //сплитим ее в массив
            list.Remove(richTextBox1.Text.Replace(" ", ""));
            using (StreamWriter sw = new StreamWriter(filename2, false))
            {
                foreach (string str in list)
                {
                    sw.WriteLine(str);
                }
                
            }
        }
        private void DeleteWordFromJavaLibraryToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string text;
            using (StreamReader sr = new StreamReader(filename4)) //считываем файл в строку
            {
                text = sr.ReadToEnd();
            }
            List<string> list = (text.Split(new char[] { ' ', '\n', '_', '\t', '\r' }, StringSplitOptions.RemoveEmptyEntries)).Cast<string>().ToList(); //сплитим ее в массив
            list.Remove(richTextBox1.Text.Replace(" ", ""));
            using (StreamWriter sw = new StreamWriter(filename4, false))
            {
                foreach (string str in list)
                {
                    sw.WriteLine(str);
                }

            }
        }

        private void InfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEnumerable<string> result = stringArray.Distinct(); //удаляет одинаковые слова
            stringArray = result.ToArray();//превращает list в массив

            int orderNumber = 1; //указыват порядковый номер слова
            using (StreamWriter writer = new StreamWriter(filename10)) //создает или редактирует файл вывода
            {
                writer.WriteLine("Все ранее добавленные слова:");
                foreach (string stroka in stringArray) //пока есть слова в массиве
                {
                    writer.WriteLine(orderNumber + ". " + stroka); //добавляет строку
                    orderNumber++; //увеличивает порядковый номер на 1
                }
            }
            System.Diagnostics.Process.Start(filename10);//открывает файл
            Form ifrm = new Form2();
            ifrm.Show(); // отображаем Form2
        }
        private void TenThousandWordLibraryToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Array.Resize<string>(ref stringArray, 0); //уже описано в первом случае
            check = true;
            pt1 = new Trie();
            pt2 = new TST();
            pt3 = new BST();
            stringArray = File.ReadAllLines(filename6);
            FileInsertion(filename6);
            check = false;
        }
        private void HundredThousandWordLibraryToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Array.Resize<string>(ref stringArray, 0);
            check = true;
            pt1 = new Trie();
            pt2 = new TST();
            pt3 = new BST();
            stringArray = File.ReadAllLines(filename5);
            FileInsertion(filename5);
            check = false;
        }
        private void ThousandWordLibraryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            check = true;
            Array.Resize<string>(ref stringArray, 0);
            pt1 = new Trie();
            pt2 = new TST();
            pt3 = new BST();
            stringArray = File.ReadAllLines(filename7);
            FileInsertion(filename7);
            check = false;
        }
        private void HundredWordLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check = true;
            Array.Resize<string>(ref stringArray, 0);
            pt1 = new Trie();
            pt2 = new TST();
            pt3 = new BST();
            stringArray = File.ReadAllLines(filename8);
            FileInsertion(filename8);
            check = false;
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

    }
}
