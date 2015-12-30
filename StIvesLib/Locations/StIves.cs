namespace StIvesLib.Locations
{
    class StIves : Location
    {
        public StIves()
            : base("St. Ives")
        {
        }
        public override bool HasChurch
        {
            get
            {
                return false;
            }
        }
    }
}
