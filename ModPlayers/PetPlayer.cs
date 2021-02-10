using Terraria.ModLoader;
using TrailEffects.Pets.FloatingBonboriPet;

namespace TrailEffects.ModPlayers
{
    /// <summary>
    ///     Only used to manage the <see cref="FloatingBonbori" />.
    /// </summary>
    public class PetPlayer : ModPlayer
    {
        public bool hasFloatingBonboriPet;

        public override void ResetEffects()
        {
            hasFloatingBonboriPet = false;
        }
    }
}