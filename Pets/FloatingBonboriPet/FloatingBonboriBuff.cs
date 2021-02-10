using Microsoft.Xna.Framework;
using System.Linq;
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
            if (player.ownedProjectileCounts[projType] <= 0 && player.whoAmI == Main.myPlayer)
                Projectile.NewProjectile(player.position.X + player.width / 2f, player.position.Y + player.height / 2f, 0f, 0f, ModContent.ProjectileType<FloatingBonboriProj>(), 0, 0f, player.whoAmI);

            /*if (!player.controlDown || !player.releaseDown || player.doubleTapCardinalTimer[0] <= 0 ||
                player.doubleTapCardinalTimer[0] == 15) return;

            foreach (Projectile projectile in Main.projectile.Where(proj => proj.active && proj.type == ModContent.ProjectileType<FloatingBonboriProj>() && proj.owner == player.whoAmI))
                projectile.velocity += 5f * Vector2.Normalize(Main.MouseWorld - projectile.Center);*/
        }
    }
}