using System;

namespace Enigma
{
    abstract class Rotor
    {
        internal int rotorPos = 0;
        internal int ringPos = 0;
        internal string wiring = null;
        private int Substitution(int index)
        {
            if (index < 0 || index >= wiring.Length)
            {
                return -1;
            }

            return (wiring[index] - 65);
        }

        private int InverseSubstitution(int character)
        {
            character += 65;

            return (wiring.IndexOf(Convert.ToChar(character)));
        }

        public int DirectEncryption(int input)
        {
            int aux = (input + rotorPos - ringPos + 26) % 26;

            aux = Substitution(aux);

            aux = (aux - rotorPos + ringPos + 26) % 26;

            return aux;
        }

        public int InverseEncryption(int input)
        {
            int aux = (input + rotorPos - ringPos + 26) % 26;

            aux = InverseSubstitution(aux);

            aux = (aux - rotorPos + ringPos + 26) % 26;

            return aux;
        }

        public int GetRotorPos()
        {
            return rotorPos;
        }
    }
}
