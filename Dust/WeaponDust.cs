using Terraria;
using Terraria.ModLoader;

namespace RuneCarver.Dusts {
    /*
        green - new Color(49, 215, 65, 25)
        yellow - new Color(243, 255, 103, 25)
        red - new Color(255, 77, 77, 25)
     */
    class WeaponDust : ModDust {
        public override bool Autoload(ref string name, ref string texture) {
            texture = "RuneCarver/Dust/default";
            return true;
        }

        public override void OnSpawn(Dust dust) {
            dust.noGravity = true;
            dust.noLight = false;
        }

        public override bool Update(Dust dust) {
            dust.position += dust.velocity;
            dust.scale -= 0.01f;
            Lighting.AddLight(dust.position, (dust.color.R/255f), (dust.color.G/255f), (dust.color.B/255f));
            if (dust.scale < 0.95f) {
                dust.active = false;
            }
            return false;
        }
    }
}