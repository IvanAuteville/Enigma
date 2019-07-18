using System;

namespace Enigma
{
    class Enigma
    {
        private readonly int enigmaModel = 0;
        private readonly Reflector reflector = null;
        private readonly Plugboard plugboard = null;
        private readonly RotorM4 rotor4 = null;
        private readonly RotorM3 rotor3 = null;
        private readonly RotorM3 rotor2 = null;
        private readonly RotorM3 rotor1 = null;
        private readonly int[] rotorsPos;

        public Enigma
            (int enigmaModel, int reflectorType,
            int wh4, int wh3, int wh2, int wh1,
            char rs4, char rs3, char rs2, char rs1,
            char ws4, char ws3, char ws2, char ws1,
            ref int[] plugs, ref int[] rotorsPos
            )
        {
            this.enigmaModel = enigmaModel;

            // Rotors
            rotor4 = new RotorM4(wh4, rs4, ws4);
            rotor3 = new RotorM3(wh3, rs3, ws3);
            rotor2 = new RotorM3(wh2, rs2, ws2);
            rotor1 = new RotorM3(wh1, rs1, ws1);

            // Reflector
            reflectorType += (enigmaModel == 1 ? 2 : 0);
            reflector = new Reflector(reflectorType);

            // Plugboard
            plugboard = new Plugboard(ref plugs);

            this.rotorsPos = rotorsPos;
        }

        internal char Encrypt(int typedChar)
        {
            // Numeric representation
            int encryptedChar = typedChar - 65;

            // Input through Plugboard
            encryptedChar = plugboard.Encrypt(encryptedChar);

            // Rotate
            bool rotateNext = rotor1.Rotate();
            
            if(rotateNext)
            {
                rotateNext = rotor2.Rotate();
            }

            if (rotateNext)
            {
                rotor3.Rotate();
            }

            SetRotorsPos();

            // Direct Encryption
            encryptedChar = rotor1.DirectEncryption(encryptedChar);
            encryptedChar = rotor2.DirectEncryption(encryptedChar);
            encryptedChar = rotor3.DirectEncryption(encryptedChar);

            if (enigmaModel == 1)
            {
                encryptedChar = rotor4.DirectEncryption(encryptedChar);
            }

            // Pass through Reflector
            encryptedChar = reflector.Encrypt(encryptedChar);

            // Inverse Encryption
            if (enigmaModel == 1)
            {
                encryptedChar = rotor4.InverseEncryption(encryptedChar);
            }

            encryptedChar = rotor3.InverseEncryption(encryptedChar);
            encryptedChar = rotor2.InverseEncryption(encryptedChar);
            encryptedChar = rotor1.InverseEncryption(encryptedChar);

            // Output through Plugboard
            encryptedChar = plugboard.Encrypt(encryptedChar);

            return (Convert.ToChar(encryptedChar + 65));
        }

        private void SetRotorsPos()
        {
            rotorsPos[0] = rotor1.GetRotorPos();
            rotorsPos[1] = rotor2.GetRotorPos();
            rotorsPos[2] = rotor3.GetRotorPos();
            rotorsPos[3] = rotor4.GetRotorPos();
        }
    }
}
