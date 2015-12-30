namespace StIvesLib.Meeples.Wives
{
    class Matchmaker : Wife
    {
        public override int Dowry
        {
            get { return 12; }
        }

        public override string Name
        {
            get { return "Matchmaker"; }
        }

        public override bool MayMarry
        {
            get
            {
                return true;
            }
        }
    }
}
