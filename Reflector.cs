namespace Enigma
{
    class Reflector
    {
        private string reflector = null;
        public Reflector(int model)
        {
            switch(model)
            {
                case 0: // UKW B
                    reflector = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
                    break;
                case 1: // UKW C
                    reflector = "FVPJIAOYEDRZXWGCTKUQSBNMHL";
                    break;
                case 2: // UKW B THIN
                    reflector = "ENKQAUYWJICOPBLMDXZVFTHRGS";
                    break;
                case 3: // UKW C THIN
                    reflector = "RDOBJNTKVEHMLFCWZAXGYIPSUQ";
                    break;

                default:
                    break;
            }
        }

        public int Encrypt (int index)
        {
            if(index < 0 || index >= reflector.Length)
            {
                return -1;
            }

            return reflector[index] - 65;
        }
    }


}
