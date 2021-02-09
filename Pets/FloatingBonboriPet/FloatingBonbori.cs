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
        public override string Texture => "TrailEffects/Items/Bags/WanderingBonbori";

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
                player.AddBuff(Item.buffTime, 3600);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<WanderingBonbori>())
                .AddIngredient(ItemID.DynastyWood, 25)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}