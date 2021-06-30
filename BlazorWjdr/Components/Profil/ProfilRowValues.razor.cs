using BlazorWjdr.DomainModel;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components.Profil
{
    public partial class ProfilRowValues
    {
        [Parameter]
        public string LeftTitle { get; set; } = "";
        [Parameter]
        public ProfilDto Profil { get; set; } = null!;
    }
}
