namespace BlazorWjdr.Models
{
    public class ProfilDto
    {
        public int Id { get; init; }
        public int Cc { get; init; }
        public int Ct { get; init; }
        public int F { get; init; }
        public int E { get; init; }
        public int I { get; init; }
        public int Ag { get; init; }
        public int Dex { get; init; }
        public int Int { get; init; }
        public int Fm { get; init; }
        public int Soc { get; init; }
        public int Bf => F / 10;
        public int Be => E / 10;
        public int Bfm => Fm / 10;
    }
}
