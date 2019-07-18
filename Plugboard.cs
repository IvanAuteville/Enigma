namespace Enigma
{
    class Plugboard
    {
        private int[] connections;

        public Plugboard(ref int[] plugs)
        {
            connections = plugs;
        }

        internal int Encrypt(int index)
        {
            if (index < 0 || index >= connections.Length)
            {
                return -1;
            }

            else if(connections[index] == -1)
            {
                return index;
            }

            else
            {
                return connections[index];
            }
        }
    }
}
