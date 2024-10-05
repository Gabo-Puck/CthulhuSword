using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TutorialSword.Content.Projectiles;

namespace TutorialSword.Content.Items
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class TutorialSword : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.TutorialSword.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Melee;
			Item.width = 100;
			Item.height = 100;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shoot = ModContent.ProjectileType<EyeCthulhu>();
			Item.knockBack = 6;
			Item.value = Item.buyPrice(silver: 1);
			Item.rare = ItemRarityID.Blue;
			Item.shootSpeed = 15;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Console.WriteLine("Shoot hook");
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
			float ceilingLimitY = target.X;

			if(ceilingLimitY > player.Center.X - 200f)
			{
				ceilingLimitY = player.Center.X - 200f;
			}

			for (int i = 0; i < 3; i++)
			{
				Console.WriteLine("Shoot");
				position = player.Center;
				//position.Y -= 100 * i;
				Vector2 heading = target - position;

				heading.Normalize();
				heading *= velocity.Length();
				//heading.Y += 2;
				Projectile.NewProjectile(source, position, heading, type, damage * 2, knockback, player.whoAmI, 0f, ceilingLimitY);
            }
			return false;
        }
    }
}
