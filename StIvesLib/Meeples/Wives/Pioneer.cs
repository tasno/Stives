namespace StIvesLib.Meeples.Wives
{
    class Pioneer : Wife
    {
        public override string Name { get { return "Pioneer"; } }
        public override bool CanWork { get { return true; } }
        public override int Dowry { get { return 0; } }
        public override int DefaultBillet { get { return 1; } }
    }
}
