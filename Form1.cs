using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator_App
{
    public partial class Form1 : Form
    {

        clsProccess Proccess;

        public Form1()
        {
            InitializeComponent();
            Proccess = new clsProccess(lblResult, lblSubResult);
        }

        class clsProccess
        {
            private double? _Number1 = null;
            private double? _Number2 = null;
            private string _Operation = null;
            private string _FullNumber = "";
            private bool _RestartCheck = false;

            private Label _lblResult = null;
            private Label _lblSubResult = null;


            public clsProccess(Label lblResult, Label lblSubResult)
            {
                _lblResult = lblResult;
                _lblSubResult = lblSubResult;
            }

            public void SetValue(Button btn)
            {
                if (_lblResult.Text == "00" || _lblResult.Text == "0")
                {
                    _lblResult.Text = "";
                    _Number1 = 0;

                }

                if (_Operation == null)
                {
                    SetNum1Value(btn);
                }
                else
                    SetNum2Value(btn);
            }

            void SetNum1Value(Button btn)
            {
                if (_RestartCheck)
                {
                    _Number1 = null;
                    _Number2 = null;
                    _Operation = null;
                    _FullNumber = "";

                    _lblSubResult.Text = null;
                    _lblResult.Text = null;

                    _RestartCheck = false;
                }

                if (btn.Tag == null)
                    btn.Tag = 0;

                if (_lblResult.Text == "AHHHHH")
                    _lblResult.Text = null;

                if (btn.Tag.ToString() == "." && _FullNumber.Contains("."))
                    return;

                _FullNumber += btn.Tag;

                if (_FullNumber == ".")
                    _FullNumber = "0.";

                if (!string.IsNullOrEmpty(_FullNumber) && double.TryParse(_FullNumber, out double parsedNumber1))
                    _Number1 = parsedNumber1;

                _lblResult.Text += btn.Tag.ToString();
            }

            private void SetNum2Value(Button btn)
            {
                if (btn.Tag == null)
                    btn.Tag = 0;

                if (btn.Tag.ToString() == "." && _FullNumber.Contains("."))
                    return;

                _FullNumber += btn.Tag;

                if (_FullNumber == ".")
                    _FullNumber = "0.";

                if (!string.IsNullOrEmpty(_FullNumber) && double.TryParse(_FullNumber, out double parsedNumber2))
                    _Number2 = parsedNumber2;

                _lblResult.Text += btn.Tag.ToString();
            }

            public void SetOperation(Button btn)
            {
                if (_Number1.HasValue)
                {
                    _Operation = btn.Tag.ToString();

                    _lblSubResult.Text = _Number1.ToString() + " " + _Operation;

                    _FullNumber = "";
                    _lblResult.Text = null;
                }
                else
                    MessageBox.Show("Insert Number First..", "Error!"
                        , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            public void GetFinalResult()
            {
                double? result = 0;
                switch (_Operation)
                {
                    case "+":
                        result = _Number1 + _Number2;
                        break;

                    case "-":
                        result = _Number1 - _Number2;
                        break;

                    case "×":
                        result = _Number1 * _Number2;
                        break;

                    case "/":
                        if (_Number2 == 0)
                        {
                            _lblResult.Text = "AHHHHH";
                            _RestartCheck = true;
                            _Operation = null;
                            _lblSubResult.Text = null;
                            return;
                        }
                        else
                            result = _Number1 / _Number2;
                        break;

                    default:
                        break;
                }
                _RestartCheck = true;
                _Number1 = result;
                _Operation = null;
                _lblResult.Text = result.ToString();
                _lblSubResult.Text = null;
            }


            public void Reset()
            {
                _Number1 = 0;
                _Number2 = null;
                _Operation = null;
                _FullNumber = "";

                _lblSubResult.Text = null;
                _lblResult.Text = "00";
            }

            public void Delete()
            {
                if (!string.IsNullOrEmpty(_FullNumber))
                {
                    if (_Operation == null)
                        _FullNumber = _Number1.ToString();
                    else
                        _FullNumber = _Number2.ToString();

                    _FullNumber = _lblResult.Text;

                    if (_FullNumber.EndsWith("."))
                        _FullNumber = _FullNumber.Replace(".", "");

                    else
                        _FullNumber = _FullNumber.Remove(_FullNumber.Length - 1);

                    if (string.IsNullOrEmpty(_FullNumber))
                    {
                        _lblResult.Text = "0";
                        _FullNumber = "0";
                    }

                    if (_Operation == null)
                    {
                        if (_FullNumber == "-" || _FullNumber == "." || _lblResult.Text == "AHHHHH")
                            _FullNumber = "0";

                        _Number1 = double.Parse(_FullNumber);
                    }
                    else
                        _Number2 = double.Parse(_FullNumber);

                    _lblResult.Text = _FullNumber;
                }
            }

            public void Start()
            {
                _Number1 = 0;
            }
        };


        private void btnNum_Click(object sender, EventArgs e)
        {
            Proccess.SetValue((Button)sender);
        }

        private void btnOperation_Click(object sender, EventArgs e)
        {
            Proccess.SetOperation((Button)sender);
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (lblSubResult.Text == "")
                return;
            Proccess.GetFinalResult();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Proccess.Reset();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            Proccess.Delete();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Proccess.Start();
        }

        private void btnSwitchTheme_Click(object sender, EventArgs e)
        {
            if (btnSwitchTheme.Text == "Switch To Light Mode")
            {
                this.BackColor = Color.FromArgb(230, 230, 230);
                lblResult.BackColor = Color.FromArgb(237, 237, 237);
                pictureBox1.BackColor = Color.FromArgb(208, 204, 203);
                label1.ForeColor = Color.FromArgb(53, 53, 41);
                lblResult.ForeColor = Color.FromArgb(53, 53, 41);
                lblSubResult.ForeColor = Color.FromArgb(53, 53, 41);
                lblSubResult.BackColor = Color.FromArgb(237, 237, 237);
                btnDel.BackColor = Color.FromArgb(54, 127, 134);
                btnReset.BackColor = Color.FromArgb(54, 127, 134);
                btnSwitchTheme.BackColor = Color.FromArgb(59, 70, 100);
                btnSwitchTheme.ForeColor = Color.FromArgb(230, 230, 230);
                btnSwitchTheme.Text = "Switch To Dark Mode";
            }
            else if (btnSwitchTheme.Text == "Switch To Dark Mode")
            {
                this.BackColor = Color.FromArgb(25, 25, 30);
                lblResult.BackColor = Color.FromArgb(35, 35, 40);
                lblSubResult.BackColor = Color.FromArgb(35, 35, 40);
                pictureBox1.BackColor = Color.FromArgb(45, 45, 50);
                label1.ForeColor = Color.FromArgb(255, 255, 255);
                lblResult.ForeColor = Color.FromArgb(255, 255, 255);
                lblSubResult.ForeColor = Color.FromArgb(210, 210, 210);
                btnDel.BackColor = Color.FromArgb(123, 104, 238);
                btnReset.BackColor = Color.FromArgb(123, 104, 238);
                btnSwitchTheme.BackColor = Color.FromArgb(233, 227, 220);
                btnSwitchTheme.ForeColor = Color.FromArgb(40, 40, 45);
                btnSwitchTheme.Text = "Switch To Light Mode";
            }
        }
    }
}
