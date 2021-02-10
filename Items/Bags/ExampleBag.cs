﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using TrailEffects.Utilities;

namespace TrailEffects.Items.Bags
{
    public class ExampleBag : DustItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Example Pouch");
            Tooltip.SetDefault("Creates a trail behind you");
        }

        public override void SafeSetDefaults()
        {
            Item.DefaultToBag(ItemRarityID.Green);
        }

        public override void UpdateMovement(Player player)
        {
            for (int i = 0; i < 2; i++)
            {
                Dust dust = Dust.NewDustDirect(player.position, player.width, player.height - 4, 267, 0, 0, 100,
                    Color.SlateGray);
                dust.noGravity = true;
                dust.velocity *= 0.5f;
                dust.velocity.Y -= 0.5f;
                dust.fadeIn = 1.2f;
                dust.shader = GameShaders.Armor.GetSecondaryShader(player.cMinion, player);
            }
        }

        public override void AddRecipes()
        {
            /*CreateRecipe()
                .AddIngredient(ItemID.Silk, 3)
                .AddIngredient(ItemID.Cobweb, 15)
                .AddIngredient(ItemID.Gel, 10)
                .AddTile(TileID.Loom);
                .Register();*/
        }
    }
}