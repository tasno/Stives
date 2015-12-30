namespace StIvesLib.Meeples.Wives
{
    class Seamstress : Wife
    {
        public override int Dowry
        {
            get { return 3; }
        }

        public override string Name
        {
            get { return "Seamstress"; }
        }

        public override int MaxSacks
        {
            get
            {
                return 2;
            }
        }
    }
}
