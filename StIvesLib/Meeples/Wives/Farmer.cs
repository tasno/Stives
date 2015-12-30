namespace StIvesLib.Meeples.Wives
{
    class Farmer : Wife
    {
        public override int Dowry
        {
            get { return 1; }
        }

        public override string Name
        {
            get { return "Farmer"; }
        }

        public override int FarmingIncome
        {
            get
            {
                return 9;
            }
        }
    }
}
