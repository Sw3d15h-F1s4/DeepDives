using Microsoft.UI.Xaml;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using DeepDives.Models;
using System.Diagnostics;
using System.Text.Json;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DeepDives
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.AppWindow.Resize(new(500, 700));
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(CustomDragRegion);
        }

        static async Task<DRGResponseViewModel?> GetDives()
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("https://drgapi.com"),
            };

            try
            {
                DRGResponse? response = await client.GetFromJsonAsync<DRGResponse>("/v1/deepdives");
                return new DRGResponseViewModel(response ?? new());
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine("[ERROR]: HTTP Request Exception:");
                Debug.WriteLine(ex.Message);
                return null;
            }
            catch (NotSupportedException ex)
            {
                Debug.WriteLine("[ERROR]: Not Supported Exception: Content Type is not valid");
                Debug.WriteLine(ex.Message);
                return null;
            }
            catch (JsonException ex)
            {
                Debug.WriteLine("[ERROR]: JSON Exception");
                Debug.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex) 
            {
                Debug.WriteLine("[ERROR]: Unknown Exception");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        private async void TabMain_Loaded(object sender, RoutedEventArgs e)
        {
            // :clown: imagine having a ui framework that can't resize windows :clown: :clown:
            var scale = this.Content.XamlRoot.RasterizationScale;
            this.AppWindow.Resize(new(Convert.ToInt32(500 * scale), Convert.ToInt32(700 * scale)));
            var response = await GetDives();
            TabMain.TabItemsSource = response?.Variants;
            TabMain.SelectedIndex = 0;
        }

        private DRGResponseViewModel GetTestData()
        {
            var response = new DRGResponse()
            {
                StartTime = "Now!",
                EndTime = "Later!",
                Variants = new()
                {
                    { GetRandomDive() },
                    { GetRandomDive() },
                }
            };
            
            return new DRGResponseViewModel(response);
        }

        private DeepDive GetRandomDive()
        {
            return new DeepDive()
            {
                Type = "Example Deep Dive",
                Biome = GetRandomBiome(),
                Name = GetRandomName(),
                Stages = new()
                {
                    { GetRandomStage() },
                    { GetRandomStage() },
                    { GetRandomStage() },
                }
            };
        }

        private string GetRandomName()
        {
            var rng = new Random();
            return rng.Next(5) switch
            {
                0 => "Hunter's Nightmare",
                1 => "Nathan's Sandwich",
                2 => "Evelyn's Plight",
                3 => "Karl's Wet Dream",
                4 => "The Death of Sam",
                _ => "Nathan calls this one the sandwich."
            };
        }

        private Stage GetRandomStage()
        {
            return new Stage()
            {
                Primary = GetRandomMissionType(),
                Secondary = GetRandomMissionType(),
                Warning = GetRandomWarning(),
                Anomaly = GetRandomAnomaly(),
            };
        }
        
        private string GetRandomBiome()
        {
            var rng = new Random();
            return rng.Next(10) switch
            {
                0 => "Crystalline Caverns",
                1 => "Salt Pits",
                2 => "Fungus Bogs",
                3 => "Radioactive Exclusion Zone",
                4 => "Dense Biozone",
                5 => "Glacial Strata",
                6 => "Hollow Bough",
                7 => "Azure Weald",
                8 => "Magma Core",
                9 => "Sandblasted Corridors",
                _ => "God has forsaken us, the RNG generated a value greater than 9. Intellisense is in shambles."
            };
        }
        private string GetRandomMissionType()
        {
            var rng = new Random();
            return rng.Next(9) switch
            {
                0 => "Black Box",
                1 => "Egg x100",
                2 => "Dreadnought x450 (Bruh)",
                3 => "Escort Duty",
                4 => "Morkite x999",
                5 => "On-Site Refining",
                6 => "Industrial Sabotage",
                7 => "Mule xMorbillion",
                8 => "Aquarq xToo Many",
                _ => "The end is nigh. The RNG generated a value greater than 8."
            };
        }
        private string GetRandomWarning()
        {
            var rng = new Random();
            return rng.Next(13) switch
            {
                0 => "Cave Leech Cluster",
                1 => "Elite Threat",
                2 => "Haunted Cave",
                3 => "Lethal Enemies",
                4 => "Low Oxygen",
                5 => "Mactera Plague",
                6 => "Parasites",
                7 => "Regenerative Bugs",
                8 => "Rival Presence",
                9 => "Shield Disruption",
                10 => "Swarmageddon",
                11 => "Lithophage Outbreak",
                12 => "Exploder Infestation",
                _ => "The value was greater than 12. Bill Gates shudders at the possibility"
            };
        }
        private string GetRandomAnomaly()
        {
            var rng = new Random();
            return rng.Next(8) switch
            {
                0 => "Critical Weakness",
                1 => "Double XP",
                2 => "Gold Rush",
                3 => "Low Gravity",
                4 => "Mineral Mania",
                5 => "Rich Atmosphere",
                6 => "Volatile Guts",
                7 => "Golden Bugs",
                _ => "The value was greater than 7. Clearly, a cosmic ray caused a bit shift."
            };
        }

        private async void RefreshItem(object sender, RoutedEventArgs e)
        {
            var response = await GetDives();
            TabMain.TabItemsSource = response?.Variants;
            TabMain.SelectedIndex = 0;
        }
    }
}
