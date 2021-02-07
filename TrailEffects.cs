using Terraria.ModLoader;

namespace TrailEffects
{
    class x : ModItem { public override void SetDefaults() { } public override void SetStaticDefaults() { DisplayName.SetDefault("When the nefarious one..."); } public override void UpdateAccessory(Player player) { player.KillMe(Terraria.DataStructures.PlayerDeathReason.ByCustomReason($"Died to {0}", 500, 500, 500, 2); } }
    public class TrailEffects : Mod
    {
    }
}
