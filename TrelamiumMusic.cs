using Terraria;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Players;

namespace TrelamiumMusic
{
    public class TrelamiumMusic : Mod
    {
        public TrelamiumMusic()
        {

        }
        public override void Load()
        {

        }
		public override void UpdateMusic(ref int music, ref MusicPriority priority)
		{
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
			if (NPC.AnyNPCs(ModContent.NPCType<TrelamiumTwo.Content.NPCs.Fungore.Fungore>()))
			{
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/FungalFracas");
				priority = MusicPriority.BossMedium;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<TrelamiumTwo.Content.NPCs.Glacier.Glacier>()))
			{
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/Hyperborean");
				priority = MusicPriority.BossMedium;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<TrelamiumTwo.Content.NPCs.ForestGuardian.ForestGuardian>()))
			{
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/TreeProtector");
				priority = MusicPriority.BossMedium;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<TrelamiumTwo.Content.NPCs.Cumulor.Cumulor>()))
			{
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/FogFulcrum");
				priority = MusicPriority.BossMedium;
			}
		}
	}
}