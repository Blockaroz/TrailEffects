﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using TrailEffects.Utilities;

namespace TrailEffects.Items.Bags
{
    public class WanderingBonbori : DustItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wandering Bonbori");
            Tooltip.SetDefault("Attracts fireflies");

            ItemID.Sets.ItemNoGravity[Type] = true;
        }

        public override void SafeSetDefaults()
        {
            Item.DefaultToBag(ItemRarityID.Green);
        }

        public override void SafeUpdateAccessory(Player player, bool hideVanity)
        {
            DustMethod(player, 5, 3);
        }

        public override void SafeUpdateVanity(Player player)
        {
            DustMethod(player, 5, 3);
        }

        public override void HoldStyle(Player player)
        {
            DustMethod(player, 15, 4);
        }

        public override void PostUpdate()
        {
            SineLightingMethod(Item.Center, 3f, 0.6f);
        }

        public void DustMethod(Player player, int frequency1, int frequency2, float xV = 0, float yV = 0)
        {
            if (player.miscCounter % frequency1 == 0 && Main.rand.NextBool(frequency2))
            {
                Vector2 center = player.position;
                float rand = 1f + Main.rand.NextFloat() * 0.5f;

                if (Main.rand.NextBool(2))
                    rand *= -1f;

                center += new Vector2(rand * -25f, -8f);

                Dust dust = Dust.NewDustDirect(center, player.width, player.height, DustID.Firefly, xV, yV, 100);
                dust.rotation = Main.rand.NextFloat() * ((float)Math.PI * 2f);
                dust.velocity.X = rand * 0.2f;
                dust.noGravity = true;
                dust.customData = player;
                dust.shader = GameShaders.Armor.GetSecondaryShader(player.cMinion, player);
            }

            SineLightingMethod(player.Center, 3f, 0.7f);
        }

        private static void SineLightingMethod(Vector2 position, float speed, float opacity)
        {
            float sine = ((float)Math.Sin(Main.GlobalTimeWrappedHourly * speed) + 1f) / 2.2f;
            //Main.NewText(sine);
            Lighting.AddLight(position, Color.DarkOrange.ToVector3() * opacity * (sine + 0.9f));
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
            float rotation, float scale, int whoAmI)
        {
            Asset<Texture2D> texture = ModContent.GetTexture("TrailEffects/Assets/WanderingBonbori_Glow");
            Vector2 position = Item.position - Main.screenPosition +
                               new Vector2(Item.width / 2f, Item.height - texture.Height() * 0.5f);

            spriteBatch.Draw(texture.Value, position, null, Color.White, rotation, texture.Size() * 0.5f, scale,
                SpriteEffects.None, 0f);
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
            ref float rotation, ref float scale, int whoAmI)
        {
            Asset<Texture2D> texture = ModContent.GetTexture("TrailEffects/Assets/WanderingBonbori_Glow");
            Vector2 position = Item.position - Main.screenPosition +
                               new Vector2(Item.width / 2f, Item.height - texture.Height() * 0.5f);

            for (int i = 0; i < 4; i++)
            {
                float sine = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f) + 1f;
                Vector2 offsetPosition = new Vector2(0, sine).RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture.Value, position + offsetPosition, null, Color.White * 0.25882352941f, rotation,
                    texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            }

            return true;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame,
            Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Asset<Texture2D> texture = ModContent.GetTexture("TrailEffects/Assets/WanderingBonbori_Glow");

            for (int i = 0; i < 4; i++)
            {
                float sine = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f);
                Vector2 offsetPosition = new Vector2(0, (sine + 1f) / 1.5f).RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture.Value, position + offsetPosition, null, Color.White * 0.25882352941f, 0,
                    origin,
                    scale, SpriteEffects.None, 0f);
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.Firefly, 3)
                .AddTile(TileID.Loom)
                .Register();
        }
    }
}