using Environment.Hex;

namespace Units.Possibilities.View
{
    public interface IViewable
    {
        int ViewRadius { get; set; }

        void DispelWarFog(Hex hex);
    }
}