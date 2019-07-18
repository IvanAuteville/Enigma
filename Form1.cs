using System;
using System.Drawing;
using System.Windows.Forms;

namespace Enigma
{
    public partial class EnigmaForm : Form
    {
        private const int alphabet = 26;
        private Enigma enigma = null;

        private readonly Image[] enigmaImg;
        private readonly String[] reflectorsM3;
        private readonly String[] reflectorsM4;
        private readonly String[] rotors;
        private readonly String[] m4Rotors;
        private int[] rotorsPos;

        private readonly TextBox[] txtBoxPlugs;
        private int[] plugConnections;

        public EnigmaForm()
        {
            enigmaImg = new Image[2];
            enigmaImg[0] = Image.FromFile(System.IO.Path.Combine(Environment.CurrentDirectory, "img\\Enigma M3.jpg"));
            enigmaImg[1] = Image.FromFile(System.IO.Path.Combine(Environment.CurrentDirectory, "img\\Enigma M4.jpg"));

            reflectorsM3 = new String[2];
            reflectorsM4 = new String[2];
            reflectorsM3[0] = "UKW - B";
            reflectorsM3[1] = "UKW - C";

            reflectorsM4[0] = "UKW - B Thin";
            reflectorsM4[1] = "UKW - C Thin";

            rotors = new String[8];
            m4Rotors = new String[2];

            rotors[0] = "I";
            rotors[1] = "II";
            rotors[2] = "III";
            rotors[3] = "IV";
            rotors[4] = "V";
            rotors[5] = "VI";
            rotors[6] = "VII";
            rotors[7] = "VIII";

            m4Rotors[0] = "Beta";
            m4Rotors[1] = "Gamma";

            rotorsPos = new int[4];

            txtBoxPlugs = new TextBox[alphabet];
            plugConnections = new int[alphabet];

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            enigmaModelComboBox.SelectedIndex = 0;
            EnigmaPictureBox.Image = enigmaImg[0];

            wh4comboBox.Enabled = false;
            RS4textBox.Enabled = false;
            WH4textBox.Enabled = false;

            reflectorComboBox.DataSource = reflectorsM3;
            reflectorComboBox.SelectedIndex = 0;

            wh1comboBox.DataSource = rotors;

            wh2comboBox.BindingContext = new BindingContext();
            wh2comboBox.DataSource = rotors;

            wh3comboBox.BindingContext = new BindingContext();
            wh3comboBox.DataSource = rotors;

            wh4comboBox.BindingContext = new BindingContext();
            wh4comboBox.DataSource = m4Rotors;

            RS1textBox.TextChanged += SettingsTextChanged;
            RS2textBox.TextChanged += SettingsTextChanged;
            RS3textBox.TextChanged += SettingsTextChanged;
            RS4textBox.TextChanged += SettingsTextChanged;

            WH1textBox.TextChanged += SettingsTextChanged;
            WH2textBox.TextChanged += SettingsTextChanged;
            WH3textBox.TextChanged += SettingsTextChanged;
            WH4textBox.TextChanged += SettingsTextChanged;

            wh3comboBox.SelectedIndex = 0;
            wh2comboBox.SelectedIndex = 1;
            wh1comboBox.SelectedIndex = 2;

            // Link each Plugboard TextBox with txtBoxPlugs Array
            for (int i = 0; i < txtBoxPlugs.Length; i++)
            {
                txtBoxPlugs[i] = (TextBox)this.Controls["textBox" + (i < 10 ? "0" : "") + (i)];
                txtBoxPlugs[i].TextChanged += PlugboardTextChanged;

                // Set no connection
                plugConnections[i] = -1;
            }

            plainTextBox.ReadOnly = true;
            encryptedTextBox.ReadOnly = true;
        }

        private void EnigmaModelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = enigmaModelComboBox.SelectedIndex;
            EnigmaPictureBox.Image = enigmaImg[index];

            reflectorComboBox.DataSource = (index == 0 ? reflectorsM3 : reflectorsM4);
            wh4comboBox.Enabled = (index == 0 ? false : true);

            if (index == 0)
            {
                RS4textBox.Enabled = false;
                WH4textBox.Enabled = false;
            }
            else
            {
                RS4textBox.Enabled = true;
                WH4textBox.Enabled = true;
            }

            reflectorComboBox.SelectedIndex = 0;
        }

        private void ClearPlugsButton_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "";

            for (int i = 0; i < alphabet; i++)
            {
                plugConnections[i] = -1;
                txtBoxPlugs[i].Text = "";
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "";

            if (wh3comboBox.SelectedIndex != wh2comboBox.SelectedIndex && 
                wh3comboBox.SelectedIndex != wh1comboBox.SelectedIndex
                && wh2comboBox.SelectedIndex != wh1comboBox.SelectedIndex)
            {
                // Block controls
                SetControls(false);
                plainTextBox.Text = "";
                encryptedTextBox.Text = "";

                // Create Enigma
                enigma = new Enigma(enigmaModelComboBox.SelectedIndex, reflectorComboBox.SelectedIndex, 
                   wh4comboBox.SelectedIndex, wh3comboBox.SelectedIndex, wh2comboBox.SelectedIndex, wh1comboBox.SelectedIndex,
                   RS4textBox.Text[0], RS3textBox.Text[0], RS2textBox.Text[0], RS1textBox.Text[0], 
                   WH4textBox.Text[0], WH3textBox.Text[0], WH2textBox.Text[0], WH1textBox.Text[0],
                   ref plugConnections, ref rotorsPos);

                inputText.Focus();
            }
            else
            {
                groupBox1.Text = "Each Wheel must be unique!";
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            // Unblock controls
            SetControls(true);

            enigmaModelComboBox.Focus();
        }

        private void SetControls(bool setting)
        {
            startButton.Enabled = setting;
            stopButton.Enabled = !setting;
            clearPlugsButton.Enabled = setting;

            enigmaModelComboBox.Enabled = setting;
            reflectorComboBox.Enabled = setting;
            wh1comboBox.Enabled = setting;
            wh2comboBox.Enabled = setting;
            wh3comboBox.Enabled = setting;

            if (enigmaModelComboBox.SelectedIndex == 1)
            {
                wh4comboBox.Enabled = setting;
                RS4textBox.Enabled = setting;
                WH4textBox.Enabled = setting;
            }

            RS1textBox.Enabled = setting;
            WH1textBox.Enabled = setting;

            RS2textBox.Enabled = setting;
            WH2textBox.Enabled = setting;

            RS3textBox.Enabled = setting;
            WH3textBox.Enabled = setting;

            inputText.Enabled = !setting;

            for (int i = 0; i < alphabet; i++)
            {
                txtBoxPlugs[i].Enabled = setting;
            }
        }

        private void SettingsTextChanged(object sender, EventArgs e)
        {
            TextBox thisTextBox = (TextBox)sender;
            thisTextBox.TextChanged -= SettingsTextChanged;

            if (thisTextBox.Text == "")
            {
                thisTextBox.Text = "A";
            }
            else
            {
                thisTextBox.Text = thisTextBox.Text.Substring(0, 1).ToUpper();

                if (thisTextBox.Text.Length >= 1)
                {
                    char enteredChar = thisTextBox.Text[0];

                    if (enteredChar < 65 || enteredChar > 90)
                    {
                        thisTextBox.Text = "A";
                    }
                }
            }

            thisTextBox.TextChanged += SettingsTextChanged;
        }

        private void PlugboardTextChanged(object sender, EventArgs e)
        {
            TextBox thisTextBox = (TextBox)sender;
            thisTextBox.TextChanged -= PlugboardTextChanged;

            groupBox1.Text = "";

            int thisIndex = int.Parse(thisTextBox.Name.Substring(thisTextBox.Name.Length - 2));
            int thisChar = thisIndex + 65;

            // If the PlugText was erased
            if (thisTextBox.Text == "")
            {
                // And were connections made...
                if (plugConnections[thisIndex] != -1)
                {
                    // Get other End
                    int otherIndex = plugConnections[thisIndex];

                    // Erase Connections
                    plugConnections[thisIndex] = -1;
                    plugConnections[otherIndex] = -1;

                    // Set Char
                    txtBoxPlugs[otherIndex].Text = "";
                }// Else do nothing....
            }
            // A Letter is typed
            else
            {
                // Clean the string
                thisTextBox.Text = thisTextBox.Text.Substring(0, 1).ToUpper();

                // Exceptions free
                if (thisTextBox.Text.Length >= 1)
                {
                    // Get the Char typed
                    char enteredChar = thisTextBox.Text[0];

                    // If is invalid
                    if (enteredChar == thisChar || enteredChar < 65 || enteredChar > 90)
                    {
                        // If there wasn't a connection, clean txtBox
                        if (plugConnections[thisIndex] == -1)
                        {
                            thisTextBox.Text = "";
                        }
                        else // Restore the txtBox to its previous state
                        {
                            thisTextBox.Text = Convert.ToChar(plugConnections[thisIndex] + 65).ToString();
                        }
                    }
                    // Valid Char
                    else
                    {
                        // If there wasn't a connection...
                        if (plugConnections[thisIndex] == -1)
                        {
                            // Get other End
                            int otherIndex = enteredChar - 65;

                            // And the other end is free
                            if (plugConnections[otherIndex] == -1)
                            {
                                // Make connections
                                plugConnections[thisIndex] = otherIndex;
                                plugConnections[otherIndex] = thisIndex;

                                // Set Char
                                txtBoxPlugs[otherIndex].Text = Convert.ToChar(thisIndex + 65).ToString();
                            }
                            else // The other end is taken
                            {
                                // Show error message
                                thisTextBox.Text = "";
                                groupBox1.Text = "The Plug in position " + enteredChar + " is already in use.";
                            }
                        }
                        else // Restore the txtBox to its previous state
                        {
                            thisTextBox.Text = Convert.ToChar(plugConnections[thisIndex] + 65).ToString();
                        }
                    }
                }
            }

            thisTextBox.TextChanged += PlugboardTextChanged;
        }

        private void InputText_TextChanged(object sender, EventArgs e)
        {
            inputText.TextChanged -= InputText_TextChanged;

            if (inputText.Text != "")
            {
                char[] inputAux = inputText.Text.ToUpper().ToCharArray();
                inputText.Text = "";

                for (int i = 0; i < inputAux.Length; i++)
                {
                    char typedChar = inputAux[i];

                    if (typedChar < 65 || typedChar > 90)
                    {
                        inputAux[i] = ' ';
                    }
                }

                string strAux = new string(inputAux);
                strAux = strAux.Replace(" ", "");

                inputAux = strAux.ToCharArray();
                plainTextBox.Text += strAux;

                
                if (inputAux.Length >= 1)
                {
                    for (int i = 0; i < inputAux.Length; i++)
                    {
                        char typedChar = inputAux[i];

                        // Encrypt (send enteredChar - receive encryptedChar)
                        char encryptedChar = enigma.Encrypt(typedChar);

                        encryptedTextBox.Text += encryptedChar;
                    }

                    // Refresh Rotors Position
                    WH1textBox.Text = Convert.ToChar(rotorsPos[0] + 65).ToString();
                    WH2textBox.Text = Convert.ToChar(rotorsPos[1] + 65).ToString();
                    WH3textBox.Text = Convert.ToChar(rotorsPos[2] + 65).ToString();
                    WH4textBox.Text = Convert.ToChar(rotorsPos[3] + 65).ToString();
                }
            }

            inputText.TextChanged += InputText_TextChanged;
        }
    }

        
}
