using Base.Core;
using Base.Defs;
using Base.Levels;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Modding;
using PhoenixPoint.Common.Levels.MapGeneration;
using PhoenixPoint.Common.Entities.Items;
using PhoenixPoint.Geoscape.Entities.PhoenixBases;
using PhoenixPoint.Geoscape.Entities.PhoenixBases.FacilityComponents;
using System.Linq;
using UnityEngine;

namespace ResourceCrateAndFacility
{
	/// <summary>
	/// This is the main mod class. Only one can exist per assembly.
	/// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
	/// </summary>
	public class ResourceCrateAndFacilityMain : ModMain
	{
		/// Config is accessible at any time, if any is declared.
		public new ResourceCrateAndFacilityConfig Config => (ResourceCrateAndFacilityConfig)base.Config;

		/// This property indicates if mod can be Safely Disabled from the game.
		/// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
		/// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled
		public override bool CanSafelyDisable => true;

		/// <summary>
		/// Callback for when mod is enabled. Called even on game starup.
		/// </summary>
		public override void OnModEnabled() {

			/// All mod dependencies are accessible and always loaded.
			int c = Dependencies.Count();
			/// Mods have their own logger. Message through this logger will appear in game console and Unity log file.
			Logger.LogInfo($"Say anything crab people-related.");
			/// Metadata is whatever is written in meta.json
			string v = MetaData.Version.ToString();
			/// Game creates Harmony object for each mod. Accessible if needed.
			HarmonyLib.Harmony harmony = (HarmonyLib.Harmony)HarmonyInstance;
			/// Mod instance is mod's runtime representation in game.
			string id = Instance.ID;
			/// Game creates Game Object for each mod. 
			GameObject go = ModGO;
			/// PhoenixGame is accessible at any time.
			PhoenixGame game = GetGame();

			/// Apply any general game modifications.
			DefRepository Repo = GameUtl.GameComponent<DefRepository>();

			ResourceCrateDef x7materialRC = Repo.GetAllDefs<ResourceCrateDef>().FirstOrDefault(a => a.name.Equals("Materials_ResourceCrateDef"));

			x7materialRC.ResourceAmount = 700;

			ResourceCrateDef x7techRC = Repo.GetAllDefs<ResourceCrateDef>().FirstOrDefault(a => a.name.Equals("Tech_ResourceCrateDef"));

			x7techRC.ResourceAmount = 700;

			ResourceCrateDef x7supplieRC = Repo.GetAllDefs<ResourceCrateDef>().FirstOrDefault(a => a.name.Equals("Supplies_ResourceCrateDef"));

			x7supplieRC.ResourceAmount = 700;

			ItemDef x7material = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("MaterialsPack_ItemDef"));

			x7material.ManufactureMaterials = 700;

			ItemDef x7tech = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("TechPack_ItemDef"));

			x7tech.ManufactureTech = 700;

			//ItemDef x7food = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("FoodPack_ItemDef"));

            //x7food. = 700;

			ItemDef x7mutagen = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("MutagenPack_ItemDef"));

			x7mutagen.ManufactureMutagen = 700;

			HealFacilityComponentDef Heal_x7 = Repo.GetAllDefs<HealFacilityComponentDef>().FirstOrDefault(a => a.name.Equals("E_Heal [MedicalBay_PhoenixFacilityDef]"));
			Heal_x7.BaseHeal = 28;

			HealFacilityComponentDef HealStaminax14 = Repo.GetAllDefs<HealFacilityComponentDef>().FirstOrDefault(a => a.name.Equals("E_Heal [LivingQuarters_PhoenixFacilityDef]"));
			HealStaminax14.BaseStaminaHeal = 28;

			ExperienceFacilityComponentDef Training_x7 = Repo.GetAllDefs<ExperienceFacilityComponentDef>().FirstOrDefault(a => a.name.Equals("E_Experience [TrainingFacility_PhoenixFacilityDef]"));

			Training_x7.SkillPointsPerDay = 375;
			Training_x7.ExperiencePerUser = 14;
			Training_x7.HasWorkerLevelCheck = true;
			Training_x7.WorkerOutputMod = 0.95f;

			ResourceGeneratorFacilityComponentDef Food_x7 = Repo.GetAllDefs<ResourceGeneratorFacilityComponentDef>().FirstOrDefault(a => a.name.Equals("E_ResourceGenerator [FoodProduction_PhoenixFacilityDef]"));

			//Food_x7.BaseResourcesOutput.Values[0] = new PhoenixPoint.Common.Core.ResourceUnit {Type = PhoenixPoint.Common.Core.ResourceType.Supplies, Value = 0.93f };

            Food_x7.BaseResourcesOutput[0] = new PhoenixPoint.Common.Core.ResourceUnit { Type = PhoenixPoint.Common.Core.ResourceType.Supplies, Value = 0.93f};
            Food_x7.WorkerAdditionalOutput[0] = new PhoenixPoint.Common.Core.ResourceUnit { Type = PhoenixPoint.Common.Core.ResourceType.Supplies,  Value = 9};
			//Food_x7.WorkerOutputMod = 0.0f;
		}

		/// <summary>
		/// Callback for when mod is disabled. This will be called even if mod cannot be safely disabled.
		/// Guaranteed to have OnModEnabled before.
		/// </summary>
		public override void OnModDisabled() {
			/// Undo any game modifications if possible. Else "CanSafelyDisable" must be set to false.
			/// ModGO will be destroyed after OnModDisabled.
		}

		/// <summary>
		/// Callback for when any property from mod's config is changed.
		/// </summary>
		public override void OnConfigChanged() {
			/// Config is accessible at any time.
		}


		/// <summary>
		/// In Phoenix Point there can be only one active level at a time. 
		/// Levels go through different states (loading, unloaded, start, etc.).
		/// General puprose level state change callback.
		/// </summary>
		/// <param name="level">Level being changed.</param>
		/// <param name="prevState">Old state of the level.</param>
		/// <param name="state">New state of the level.</param>
		public override void OnLevelStateChanged(Level level, Level.State prevState, Level.State state) {
			/// Alternative way to access current level at any time.
			Level l = GetLevel();
		}

		/// <summary>
		/// Useful callback for when level is loaded, ready, and starts.
		/// Usually game setup is executed.
		/// </summary>
		/// <param name="level">Level that starts.</param>
		public override void OnLevelStart(Level level) {
		}

		/// <summary>
		/// Useful callback for when level is ending, before unloading.
		/// Usually game cleanup is executed.
		/// </summary>
		/// <param name="level">Level that ends.</param>
		public override void OnLevelEnd(Level level) {
		}
	}
}
