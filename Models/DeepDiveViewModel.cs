using Microsoft.UI.Xaml;
using System.Collections.Generic;

namespace DeepDives.Models
{
    internal class DeepDiveViewModel
    {
        public DeepDiveViewModel(DeepDive model)
        {
            Model = model;
            Stages = new();

            if (Model.Stages != null)
            {
                foreach (var stage in Model.Stages)
                {
                    Stages.Add(new(stage));
                }
            }
        }

        public DeepDive Model;
        public string Type { get => Model.Type ?? ""; }
        public string Name { get => Model.Name ?? ""; }
        public string Biome { get => Model.Biome ?? ""; }

        public List<StageViewModel> Stages;

        public string BiomeImage
        {
            get => Biome switch
            {
                "Crystalline Caverns" => "/Assets/BiomeImages/CrystallineCaverns.png",
                "Salt Pits" => "/Assets/BiomeImages/SaltPits.png",
                "Fungus Bogs" => "/Assets/BiomeImages/FungusBogs.png",
                "Radioactive Exclusion Zone" => "/Assets/BiomeImages/RadioactiveExclusionZone.png",
                "Dense Biozone" => "/Assets/BiomeImages/DenseBiozone.png",
                "Glacial Strata" => "/Assets/BiomeImages/GlacialStrata.png",
                "Hollow Bough" => "/Assets/BiomeImages/HollowBough.png" ,
                "Azure Weald" => "/Assets/BiomeImages/AzureWeald.png",
                "Magma Core" => "/Assets/BiomeImages/MagmaCore.png",
                "Sandblasted Corridors" => "/Assets/BiomeImages/SandblastedCorridors.png",
                _ => "/Assets/Biome/CrystallineCaverns.png"
            };
        }
    }

    internal class StageViewModel
    {
        public StageViewModel(Stage model)
        {
            Model = model;
        }

        public Stage Model;
        public string Primary { get => Model.Primary ?? ""; }
        public string Secondary { get => Model.Secondary ?? ""; }
        public string Anomaly { get => Model.Anomaly ?? ""; }
        public string Warning { get => Model.Warning ?? ""; }

        public string PrimaryIcon
        {
            get => GetMissionIcon(Primary);
        }
        public string SecondaryIcon
        {
            get => GetMissionIcon(Secondary);
        }
        public string WarningIcon
        {
            get => Warning switch
            {
                "Cave Leech Cluster" => "/Assets/WarningIcons/CaveLeechCluster.png",
                "Elite Threat" => "/Assets/WarningIcons/EliteThreat.png",
                "Exploder Infestation" => "/Assets/WarningIcons/ExploderInfestation.png",
                "Haunted Cave" => "/Assets/WarningIcons/HauntedCave.png",
                "Lethal Enemies" => "/Assets/WarningIcons/LethalEnemies.png",
                "Low Oxygen" => "/Assets/WarningIcons/LowOxygen.png",
                "Mactera Plague" => "/Assets/WarningIcons/MacteraPlague.png",
                "Parasites" => "/Assets/WarningIcons/Parasites.png",
                "Regenerative Bugs" => "/Assets/WarningIcons/RegenerativeBugs.png",
                "Rival Presence" => "/Assets/WarningIcons/RivalPresence.png",
                "Shield Disruption" => "/Assets/WarningIcons/ShieldDisruption.png",
                "Swarmageddon" => "/Assets/WarningIcons/Swarmageddon.png",
                "Lithophage Outbreak" => "/Assets/WarningIcons/LithophageOutbreak.png",
                _ => "/Assets/WarningIcons/CaveLeechCluster.png",
            };
        }

        public string AnomalyIcon
        {
            get => Anomaly switch
            {
                "Critical Weakness" => "/Assets/AnomalyIcons/CriticalWeakness.png",
                "Double XP" => "/Assets/AnomalyIcons/DoubleXP.png",
                "Gold Rush" => "/Assets/AnomalyIcons/GoldRush.png",
                "Golden Bugs" => "/Assets/AnomalyIcons/GoldenBugs.png",
                "Low Gravity" => "/Assets/AnomalyIcons/LowGravity.png",
                "Mineral Mania" => "/Assets/AnomalyIcons/MineralMania.png",
                "Rich Atmosphere" => "/Assets/AnomalyIcons/RichAtmosphere.png",
                "Volatile Guts" => "/Assets/AnomalyIcons/VolatileGuts.png",
                _ => "/Assets/AnomalyIcons/CriticalWeakness.png",
            };
        }

        public Visibility WarningIconVisibility
        {
            get
            {
                if (string.IsNullOrEmpty(Warning))
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }
        public Visibility AnomalyIconVisibility
        {
            get
            {
                if (string.IsNullOrEmpty(Anomaly))
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        // Allowed Values:
        //Black Box
        //Egg
        //Dreadnought
        //Escort Duty
        //Morkite
        //On-Site Refining
        //Industrial Sabotage
        //Mule
        //Aquarq
        private string GetMissionIcon(string name)
        {
            return name.Split(' ')[0] switch
            {
                "Black" => "/Assets/MissionIcons/BlackBox.png",
                "Egg" => "/Assets/MissionIcons/Eggs.png",
                "Dreadnought" => "/Assets/MissionIcons/Elimination.png",
                "Escort" => "/Assets/MissionIcons/Escort.png",
                "Morkite" => "/Assets/MissionIcons/Mining.png",
                "On-Site" => "/Assets/MissionIcons/Refining.png",
                "Industrial" => "/Assets/MissionIcons/Sabotage.png",
                "Mule" => "/Assets/MissionIcons/Salvage.png",
                "Aquarq" => "/Assets/MissionIcons/Extraction.png",
                _ => "/Assets/MissionIcons/Mining.png",
            };
        }
    }

    internal class DRGResponseViewModel
    {
        public DRGResponseViewModel(DRGResponse response)
        {
            Model = response;
            Variants = new();

            if (Model.Variants != null)
            {
                foreach (var variant in Model.Variants)
                {
                    Variants.Add(new(variant));
                }
            }
        }

        public DRGResponse Model;
        public List<DeepDiveViewModel> Variants;
    }
}
