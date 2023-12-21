namespace CharHammer.Services;

public class CarrieresService(IReadOnlyDictionary<int, CarriereDto> data)
{
  public IEnumerable<CarriereDto> AllCarrieres { get; } = data.Values.OrderBy(t => t.Nom).ToArray();

  //private IEnumerable<CarriereDto> GetCarrieres(IEnumerable<int> ids) => ids.Select(GetCarriere);

  public CarriereDto GetCarriere(int id) => data[id];

  //public IEnumerable<CarriereDto> GetCarrieresProposant(AptitudeDto aptitude)
  //    => AllCarrieres.Where(c => c.AptitudesPourScore.Any(comp => comp.Id == aptitude.Id));

  public IEnumerable<CarriereDto> GetCarrieresParuesDans(ReferenceDto reference)
      => AllCarrieres.Where(c => c.Source?.Book == reference);

  public IEnumerable<CarriereDto> Recherche(string searchText)
  {
    searchText = GenericService.NettoyerPourRecherche(searchText);
    var motsClefRecherches = GenericService.MotsClefsDeRecherche(searchText);

    return AllCarrieres
        .Where(c => GenericService.NettoyerPourRecherche(c.Nom).Contains(searchText)
                    || c.MotsClefDeRecherche.Intersect(motsClefRecherches).Any())
        .OrderByDescending(c => c.MotsClefDeRecherche.Intersect(motsClefRecherches).Count());
  }

  public IEnumerable<int> CarrieresSkaven => AllCarrieres.Where(c => c.Source?.Book.Id == 17).Select(c => c.Id);
  //public IEnumerable<CarriereDto> CarrieresDeBretonnie => AllCarrieres.Where(c => c.Source?.Book.Id is 15 or 16);
  //public IEnumerable<CarriereDto> CarrieresDuKislev => AllCarrieres.Where(c => c.Id == 53 || c.Source?.Book.Id == 14);

  //// Tueur de morts, Fouet de dieu, Flagellant, Fanatique, Pénitent, Prêcheur de rue, Tueur de démons/géants/trolls, Exécuteur, Mystique, Cénobite
  //public IEnumerable<CarriereDto> CarrieresFanatiques => GetCarrieres([ 209, 213, 170, 45, 212, 88, 198, 199, 77, 87, 268, 267, 194, 1, 314 ]);
  //public IEnumerable<CarriereDto> CarrieresDeNorsca => GetCarrieres([ 26, 89, 302, 303, 90, 91, 92, 93, 94, 95, 304 ]);
  public static IEnumerable<int> CarrieresChaos => [305, 307, 309, 311, 306, 308, 310, 312, 293, 294];
  //public IEnumerable<CarriereDto> CarrieresCriminelles => GetCarrieres([ 21, 141, 149, 151, 152, 158, 37, 38, 165, 43, 168, 280, 111, 51, 265, 183, 127, 145, 295, 66, 188, 191, 192, 193, 258, 76, 80 ]);
  //public IEnumerable<CarriereDto> CarrieresBureaucratie => GetCarrieres([ 315, 251, 35, 86, 87, 97, 125, 46, 129, 49, 175, 281, 177, 58, 59, 63, 128, 146, 147, 69, 70 ]);
  //public IEnumerable<CarriereDto> CarrieresMilitaires => GetCarrieres([ 296, 316, 297, 154, 155, 319, 156, 161, 252, 162, 253, 254, 286, 36, 274, 40, 41, 301, 171, 46, 129, 48, 203, 112, 256, 53, 54, 185, 187, 197, 73, 259 ]);


  public CarriereDto CarriereInitie => GetCarriere(52);
  public CarriereDto CarriereNovice => GetCarriere(133);
  public CarriereDto CarrierePretre => GetCarriere(189);
  public CarriereDto CarrierePretreConsacre => GetCarriere(190);
  public CarriereDto CarriereGrandPretre => GetCarriere(172);

  public CarriereDto CarriereFanatique => GetCarriere(45);
  public CarriereDto CarriereFlagellant => GetCarriere(170);
  public CarriereDto CarriereFouetDeDieu => GetCarriere(213);
  public CarriereDto CarriereHerautDesCata => GetCarriere(666);

  //public IEnumerable<CarriereDto> CarrieresDevotes
  //{
  //  get
  //  {
  //    var list = [CarriereInitie, CarrierePretre, CarriereGrandPretre, CarrierePretreConsacre, CarriereChevalierDeLEmpire];
  //    list.AddRange(AllCarrieres.Where(c => c.Source?.Book.Id == 13 || c.Parent is not null && list.Contains(c.Parent)));
  //    list.AddRange([CarriereFanatique, CarriereFlagellant, CarriereAnachorete, CarriereMystique, CarriereExorciste, CarriereLayPriest, CarrierePrelat]);
  //    return list;
  //  }
  //}
}
