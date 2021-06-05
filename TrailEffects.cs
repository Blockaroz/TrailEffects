using Terraria.ModLoader;

namespace TrailEffects
{
    class x : ModItem { public override void SetDefaults() { } public override void SetStaticDefaults() { DisplayName.SetDefault("When the nefarious one..."); } public override void UpdateAccessory(Player player) { player.KillMe(Terraria.DataStructures.PlayerDeathReason.ByCustomReason($"Died to {0}", 500, 500, 500, 2); } }
    public class TrailEffects : Mod
    {
        public TrailEffects()
        {
            Instance = this;

            Properties = new ModProperties
            {
                Autoload = true,
                AutoloadBackgrounds = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }

        /// <summary>
        ///     <see cref="TrailEffects" />' instance, the same instance utilized by tModLoader.
        /// </summary>
        public TrailEffects Instance { get; }
    }
}
