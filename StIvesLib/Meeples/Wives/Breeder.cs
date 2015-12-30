namespace StIvesLib.Meeples.Wives
{
    class Breeder : Wife
    {
        public override int Dowry
        {
            get { return 5; }
        }

        public override string Name
        {
            get { return "Breeder"; }
        }

        public override int MaxKittens
        {
            get
            {
                return 2;
            }
        }
    }
}
