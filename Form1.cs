using com.calitha.goldparser;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Calculadora
{
    public partial class Form1 : Form
    {
        MyParser parser;
        private string memoria = ""; 

        public Form1()
        {
            parser = new MyParser(Application.StartupPath + "\\Gramatica.cgt");
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Btn_MemoryRecall.Visible = false;
        }

        #region PROPIEDADES DE BOTONES
        private void Btn_Cero_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += "0";
        }

        private void Btn_Uno_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += "1";
        }

        private void Btn_Dos_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += "2";
        }

        private void Btn_Tres_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += "3";
        }

        private void Btn_Cuatro_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += "4";
        }

        private void Btn_Cinco_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += "5";
        }

        private void Btn_Seis_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += "6";
        }

        private void Btn_Siete_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += "7";
        }

        private void Btn_Ocho_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += "8";
        }

        private void Btn_Nueve_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += "9";
        }
        private void Btn_Punto_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += ".";
        }

        private void Btn_Suma_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += "+";
        }

        private void Btn_Resta_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += "-";
        }

        private void Btn_Multi_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += "*";
        }

        private void Btn_Division_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += "/";
        }

        private void Btn_ParIzquierda_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += "(";
        }
        private void Btn_ParDerecha_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += ")";
        }

        private void Btn_Raiz_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += "sqrt(";
            BtnComa.BackColor = System.Drawing.ColorTranslator.FromHtml("#019688");
            BtnComa.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

        }

        private void Btn_Tan_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += "tan(";
        }

        private void Btn_PI_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += "3.14159";
        }

        private void BtnLimpiarTodo_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Clear();
            textBoxResultado.Clear();
            BtnComa.BackColor = System.Drawing.ColorTranslator.FromHtml("#283736");
            BtnComa.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        }

        private void Btn_Retroceder_Click(object sender, EventArgs e)
        {
            if (TextBoxOperacion.Text.Length > 0)
            {
                TextBoxOperacion.Text = TextBoxOperacion.Text.Remove(TextBoxOperacion.Text.Length - 1);
            }
        }

        private void BtnComa_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text += ",";
            BtnComa.BackColor = System.Drawing.ColorTranslator.FromHtml("#283736");
            BtnComa.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

        }

        private void Btn_Igual_Click(object sender, EventArgs e)
        {
            parser.Parse(TextBoxOperacion.Text);
            textBoxResultado.Text = parser.resultado;
            //Console.WriteLine(parser.resultado);
        }

        #endregion

        #region BOTONESMEMORIA
        //BOTON PARA RECUPERAR NUMERO EN LA MEMORIA SOLO ES VISIBLE CUANDO HAY DATOS GUARDADO
        private void customBtn3_Click(object sender, EventArgs e)
        {
            TextBoxOperacion.Text = memoria;
        }

        //BOTON PARA GUARDAR NUMERO EN LA MEMORIA
        private void Btn_MemoryStorage_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxOperacion.Text))
            {
                memoria = TextBoxOperacion.Text;
                Btn_MemoryRecall.Visible = true;
                Btn_MemoryStorage.BackColor = System.Drawing.ColorTranslator.FromHtml("#019789");
            }
        }
        //BOTON PARA LIMPIAR LA MEMORIA
        private void Btn_MemoryClear_Click(object sender, EventArgs e)
        {
            memoria = "";
            Btn_MemoryRecall.Visible = false;
            Btn_MemoryStorage.BackColor = System.Drawing.ColorTranslator.FromHtml("#73C6B6");
        }
        #endregion

    }
}
