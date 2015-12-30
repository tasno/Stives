namespace StIvesLib.Meeples.Wives
{
    class Shooter : Wife
    {
        public override int Dowry { get { return 6; } }
        public override string Name { get { return "Shooter"; } }
        public override bool ShootsFirst { get { return true; } }
    }
}
