using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TrailEffects.ModPlayers;

namespace TrailEffects.Pets.FloatingBonboriPet
{
    public class FloatingBonboriBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Floating Bonbori");
            Description.SetDefault("A bonbori is floating behind you");

            Main.buffNoTimeDisplay[Type] = true;
            Main.lightPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<PetPlayer>().hasFloatingBonboriPet = true;
            player.buffTime[buffIndex] = 18000;

            int projType = ModContent.ProjectileType<FloatingBonboriProj>();
            Vector2 pos = new Vector2(player.position.X + player.width / 2f, player.position.Y + player.height / 2f);
            if (player.ownedProjectileCounts[projType] <= 0 && player.whoAmI == Main.myPlayer)
                Projectile.NewProjectileDirect(player.GetProjectileSource_Buff(buffIndex), pos, Vector2.Zero, ModContent.ProjectileType<FloatingBonboriProj>(), 0, 0f, player.whoAmI);
        }
    }
}