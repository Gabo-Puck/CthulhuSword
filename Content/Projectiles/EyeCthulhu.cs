using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TutorialSword.Content.Projectiles
{
    internal class EyeCthulhu : ModProjectile
    {
        public bool FadeIn
        {
            get => Projectile.localAI[0] == 1f;
            set => Projectile.localAI[0] = value ? 1f : 0f;
        }

        public bool PlayedSpawnSound
        {
            get => Projectile.localAI[1] == 1f;
            set => Projectile.localAI[1] = value ? 1f : 0f;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.alpha = 255;
            Projectile.timeLeft = 90;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.netImportant = true;
            Projectile.aiStyle = -1;
            Projectile.damage = 10;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * Projectile.Opacity;
        }

        public void FadeInAndOut()
        {
            int fadeSpeed = 10;
            if (!FadeIn && Projectile.alpha > 0)
            {
                Projectile.alpha -= fadeSpeed;
                if (Projectile.alpha < 0)
                {
                    FadeIn = true;
                    Projectile.alpha = 0;
                }
            }else if (FadeIn && Projectile.timeLeft < 255f / fadeSpeed)
            {
                Projectile.alpha += fadeSpeed;
                if (Projectile.alpha > 255) {
                    Projectile.alpha = 255;
                }
            }
        }

        private void SpawnDust()
        {
            if (Main.rand.NextBool(2)) // only spawn 20% of the time
            {
                int choice = Main.rand.Next(3); // choose a random number: 0, 1, or 2
                if (choice == 0) // use that number to select dustID: 15, 57, or 58
                {
                    choice = 15;
                }
                else if (choice == 1)
                {
                    choice = 57;
                }
                else
                {
                    choice = 58;
                }
                // Spawn the dust
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, choice, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, Color.DarkRed, 0.7f);
            }
        }

        public override void AI()
        {
            SpawnDust();
            FadeInAndOut();
            Lighting.AddLight(Projectile.Center, 0.9f, 0.1f, 0.3f);
            if (!PlayedSpawnSound)
            {
                PlayedSpawnSound = true;
                SoundEngine.PlaySound(SoundID.DD2_BetsyScream, Projectile.position);
            }

            Projectile.velocity *= 1.01f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
    }
}
