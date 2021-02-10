using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using TrailEffects.Items;
using TrailEffects.Items.Bags;

namespace TrailEffects.Pets.FloatingBonboriPet
{
    public class FloatingBonbori : TEItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Floating Bonbori");
            Tooltip.SetDefault("Attracts fireflies" +
                "\nFollows the player");
        }
        public override void SafeSetDefaults()
        {
            Item.DefaultToVanitypet(ModContent.ProjectileType<FloatingBonboriProj>(), ModContent.BuffType<FloatingBonboriBuff>());
            Item.useStyle = ItemUseStyleID.HoldUp;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
                player.AddBuff(Item.buffType, 3600);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<WanderingBonbori>())
                .AddIngredient(ItemID.DynastyWood, 25)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }

        #region drawing
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Asset<Texture2D> texture = ModContent.GetTexture("TrailEffects/Assets/FloatingBonbori_Glow");
            Vector2 position = Item.position - Main.screenPosition + new Vector2(Item.width / 2, Item.height - texture.Height() * 0.5f);
            float sine = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 1.16f) + 1f;

            spriteBatch.Draw(texture.Value, position, null, Color.White, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Asset<Texture2D> texture = ModContent.GetTexture("TrailEffects/Assets/FloatingBonbori_Glow");
            Vector2 position = Item.position - Main.screenPosition + new Vector2(Item.width / 2, Item.height - texture.Height() * 0.5f);

            for (int i = 0; i < 4; i++)
            {
                float sine = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 1.16f) + 1f;
                Vector2 offsetPositon = new Vector2(0, sine).RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture.Value, position + offsetPositon, null, Color.White * 0.25882352941f, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Asset<Texture2D> texture = ModContent.GetTexture("TrailEffects/Assets/FloatingBonbori_Glow");

            for (int i = 0; i < 4; i++)
            {
                float sine = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f);
                Vector2 offsetPositon = new Vector2(0, (sine + 1f) / 1.5f).RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture.Value, position + offsetPositon, null, Color.White * 0.25882352941f, 0, origin, scale, SpriteEffects.None, 0f);
            }

            return true;
        }
        #endregion
    }
}