using Terraria.ModLoader;

namespace TrailEffects
{
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