﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.ModLoader;
using TrailEffects.Utilities;

namespace TrailEffects.Items
{
    /// <summary>
    /// Base <see cref="ModItem"/> class using by <see cref="TrailEffects"/>.
    /// </summary>
    public abstract class TEItem : ModItem
    {
        public virtual string Glowmask => "";

        /// <summary>
        /// Whether this item's size should be set automatically or not.
        /// </summary>
        public virtual bool Autosize { get; }

        public sealed override void SetDefaults()
        {
            if (Autosize)
                Item.Autosize();

            SafeSetDefaults();
        }

        /// <summary>
        /// Called at the end of <see cref="SetDefaults"/>.
        /// </summary>
        public virtual void SafeSetDefaults()
        {
        }

        public sealed override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            if (!string.IsNullOrEmpty(Glowmask))
            {
                Asset<Texture2D> texture = ModContent.GetTexture(Glowmask);
                Vector2 itemSizeOffset = new Vector2(Item.width / 2, Item.height - texture.Height() * 0.5f);
                Vector2 position = Item.position - Main.screenPosition + itemSizeOffset;

                spriteBatch.Draw(texture.Value, position, null, Color.White, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            }
        }

        public virtual void SafePostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
        }

        public sealed override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            if (!string.IsNullOrEmpty(Glowmask))
            {
                Asset<Texture2D> texture = ModContent.GetTexture(Glowmask);
                Vector2 position = Item.position - Main.screenPosition + new Vector2(Item.width / 2, Item.height - texture.Height() * 0.5f);

                for (int i = 0; i < 4; i++)
                {
                    float sine = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f) + 1f;
                    Vector2 offsetPosition = new Vector2(0, sine);
                    Vector2 rotatedPos = offsetPosition.RotatedBy(MathHelper.PiOver2 * i) * 2;

                    spriteBatch.Draw(texture.Value, position + rotatedPos, null, Color.White * 0.25882352941f, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
                }
            }

            return true;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (!string.IsNullOrEmpty(Glowmask))
            {
                Asset<Texture2D> texture = ModContent.GetTexture("TrailEffects/Assets/WanderingBonbori_Glow");

                for (int i = 0; i < 4; i++)
                {
                    float sine = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f);
                    Vector2 offsetPosition = new Vector2(0, (sine + 1f) / 1.5f);
                    Vector2 rotatedPos = offsetPosition.RotatedBy(MathHelper.PiOver2 * i) * 2;

                    spriteBatch.Draw(texture.Value, position + rotatedPos, null, Color.White * 0.25882352941f, 0, origin, scale, SpriteEffects.None, 0f);
                }
            }

            return true;
        }
    }
}