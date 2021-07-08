using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Common.Players;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using MonoMod.Cil;

namespace TrelamiumMusic
{
    public class TrelamiumMusic : Mod
	{
		private bool stopTitleMusic = false;
		private ManualResetEvent titleMusicStopped = new ManualResetEvent(false);
		private int customTitleMusicSlot;
		internal static TrelamiumMusic Instance { get; set; }

		public TrelamiumMusic()
        {
			Instance = this;

			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadSounds = true
			};
		}
		public override void Load()
		{
			if (Main.netMode != NetmodeID.Server)
			{
			}
		}
		public override void PostSetupContent()
		{
			customTitleMusicSlot = GetSoundSlot(SoundType.Music, "Sounds/Music/IlluminantInkiness");
			IL.Terraria.Main.UpdateAudio += il => {
				var c = new ILCursor(il);
				c.GotoNext(MoveType.After, i => i.MatchLdfld<Main>("newMusic"));
				c.EmitDelegate<Func<int, int>>(newMusic => newMusic == MusicID.Title ? customTitleMusicSlot : newMusic);
			};
		}
		public override void Close()
		{
			if (customTitleMusicSlot > 0)
			{
				stopTitleMusic = true;
				titleMusicStopped.WaitOne();
			}
			base.Close();
		}
		public override void UpdateMusic(ref int music, ref MusicPriority priority)
		{
			if (stopTitleMusic)
			{
				customTitleMusicSlot = MusicID.Title;
				var m = GetMusic("Sounds/Music/IlluminantInkiness");
				if (m.IsPlaying)
					m.Stop(AudioStopOptions.Immediate);
				titleMusicStopped.Set();
				stopTitleMusic = false;
			}

			if (Main.myPlayer == -1 || Main.gameMenu || !Main.LocalPlayer.active)
			{
				return;
			}
			// Make sure your logic here goes from lowest priority to highest so your intended priority is maintained.
			if (Main.LocalPlayer.ZoneRain && Main.raining)
			{
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/Downpour");
				priority = MusicPriority.BiomeLow;
			}
			
			/*if (Main.LocalPlayer.GetModPlayer<TrelamiumTwo.Common.Players.TrelamiumPlayer>().ZoneDruidsGarden)
			{
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/LarbreDeLaVie");
				priority = MusicPriority.BiomeMedium;
			}*/
			if (NPC.AnyNPCs(ModContent.NPCType<TrelamiumTwo.Content.NPCs.Boss.Fungore.Fungore>()))
			{
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/FungalFracas");
				priority = MusicPriority.BossMedium;
			}
		}
	}
}