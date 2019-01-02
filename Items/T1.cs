using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using RuneCarver;
using Microsoft.Xna.Framework;

namespace RuneCarver.Items.T1 {
    public class Exodium : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Exodium");
            Tooltip.SetDefault("[c/31D741:Tier I]");
        }

        public override void AutoStaticDefaults() {
            Main.itemTexture[item.type] = ModLoader.GetTexture("RuneCarver/Items/T1/Exodium");
        }

        public override void SetDefaults() {
            item.melee = true;
            item.width = 24;
            item.height = 24;
            item.scale = 2f;

            item.useStyle = 1;

            item.UseSound = SoundID.Item60;
            item.autoReuse = true;
            item.value = Item.buyPrice(gold: 2);
            item.maxStack = 1;
            item.rare = 2;

            item.shoot = mod.ProjectileType("ExodiumProjectile");

            item.damage = 23;
            item.knockBack = 5f;
            item.useAnimation = 26;
            item.useTime = 26;
            item.shootSpeed = 5f;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Starfury, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 6);
            recipe.AddIngredient(ItemID.Feather, 10);
            recipe.AddTile(mod.TileType("RuneCarver"));
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }

    public class ExodiumProjectile : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Exo Shot");
        }

        public override void AutoStaticDefaults() {
            Main.projectileTexture[projectile.type] = ModLoader.GetTexture("RuneCarver/Items/T1/Exodium_Projectile");
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

    public class Exultion : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Exultion");
            Tooltip.SetDefault("[c/31D741:Tier I]");
        }

        public override void AutoStaticDefaults() {
            Main.itemTexture[item.type] = ModLoader.GetTexture("RuneCarver/Items/T1/Exultion");
        }

        public override void SetDefaults() {
            item.width = 64;
            item.height = 64;
            item.maxStack = 1;
            item.rare = 2;
            item.useStyle = 5;
            item.scale = 1.1f;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("ExultionProjectile");
            item.value = Item.buyPrice(gold: 2);
            item.noMelee = true;
            item.noUseGraphic = true;
            item.melee = true;
            item.autoReuse = true;

            item.damage = 46;
            item.useAnimation = 28;
            item.useTime = 28;
            item.shootSpeed = 5.6f;
            item.knockBack = 6f;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LightsBane, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 6);
            recipe.AddIngredient(ItemID.Feather, 10);
            recipe.AddTile(mod.TileType("RuneCarver"));
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            ModRecipe recipeC = new ModRecipe(mod);
            recipeC.AddIngredient(ItemID.BloodButcherer, 1);
            recipeC.AddIngredient(ItemID.JungleSpores, 6);
            recipeC.AddIngredient(ItemID.Feather, 10);
            recipeC.AddTile(mod.TileType("RuneCarver"));
            recipeC.SetResult(this, 1);
            recipeC.AddRecipe();
        }

        public override bool CanUseItem(Player player) {
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
    }

    public class ExultionProjectile : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Exultion");
        }

        public override void AutoStaticDefaults() {
            Main.projectileTexture[projectile.type] = ModLoader.GetTexture("RuneCarver/Items/T1/Exultion_Projectile");
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

    public class Colstice : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Colstice");
            Tooltip.SetDefault("[c/31D741:Tier I]");
        }

        public override void AutoStaticDefaults() {
            Main.itemTexture[item.type] = ModLoader.GetTexture("RuneCarver/Items/T1/Colstice");
        }

        public override void SetDefaults() {
            item.ranged = true;
            item.noMelee = true;
            item.width = 38;
            item.height = 15;
            item.scale = 1.5f;

            item.useStyle = 5;

            item.shoot = ProjectileID.Bullet;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item36;
            item.autoReuse = true;
            item.value = Item.buyPrice(gold: 2);
            item.maxStack = 1;
            item.rare = 2;

            item.damage = 7;
            item.knockBack = 1.5f;
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
            recipe.AddIngredient(ItemID.Boomstick, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 6);
            recipe.AddIngredient(ItemID.Feather, 10);
            recipe.AddTile(mod.TileType("RuneCarver"));
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }

    public class Cellcrusher : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Cellcrusher");
            Tooltip.SetDefault("[c/31D741:Tier I]");
        }

        public override void AutoStaticDefaults() {
            Main.itemTexture[item.type] = ModLoader.GetTexture("RuneCarver/Items/T1/Cellcrusher");
        }

        public override void SetDefaults() {
            item.ranged = true;
            item.noMelee = true;
            item.width = 22;
            item.height = 40;
            item.scale = 1.5f;

            item.useStyle = 5;

            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.useAmmo = AmmoID.Arrow;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.value = Item.buyPrice(gold: 2);
            item.maxStack = 1;
            item.rare = 2;

            item.damage = 14;
            item.knockBack = 1.2f;
            item.useAnimation = 30;
            item.useTime = 30;
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
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DemonBow, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 6);
            recipe.AddIngredient(ItemID.Feather, 10);
            recipe.AddTile(mod.TileType("RuneCarver"));
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            ModRecipe recipeC = new ModRecipe(mod);
            recipeC.AddIngredient(ItemID.TendonBow, 1);
            recipeC.AddIngredient(ItemID.JungleSpores, 6);
            recipeC.AddIngredient(ItemID.Feather, 10);
            recipeC.AddTile(mod.TileType("RuneCarver"));
            recipeC.SetResult(this, 1);
            recipeC.AddRecipe();
        }
    }

    public class Ash : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ash");
            Tooltip.SetDefault("[c/31D741:Tier I]");
            Item.staff[item.type] = true;
        }

        public override void AutoStaticDefaults() {
            Main.itemTexture[item.type] = ModLoader.GetTexture("RuneCarver/Items/T1/Ash");
        }

        public override void SetDefaults() {
            item.magic = true;
            item.noMelee = true;
            item.width = 37;
            item.height = 37;
            item.scale = 1.5f;

            item.useAnimation = 10;
            item.useTime = 10;
            item.useStyle = 5;

            item.shoot = mod.ProjectileType("AshProjectile");
            item.UseSound = SoundID.Item43;
            item.autoReuse = true;
            item.value = Item.buyPrice(gold: 2);
            item.maxStack = 1;
            item.rare = 2;

            item.damage = 15;
            item.knockBack = 3f;
            item.mana = 5;
            item.useAnimation = 10;
            item.useTime = 10;
            item.shootSpeed = 9f;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AmberStaff, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 6);
            recipe.AddIngredient(ItemID.Feather, 10);
            recipe.AddTile(mod.TileType("RuneCarver"));
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }

    public class AshProjectile : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ash Fireball");
        }

        public override void AutoStaticDefaults() {
            Main.projectileTexture[projectile.type] = ModLoader.GetTexture("RuneCarver/Items/T1/Ash_Projectile");
        }

        public override void SetDefaults() {
            projectile.width = 16;
            projectile.height = 16;
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

    public class AugurArcanum : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Auger Arcanum");
            Tooltip.SetDefault("[c/31D741:Tier I]");
        }

        public override void AutoStaticDefaults() {
            Main.itemTexture[item.type] = ModLoader.GetTexture("RuneCarver/Items/T1/Augur_Arcanum");
        }

        public override void SetDefaults() {
            item.magic = true;
            item.noMelee = true;
            item.width = 40;
            item.height = 22;
            item.scale = 1.5f;

            item.useStyle = 5;

            item.shoot = mod.ProjectileType("AugurProjectile");
            item.UseSound = SoundID.Item12;
            item.autoReuse = true;
            item.value = Item.buyPrice(gold: 2);
            item.maxStack = 1;
            item.rare = 2;
            item.shootSpeed = 8f;

            item.damage = 42;
            item.knockBack = 8f;
            item.mana = 15;
            item.useAnimation = 40;
            item.useTime = 40;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpaceGun, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 6);
            recipe.AddIngredient(ItemID.Feather, 10);
            recipe.AddTile(mod.TileType("RuneCarver"));
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }

    public class AugurProjectile : ModProjectile {
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
                    Dust dust3 = Main.dust[num463];
                    dust3.velocity *= 0.2f;
                    num4 = num462;
                }
            }
        }
    }
}