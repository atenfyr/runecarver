using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;

namespace Upgrader.Tiles {
	public class Upgrader : ModTile {
		public override void SetDefaults() {
            Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18};
			TileObjectData.addTile(Type);

            animationFrameHeight = 56;

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Rune Carver");
			AddMapEntry(new Color(102, 51, 153), name);
			dustType = 53;
			disableSmartCursor = true;
		}

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) {
			r = 0.4f;
			g = 0.2f;
			b = 0.6f;
        }

		public override void AnimateTile(ref int frame, ref int frameCounter) {
			if (++frameCounter > 8) {
				frameCounter = 0;
				if (++frame > 5) {
					frame = 0;
				}
			}
        }

		public override void KillMultiTile(int i, int j, int frameX, int frameY) {
			Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("UpgraderItem"));
		}
	}
    
    public class UpgraderItem : ModItem {
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Rune Carver");
            Tooltip.SetDefault("Used to carve runes, empowering certain weapons");
		}

        public override void AutoStaticDefaults() {
            Main.itemTexture[item.type] = ModLoader.GetTexture("Upgrader/Tiles/UpgraderItem");
        }

        public override void SetDefaults() {
            item.width = 24;
			item.height = 20;
			item.maxStack = 99;
            item.rare = 2;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
            item.consumable = true;
	        item.createTile = mod.TileType("Upgrader");
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Obsidian, 25);
            recipe.AddIngredient(ItemID.Diamond, 1);
            recipe.AddIngredient(ItemID.Amber, 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
	}
}