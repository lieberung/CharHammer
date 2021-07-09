using BlazorWjdr.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components.Profil
{
    public partial class ProfilRowValues
    {
        [Parameter]
        public ProfilDto Profil { get; set; } = null!;
    }
}
