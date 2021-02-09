using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrailEffects.Pets.FloatingBonboriPet
{
    public class FloatingBonboriProj : ModProjectile
    {

        public override string Texture => "TrailEffects/Items/Bags/WanderingBonbori";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Floating Bonbori");

            Main.projFrames[Type] = 1;
            Main.projPet[Type] = true;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            ProjectileID.Sets.LightPet[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.Size = TextureAssets.Projectile[Type]?.Size() ?? Vector2.Zero;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.timeLeft *= 5;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Asset<Texture2D> texture = TextureAssets.Projectile[Projectile.type];
            //Asset<Texture2D> glowTexture = ;
            SpriteEffects flip = Projectile.direction < 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            float sine = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3.5f) + 1f;

            for (int i = 0; i < 4; i++)
            {
                Vector2 offsetPosition = new Vector2(0, sine).RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture.Value, Projectile.position + offsetPosition - Main.screenPosition, null, Color.White * 0.25882352941f, Projectile.rotation, Vector2.Zero, Projectile.scale, flip, 0f);
            }

            for (int i2 = 0; i2 < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i2++)
            {
                spriteBatch.Draw(texture.Value, Projectile.oldPos[i2] - Main.sceneBackgroundPos, null, Color.White * 0.25882352941f, Projectile.oldRot[i2], Vector2.Zero, Projectile.scale, flip, 0f);
            }

            return true;
        }

        public override void AI()
        {
            LightMethod();
            DustMethod();
        }
        private void LightMethod()
        {
            float sine = ((float)Math.Sin(Main.GlobalTimeWrappedHourly * 3.5f) + 1f) / 2.2f;
            Lighting.AddLight(Projectile.Center, Color.DarkOrange.ToVector3() * 0.75f * (sine + 0.9f));
        }
        private void DustMethod()
        {
            Player player = Main.LocalPlayer;
            if (player.miscCounter % 15 == 0 && Main.rand.NextBool(3))
            {
                Vector2 center = Projectile.position;
                float rand = 1f + Main.rand.NextFloat() * 0.5f;

                if (Main.rand.NextBool(2))
                    rand *= -1f;

                center += new Vector2(rand * -25f, -8f);

                Dust dust = Dust.NewDustDirect(center, player.width, player.height, DustID.Firefly, 0, 0, 100);
                dust.rotation = Main.rand.NextFloat() * ((float)Math.PI * 2f);
                dust.velocity.X = rand * 0.2f;
                dust.noGravity = true;
                dust.customData = Projectile;
                dust.shader = GameShaders.Armor.GetSecondaryShader(player.cLight, player);
            }
        }
    }
}