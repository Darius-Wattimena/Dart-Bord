using System;
using System.Drawing;
using System.Windows.Forms;

namespace Dart_Score_Bord
{
    public partial class Form1 : Form
    {
        DartsGame dartGame = new DartsGame();
        public Form1()
        {
            InitializeComponent();
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            textBox5.ForeColor = Color.Green;
            textBox1.ForeColor = Color.Green;
            label1.ForeColor = Color.Green;
            label3.ForeColor = Color.Green;
            textBox6.ForeColor = Color.Red;
            textBox2.ForeColor = Color.Red;
            label2.ForeColor = Color.Red;
            label4.ForeColor = Color.Red;
            label11.Text = @"Can not finish";
            label12.Text = @"Can not finish";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dartGame.GetDartThrow(comboBox1.SelectedItem, comboBox2.SelectedItem);
            CheckDisabledButtons(1);
            SetThrowedScoreTextBox(1);
            CheckTotalScoreTextBox(1);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            dartGame.GetDartThrow(comboBox4.SelectedItem, comboBox3.SelectedItem);
            CheckDisabledButtons(2);
            SetThrowedScoreTextBox(2);
            CheckTotalScoreTextBox(2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dartGame.GetDartThrow(0, "Bull");
            CheckDisabledButtons(1);
            SetThrowedScoreTextBox(1);
            CheckTotalScoreTextBox(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dartGame.GetDartThrow(0, "Bullseye");
            CheckDisabledButtons(1);
            SetThrowedScoreTextBox(1);
            CheckTotalScoreTextBox(1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dartGame.GetDartThrow(0, "Bull");
            CheckDisabledButtons(2);
            SetThrowedScoreTextBox(2);
            CheckTotalScoreTextBox(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dartGame.GetDartThrow(0, "Bullseye");
            CheckDisabledButtons(2);
            SetThrowedScoreTextBox(2);
            CheckTotalScoreTextBox(2);
        }

        public void CheckTotalScoreTextBox(int team)
        {
            ChangeText(1);
            ChangeText(2);
        }

        public void SetThrowedScoreTextBox(int team)
        {
            if (team == 1)
            {
                label11.Text = dartGame.P1Checkout;
                label12.Text = dartGame.P2Checkout;
                textBox1.Text = dartGame.ValueToString(dartGame.P1ThrowedScore);
                label5.Text = "Darts left " + dartGame.Darts;
                label6.Text = "";
            }
            else
            {
                label11.Text = dartGame.P1Checkout;
                label12.Text = dartGame.P2Checkout;
                textBox2.Text = dartGame.ValueToString(dartGame.P2ThrowedScore);
                label5.Text = "";
                label6.Text = "Darts left " + dartGame.Darts;
            }
                
        }

        public void CheckDisabledButtons(int turn)
        {
            if (dartGame.Switched)
            {
                DisableButtons();
                dartGame.Switched = false;
            }
        }

        public void DisableButtons()
        {
            if (dartGame.Playerturn == 1)
            {
                label11.Text = dartGame.P1Checkout;
                label12.Text = dartGame.P2Checkout;
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                textBox5.ForeColor = Color.Green;
                textBox1.ForeColor = Color.Green;
                label1.ForeColor = Color.Green;
                label3.ForeColor = Color.Green;
                label5.ForeColor = Color.Green;
                label6.ForeColor = Color.Red;
                textBox6.ForeColor = Color.Red;
                textBox2.ForeColor = Color.Red;
                label2.ForeColor = Color.Red;
                label4.ForeColor = Color.Red;
            }
            else
            {
                label11.Text = dartGame.P1Checkout;
                label12.Text = dartGame.P2Checkout;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                textBox5.ForeColor = Color.Red;
                textBox1.ForeColor = Color.Red;
                label1.ForeColor = Color.Red;
                label3.ForeColor = Color.Red;
                label5.ForeColor = Color.Red;
                label6.ForeColor = Color.Green;
                textBox6.ForeColor = Color.Green;
                textBox2.ForeColor = Color.Green;
                label2.ForeColor = Color.Green;
                label4.ForeColor = Color.Green;
            }
        }

        public void ChangeText(int team)
        {
            if (team == 1)
            {
                textBox5.Text = dartGame.ValueToString(dartGame.P1TotalScore);
                label7.Text = "Legs " + dartGame.ValueToString(dartGame.P1Legs);
                label8.Text = "Sets " + dartGame.ValueToString(dartGame.P1Sets);
            }
            else
            {
                textBox6.Text = dartGame.ValueToString(dartGame.P2TotalScore);
                label9.Text = "Legs " + dartGame.ValueToString(dartGame.P2Legs);
                label10.Text = "Sets " + dartGame.ValueToString(dartGame.P2Sets);
            }
        }
    }
}
