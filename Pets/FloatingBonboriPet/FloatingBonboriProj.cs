﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using TrailEffects.ModPlayers;

namespace TrailEffects.Pets.FloatingBonboriPet
{
    public class FloatingBonboriProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Floating Bonbori");

            Main.projFrames[Type] = 1;
            Main.projPet[Type] = true;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 8;
            ProjectileID.Sets.LightPet[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.Size = TextureAssets.Projectile[Type]?.Size() ?? Vector2.Zero;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.manualDirectionChange = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            float rotateFactor = 11f;

            float cos = (float)Math.Cos(Main.GlobalTimeWrappedHourly * 1.16f);
            Vector2 calcPos = new Vector2(-player.direction * 60, -20 + cos);

            if (!player.active || !player.GetModPlayer<PetPlayer>().hasFloatingBonboriPet)
                Projectile.Kill();


            float distance = Vector2.Distance(Projectile.Center, player.MountedCenter + calcPos);
            if (distance > 1000)
                Projectile.Center = player.MountedCenter + calcPos;

            if (distance < 1f)
                Projectile.velocity *= 0.25f;

            Vector2 vector = (player.MountedCenter + calcPos) - Projectile.Center;

            if (vector != Vector2.Zero)
            {
                if (vector.Length() < 0.004f)
                    Projectile.velocity = vector;
                else
                    Projectile.velocity = vector * 0.1f;
            }

            Projectile.position += new Vector2(0, cos);

            //rotation adapted from flying pets

            if (Projectile.velocity.Length() > 1f)
            {
                float value = Projectile.velocity.X * 0.02f + Projectile.velocity.Y * Projectile.spriteDirection * 0.02f;
                if (Math.Abs(Projectile.rotation - value) >= Math.PI)
                {
                    if (value < Projectile.rotation)
                        Projectile.rotation -= (float)Math.PI * 2f;
                    else
                        Projectile.rotation += (float)Math.PI * 2f;
                }
                Projectile.rotation = (Projectile.rotation * (rotateFactor - 1f) + value) / rotateFactor;
            }
            else
            {
                if (Projectile.rotation > (float)Math.PI)
                    Projectile.rotation -= (float)Math.PI * 2f;
                if (Projectile.rotation > -0.004f && Projectile.rotation < 0.004f)
                    Projectile.rotation = 0;
                else
                    Projectile.rotation *= 0.95f;
            }

            Projectile.spriteDirection = Projectile.position.X < player.position.X ? -1 : 1;

            LightMethod();

            if (player.miscCounter % 15 == 0 && Main.rand.NextBool(5))
            {
                DustMethod();
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
                DustMethod();
        }
        private void LightMethod()
        {
            float sine = ((float)Math.Sin(Main.GlobalTimeWrappedHourly * 3.5f) + 1f) / 2.2f;
            Lighting.AddLight(Projectile.Center, Color.DarkOrange.ToVector3() * 0.75f * (sine + 0.9f));
        }
        private void DustMethod()
        {
            Player player = Main.player[Projectile.owner];

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

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Asset<Texture2D> texture = ModContent.GetTexture("TrailEffects/Assets/FloatingBonbori_Glow");
            float cos = (float)Math.Cos(Main.GlobalTimeWrappedHourly * 3.5f) + 1f;

            for (int i = 0; i < 4; i++)
            {
                Vector2 offsetPosition = new Vector2(0, cos).RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture.Value, Projectile.Center + offsetPosition - Main.screenPosition, null, Color.White * 0.25882352941f, Projectile.rotation, Projectile.Size / 2f, Projectile.scale, SpriteEffects.None, 0f);
            }

            for (int h = 0; h < ProjectileID.Sets.TrailCacheLength[Projectile.type]; h++)
            {
                Vector2 position = Projectile.oldPos[h] + (Projectile.Size / 2f);
                float rotation = Projectile.oldRot[h];
                spriteBatch.Draw(texture.Value, position - Main.screenPosition, null, Color.White * 0.1f * (1f / h), rotation, Projectile.Size / 2f, Projectile.scale, SpriteEffects.None, 0f);
            }

            return true;
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;
    }
}