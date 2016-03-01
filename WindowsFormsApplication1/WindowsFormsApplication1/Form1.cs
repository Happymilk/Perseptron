using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private MyFunction[] funcs;
        private Persiptron p;

        public Form1()
        {
            InitializeComponent();
        }

        private static MyObject[][] RandomObjects(int classCount, int objectsCount, int attributesCount)
        {
            var rnd = new Random();
            var objects = new MyObject[classCount][];

            for (int i = 0; i < classCount; i++)
            {
                objects[i] = new MyObject[objectsCount];
                for (int j = 0; j < objectsCount; j++)
                {
                    objects[i][j] = new MyObject(attributesCount + 1);
                    for (int k = 0; k < attributesCount; k++)
                        objects[i][j].Attributes[k] = rnd.Next(-9, 9);
                    objects[i][j].Attributes[attributesCount] = 1;
                }
            }
            return objects;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var classCount = (int)numericUpDown1.Value;
            var objectsCount = (int)numericUpDown2.Value;
            var attributesCount = (int)numericUpDown3.Value;

            dataGridView1.ColumnCount = attributesCount;
            dataGridView1.RowCount = 1;
         
            p = new Persiptron(classCount, attributesCount+1);
            MyObject[][] objects = RandomObjects(classCount, objectsCount, attributesCount);
            funcs = p.GetFuncs(objects);

            textBox1.Text = "";
            textBox2.Text = "";

            for (int i = 0; i < classCount; i++)
            {
                textBox1.Text += (i + 1) + ":";
                for (int j = 0; j < objectsCount; j++)
                {
                    textBox1.Text += "\t";
                    for (int k = 0; k < attributesCount; k++)
                        if (objects[i][j].Attributes[k] >= 0)
                            textBox1.Text += " " + objects[i][j].Attributes[k] + " ";
                        else
                            textBox1.Text += objects[i][j].Attributes[k] + " ";
                    textBox1.Text += "\r\n";
                }
            }

            for (int i = 0; i < classCount; i++)
            {
                textBox2.Text += "d(" + (i + 1) + ")= ";
                for (int j = 0; j < attributesCount; j++)
                {
                    if (j != 0 && funcs[i].Attributes[j] > 0)
                        textBox2.Text += "+";
                    textBox2.Text += funcs[i].Attributes[j] + "*x" + (j + 1) + " ";
                }
                if (funcs[i].Attributes[attributesCount] >= 0)
                    textBox2.Text += "+";
                textBox2.Text += funcs[i].Attributes[attributesCount];
                textBox2.Text += "\r\n";
            }
        }

        private MyObject Input()
        {
            var array = new MyObject((int)numericUpDown3.Value+1);
            
            for (int i = 0; i < (int)numericUpDown3.Value; i++)
                array.Attributes[i] = Int32.Parse((dataGridView1[i, 0].Value).ToString());
            array.Attributes[(int)numericUpDown3.Value] = 1;

            return array;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (p.Max(funcs, Input()) > -1)
                MessageBox.Show("Вектор " + (p.Max(funcs, Input()) + 1) + " класса");
        }
    }
}
