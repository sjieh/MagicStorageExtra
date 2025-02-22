﻿using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace MagicStorageExtra.Items
{
	public class Locator : StorageItem
	{
		public Point16 location = new Point16(-1, -1);

		public override bool CloneNewInstances => true;

		public override void SetStaticDefaults()
		{
			DisplayName.AddTranslation(GameCulture.Russian, "Локатор");
			DisplayName.AddTranslation(GameCulture.Polish, "Lokalizator");
			DisplayName.AddTranslation(GameCulture.French, "Localisateur");
			DisplayName.AddTranslation(GameCulture.Spanish, "Locador");
			DisplayName.AddTranslation(GameCulture.Chinese, "定位器");

			Tooltip.SetDefault("<right> Storage Heart to store location" + "\n<right> Remote Storage Access to set it");
			Tooltip.AddTranslation(GameCulture.Russian, "<right> по Cердцу Хранилища чтобы запомнить его местоположение" + "\n<right> на Модуль Удаленного Доступа к Хранилищу чтобы привязать его к Сердцу Хранилища");
			Tooltip.AddTranslation(GameCulture.Polish, "<right> na serce jednostki magazynującej, aby zapisać jej lokalizację" + "\n<right> na bezprzewodowe okno dostępu aby je ustawić");
			Tooltip.AddTranslation(GameCulture.French, "<right> le Cœur de Stockage pour enregistrer son emplacement" + "\n<right> le Stockage Éloigné pour le mettre en place");
			Tooltip.AddTranslation(GameCulture.Spanish, "<right> el Corazón de Almacenamiento para registrar su ubicación" + "\n<right> el Acceso de Almacenamiento Remoto para establecerlo" + "\n<right> Stockage Éloigné pour le mettre en place");
			Tooltip.AddTranslation(GameCulture.Chinese, "<right>存储核心可储存其定位点" + "\n<right>远程存储装置以设置其定位点");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 28;
			item.maxStack = 1;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(0, 1);
		}

		public override void ModifyTooltips(List<TooltipLine> lines)
		{
			bool isSet = location.X >= 0 && location.Y >= 0;
			for (int k = 0; k < lines.Count; k++)
				if (isSet && lines[k].mod == "Terraria" && lines[k].Name == "Tooltip0")
				{
					lines[k].text = Language.GetTextValue("Mods.MagicStorageExtra.SetTo", location.X, location.Y);
				}
				else if (!isSet && lines[k].mod == "Terraria" && lines[k].Name == "Tooltip1")
				{
					lines.RemoveAt(k);
					k--;
				}
		}

		public override void AddRecipe(ModItem result)
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MeteoriteBar, 10);
			recipe.AddIngredient(ItemID.Amber, 2);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(result);
			recipe.AddRecipe();
		}

		public override TagCompound Save()
		{
			var tag = new TagCompound();
			tag.Set("X", location.X);
			tag.Set("Y", location.Y);
			return tag;
		}

		public override void Load(TagCompound tag)
		{
			location = new Point16(tag.GetShort("X"), tag.GetShort("Y"));
		}

		public override void NetSend(BinaryWriter writer)
		{
			writer.Write(location.X);
			writer.Write(location.Y);
		}

		public override void NetRecieve(BinaryReader reader)
		{
			location = new Point16(reader.ReadInt16(), reader.ReadInt16());
		}
	}
}
