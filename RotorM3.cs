namespace Enigma
{
    class RotorM3 : Rotor
    {
        private readonly int[] turnoverPos;
        public RotorM3(int model, char ringPos, char rotorPos)
        {
            if(model > 4) 
            {
                turnoverPos = new int[2];
                turnoverPos[0] = 25;
                turnoverPos[1] = 12;
            }
            else
            {
                turnoverPos = new int[1];
            }

            switch (model)
            {
                case 0: // I
                    wiring = "EKMFLGDQVZNTOWYHXUSPAIBRCJ";
                    turnoverPos[0] = 16;
                    break;
                case 1: // II
                    wiring = "AJDKSIRUXBLHWTMCQGZNPYFVOE";
                    turnoverPos[0] = 4;
                    break;
                case 2: // III
                    wiring = "BDFHJLCPRTXVZNYEIWGAKMUSQO";
                    turnoverPos[0] = 21;
                    break;
                case 3: // IV
                    wiring = "ESOVPZJAYQUIRHXLNFTGKDCMWB";
                    turnoverPos[0] = 9;
                    break;
                case 4: // V
                    wiring = "VZBRGITYUPSDNHLXAWMJQOFECK";
                    turnoverPos[0] = 25;
                    break;

                case 5: // VI
                    wiring = "JPGVOUMFYQBENHZRDKASXLICTW";
                    break;
                case 6: // VII
                    wiring = "NZJHGRCXMYSWBOUFAIVLPEKQDT";
                    break;
                case 7: // VIII
                    wiring = "FKQHTLXOCBJSPDZRAMEWNIUYGV";
                    break;

                default:
                    break;
            }

            this.rotorPos = rotorPos - 65;
            this.ringPos = ringPos - 65;
        }

        public bool Rotate()
        {
            bool rotateNext = false;

            for (int i = 0; i < turnoverPos.Length; i++)
            {
                if(turnoverPos[i] == rotorPos)
                {
                    rotateNext = true;
                    break;
                }
            }

            rotorPos = (rotorPos + 1) % 26;

            return rotateNext;
        }
    }
}
