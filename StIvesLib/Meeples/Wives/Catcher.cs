namespace StIvesLib.Meeples.Wives
{
    class Catcher : Wife
    {
        public override int Dowry
        {
            get { return 4; }
        }

        public override string Name
        {
            get { return "Catcher"; }
        }

        public override int MaxCats
        {
            get
            {
                return 2;
            }
        }
    }
}
