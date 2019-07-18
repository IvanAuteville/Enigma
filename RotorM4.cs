namespace Enigma
{
    class RotorM4: Rotor
    {
        public RotorM4(int model, char ringPos, char rotorPos)
        {
            if(model == 0) // Beta
            {
                this.wiring = "LEYJVCNIXWPBQMDRTAKZGFUHOS";
            }
            else // Gamma
            {
                this.wiring = "FSOKANUERHMBTIYCWLQPZXVGJD";
            }

            this.rotorPos = rotorPos - 65;
            this.ringPos = ringPos - 65;
        }
    }
}
