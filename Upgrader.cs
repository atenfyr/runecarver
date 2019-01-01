using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Upgrader {
    class Upgrader : Mod {
        public Upgrader() {
            Properties = new ModProperties() {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }

        public override void AddRecipeGroups() {
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Lunar Fragment", new int[] {
                ItemID.FragmentVortex,
                ItemID.FragmentNebula,
                ItemID.FragmentSolar,
                ItemID.FragmentStardust
            });
            RecipeGroup.RegisterGroup("Upgrader:AnyLunarFragment", group);
        }
    }
}
