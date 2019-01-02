using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace RuneCarver.Items.T4 {
    public class Exodium4 : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Exodium");
            Tooltip.SetDefault("[c/31D741:Tier IV]");
        }

        public override void AutoStaticDefaults() {
            Main.itemTexture[item.type] = ModLoader.GetTexture("RuneCarver/Items/T4/Exodium");
        }

        public override void SetDefaults() {
            item.melee = true;
            item.width = 36;
            item.height = 36;
            item.scale = 2f;

            item.useStyle = 1;

            item.UseSound = SoundID.Item60;
            item.autoReuse = true;
            item.value = Item.buyPrice(gold: 16);
            item.maxStack = 1;
            item.rare = 5;

            item.shoot = mod.ProjectileType("ExodiumProjectile4");

            item.damage = 86;
            item.knockBack = 8f;
            item.useAnimation = 23;
            item.useTime = 23;
            item.shootSpeed = 6.5f;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Exodium3"), 1);
            recipe.AddIngredient(ItemID.ChlorophyteClaymore, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddTile(mod.TileType("RuneCarver"));
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }

    public class ExodiumProjectile4 : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Exo Shot");
        }

        public override void AutoStaticDefaults() {
            Main.projectileTexture[projectile.type] = ModLoader.GetTexture("RuneCarver/Items/T4/Exodium_Projectile");
        }

        public override void SetDefaults() {
            projectile.width = 12;
            projectile.height = 12;
            projectile.scale = 1.5f;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 5;
            projectile.penetrate = 3;
        }

        public override void AI() {
            projectile.rotation = (float)(Math.Atan2(projectile.velocity.Y, projectile.velocity.X)) + MathHelper.ToRadians(45);
            
            if (++projectile.ai[0] >= 12) {
                int trail = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 8, 8, mod.DustType("WeaponDust"), 0f, 0f, 0, Constants.DustColors[0], 1.3f);
                Main.dust[trail].velocity *= 0.3f;
            }
        }

        public override void Kill(int timeLeft) {
            for (int i = 0; i <= 5; i++) {
                Dust.NewDust(projectile.position, 8, 8, mod.DustType("WeaponDust"), 0f, 0f, 0, Constants.DustColors[0], 1.25f);
            }
        }
    }

    public class Exultion4 : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Exultion");
            Tooltip.SetDefault("[c/31D741:Tier IV]");
        }

        public override void AutoStaticDefaults() {
            Main.itemTexture[item.type] = ModLoader.GetTexture("RuneCarver/Items/T4/Exultion");
        }

        public override void SetDefaults() {
            item.width = 64;
            item.height = 64;
            item.maxStack = 1;
            item.rare = 5;
            item.useStyle = 5;
            item.scale = 1.1f;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("ExultionProjectile4");
            item.value = Item.buyPrice(gold: 16);
            item.noMelee = true;
            item.noUseGraphic = true;
            item.melee = true;
            item.autoReuse = true;

            item.damage = 158;
            item.useAnimation = 22;
            item.useTime = 22;
            item.shootSpeed = 5.6f;
            item.knockBack = 6f;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Exultion3"), 1);
            recipe.AddIngredient(ItemID.ChlorophytePartisan, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddTile(mod.TileType("RuneCarver"));
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override bool CanUseItem(Player player) {
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
    }

    public class ExultionProjectile4 : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Exultion");
        }

        public override void AutoStaticDefaults() {
            Main.projectileTexture[projectile.type] = ModLoader.GetTexture("RuneCarver/Items/T4/Exultion_Projectile");
        }

        public override void SetDefaults() {
            projectile.width = 150;
            projectile.height = 150;
            projectile.aiStyle = 19;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.ownerHitCheck = true;
            projectile.melee = true;
            projectile.penetrate = -1;
        }

        public override void AI() {
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            projectile.direction = projOwner.direction;
            projOwner.heldProj = projectile.whoAmI;
            projOwner.itemTime = projOwner.itemAnimation;
            projectile.position.X = ownerMountedCenter.X - (float)(projectile.width / 2);
            projectile.position.Y = ownerMountedCenter.Y - (float)(projectile.height / 2);
            if (!projOwner.frozen) {
                if (projectile.ai[0] == 0f) {
                    projectile.ai[0] = 3f;
                    projectile.netUpdate = true;
                }
                if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3) {
                    projectile.ai[0] -= 2.4f;
                } else {
                    projectile.ai[0] += 2.1f;
                }
            }
            projectile.position += projectile.velocity * projectile.ai[0];
            if (projOwner.itemAnimation == 0) {
                projectile.Kill();
            }
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
            if (projectile.spriteDirection == -1) {
                projectile.rotation -= MathHelper.ToRadians(90f);
            }
        }
    }

    public class Colstice4 : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Colstice");
            Tooltip.SetDefault("[c/31D741:Tier IV]");
        }

        public override void AutoStaticDefaults() {
            Main.itemTexture[item.type] = ModLoader.GetTexture("RuneCarver/Items/T4/Colstice");
        }

        public override void SetDefaults() {
            item.ranged = true;
            item.noMelee = true;
            item.width = 42;
            item.height = 20;
            item.scale = 1.5f;

            item.useStyle = 5;

            item.shoot = ProjectileID.Bullet;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item36;
            item.autoReuse = true;
            item.value = Item.buyPrice(gold: 16);
            item.maxStack = 1;
            item.rare = 5;

            item.damage = 21;
            item.knockBack = 1.7f;
            item.useAnimation = 28;
            item.useTime = 30;
            item.shootSpeed = 20f;
        }

        public override Vector2? HoldoutOffset() {
            return new Vector2(0f, 0.1f);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
            for (int i = 0; i < 6; i++) {
                Vector2 newSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
                Projectile.NewProjectile(position.X, position.Y, newSpeed.X, newSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Colstice3"), 1);
            recipe.AddIngredient(ItemID.Megashark, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddTile(mod.TileType("RuneCarver"));
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }

    public class Cellcrusher4 : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Cellcrusher");
            Tooltip.SetDefault("[c/31D741:Tier IV]");
        }

        public override void AutoStaticDefaults() {
            Main.itemTexture[item.type] = ModLoader.GetTexture("RuneCarver/Items/T4/Cellcrusher");
        }

        public override void SetDefaults() {
            item.ranged = true;
            item.noMelee = true;
            item.width = 23;
            item.height = 40;
            item.scale = 1.5f;

            item.useStyle = 5;

            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.useAmmo = AmmoID.Arrow;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.value = Item.buyPrice(gold: 16);
            item.maxStack = 1;
            item.rare = 5;

            item.damage = 45;
            item.knockBack = 1.4f;
            item.useAnimation = 21;
            item.useTime = 21;
            item.shootSpeed = 20f;
        }

        public override Vector2? HoldoutOffset() {
            return new Vector2(0f, 2f);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
            for (int i = 0; i < 2; i++) {
                Vector2 newSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
                Projectile.NewProjectile(position.X, position.Y, newSpeed.X, newSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }

        public override void AddRecipes() {
            ModRecipe recipe1 = new ModRecipe(mod);
            recipe1.AddIngredient(mod.ItemType("Cellcrusher3"), 1);
            recipe1.AddIngredient(ItemID.ChlorophyteShotbow, 1);
            recipe1.AddIngredient(ItemID.HallowedBar, 12);
            recipe1.AddIngredient(ItemID.SoulofMight, 5);
            recipe1.AddIngredient(ItemID.SoulofSight, 5);
            recipe1.AddIngredient(ItemID.SoulofFright, 5);
            recipe1.AddTile(mod.TileType("RuneCarver"));
            recipe1.SetResult(this, 1);
            recipe1.AddRecipe();

            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(mod.ItemType("Cellcrusher3"), 1);
            recipe2.AddIngredient(ItemID.TitaniumRepeater, 1);
            recipe2.AddIngredient(ItemID.HallowedBar, 12);
            recipe2.AddIngredient(ItemID.SoulofMight, 5);
            recipe2.AddIngredient(ItemID.SoulofSight, 5);
            recipe2.AddIngredient(ItemID.SoulofFright, 5);
            recipe2.AddTile(mod.TileType("RuneCarver"));
            recipe2.SetResult(this, 1);
            recipe2.AddRecipe();
        }
    }

    public class Ash4 : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ash");
            Tooltip.SetDefault("[c/31D741:Tier IV]");
            Item.staff[item.type] = true;
        }

        public override void AutoStaticDefaults() {
            Main.itemTexture[item.type] = ModLoader.GetTexture("RuneCarver/Items/T4/Ash");
        }

        public override void SetDefaults() {
            item.magic = true;
            item.noMelee = true;
            item.width = 43;
            item.height = 43;
            item.scale = 1.5f;

            item.useStyle = 5;

            item.shoot = mod.ProjectileType("AshProjectile4");
            item.UseSound = SoundID.Item43;
            item.autoReuse = true;
            item.value = Item.buyPrice(gold: 16);
            item.maxStack = 1;
            item.rare = 5;

            item.damage = 45;
            item.knockBack = 3f;
            item.mana = 6;
            item.useAnimation = 10;
            item.useTime = 10;
            item.shootSpeed = 11f;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Ash3"), 1);
            recipe.AddIngredient(ItemID.RainbowRod, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddTile(mod.TileType("RuneCarver"));
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }

    public class AshProjectile4 : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ash Fireball");
        }

        public override void AutoStaticDefaults() {
            Main.projectileTexture[projectile.type] = ModLoader.GetTexture("RuneCarver/Items/T4/Ash_Projectile");
        }

        public override void SetDefaults() {
            projectile.width = 19;
            projectile.height = 19;
            projectile.scale = 1.5f;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.penetrate = 2;
        }

        public override void AI() {
            if (projectile.penetrate <= 1) {
                projectile.Kill();
            }
            projectile.rotation = (float)(Math.Atan2(projectile.velocity.Y, projectile.velocity.X)) + MathHelper.ToRadians(45);
            if (++projectile.ai[0] >= 12) {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 24, 24, mod.DustType("WeaponDust"), 0f, 0f, 0, Constants.DustColors[0], 1.5f);
            }
        }

        public override void Kill(int timeLeft) {
            for (int i = 0; i <= 5; i++) {
                Dust.NewDust(projectile.position, 8, 8, mod.DustType("WeaponDust"), 0f, 0f, 0, Constants.DustColors[0], 1.25f);
            }
        }
    }

    public class AugurArcanum4 : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Auger Arcanum");
            Tooltip.SetDefault("[c/31D741:Tier IV]");
        }

        public override void AutoStaticDefaults() {
            Main.itemTexture[item.type] = ModLoader.GetTexture("RuneCarver/Items/T4/Augur_Arcanum");
        }

        public override void SetDefaults() {
            item.magic = true;
            item.noMelee = true;
            item.width = 42;
            item.height = 26;
            item.scale = 1.5f;

            item.useStyle = 5;

            item.shoot = mod.ProjectileType("AugurProjectile4");
            item.UseSound = SoundID.Item12;
            item.autoReuse = true;
            item.value = Item.buyPrice(gold: 16);
            item.maxStack = 1;
            item.rare = 5;
            item.shootSpeed = 8f;

            item.damage = 162;
            item.knockBack = 8f;
            item.mana = 16;
            item.useAnimation = 40;
            item.useTime = 40;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AugurArcanum3"), 1);
            recipe.AddIngredient(ItemID.UnholyTrident, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddTile(mod.TileType("RuneCarver"));
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }

    public class AugurProjectile4 : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Auger Arcanum");
        }

        public override void AutoStaticDefaults() {
            Main.projectileTexture[projectile.type] = ModLoader.GetTexture("RuneCarver/Items/Misc/blank");
        }

        public override void SetDefaults() {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = -1;
            projectile.extraUpdates = 100;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
        }

        public override void AI() {
            int num4 = 0;
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 5f) {
                for (int num462 = 0; num462 < 4; num462 = num4 + 1) {
                    Vector2 position132 = projectile.position;
                    position132 -= projectile.velocity * ((float)num462 * 0.25f);
                    projectile.alpha = 255;
                    Vector2 position133 = position132;
                    int num463 = Dust.NewDust(position133, 1, 1, mod.DustType("WeaponDust"), 0f, 0f, 0, Constants.DustColors[0], 1f);
                    Main.dust[num463].position = position132;
                    Dust dust62 = Main.dust[num463];
                    dust62.position.X = dust62.position.X + (float)(projectile.width / 2);
                    Dust dust63 = Main.dust[num463];
                    dust63.position.Y = dust63.position.Y + (float)(projectile.height / 2);
                    Main.dust[num463].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                    Dust dusT4 = Main.dust[num463];
                    dusT4.velocity *= 0.2f;
                    num4 = num462;
                }
            }
        }
    }
}