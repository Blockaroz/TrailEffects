﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using TrailEffects.Utilities;

namespace TrailEffects.Items.Bags
{
    public class CursedBag : DustItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Flame Pouch");
            Tooltip.SetDefault("Creates a trail of cursed flames behind you");
        }

        public override void SafeSetDefaults() => Item.DefaultToBag(ItemRarityID.LightRed);

        public override void UpdateMovement(Player player)
        {
            for (int i = 0; i < 2; i++)
            {
                Dust dust = Dust.NewDustDirect(player.position, player.width, player.height - 4, DustID.CursedTorch, 0,
                    0,
                    128,
                    Color.White);
                dust.noGravity = true;
                dust.velocity *= 0.5f;
                dust.velocity.Y -= 0.5f;
                dust.fadeIn = 1.2f;
                dust.shader = GameShaders.Armor.GetSecondaryShader(player.cMinion, player);
            }
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
            float rotation, float scale, int whoAmI)
        {
            Asset<Texture2D> texture = ModContent.GetTexture("TrailEffects/Assets/CursedBag_Glow");
            Vector2 position = Item.position - Main.screenPosition +
                               new Vector2(Item.width / 2f, Item.height - texture.Height() * 0.5f);

            spriteBatch.Draw(texture.Value, position, null, Color.White, rotation, texture.Size() * 0.5f, scale,
                SpriteEffects.None, 0f);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 5)
                .AddIngredient(ItemID.Cobweb, 20)
                .AddIngredient(ItemID.CursedFlame, 25)
                .AddIngredient(ItemID.SoulofNight, 10)
                .AddTile(TileID.Loom)
                .Register();
        }
    }
}