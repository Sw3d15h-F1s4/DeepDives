using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Net.Http;
using Windows.Security.Cryptography.Core;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using System.Net.Http.Json;
using DeepDives.Models;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.UI.Windowing;

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
            AppWindow.Resize(new(600, 800));
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(CustomDragRegion);
            if (AppWindow.Presenter.GetType() == typeof(OverlappedPresenter))
            {
                (AppWindow.Presenter as OverlappedPresenter).IsResizable = false;
            }
        }

        static async Task<DRGResponse?> GetDives()
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("https://drgapi.com"),
            };

            try
            {
                DRGResponse? response = await client.GetFromJsonAsync<DRGResponse>("/v1/deepdives");
                return response;
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

        private void TabMain_Loaded(object sender, RoutedEventArgs e)
        {
            //DRGResponse? Model = await GetDives();

            var viewModel = GetTestData();

            TabMain.TabItemsSource = viewModel.Variants;
        }

        private DRGResponseViewModel GetTestData()
        {
            List<Stage> stages = new()
            {
                new()
                {
                    Primary = "Morkite x150",
                    Secondary = "Black Box",
                    Anomaly = "Critical Weakness",
                    Warning = "Cave Leech Cluster",
                },
                new()
                {
                    Primary = "Eggs x6",
                    Secondary = "Dreadnought x2 (D+T)",
                    Anomaly = "Double XP",
                    Warning = "Elite Threat",
                },
                new()
                {
                    Primary = "Escort Duty",
                    Secondary = "Aquarqs x10",
                    Anomaly = "Golden Bugs",
                    Warning = "Exploder Infestation",
                },
                new()
                {
                    Primary = "On-Site Refining",
                    Secondary = "Industrial Sabotage",
                    Anomaly = "Gold Rush",
                    Warning = "Haunted Cave",
                },
                new()
                {
                    Primary = "Mule",
                    Secondary = "Undefined Type",
                    Anomaly = "Low Gravity",
                    Warning = "Lethal Enemies",
                },
                new()
                {
                    Primary = "Undefined Type",
                    Secondary = "Undefined Type",
                    Anomaly = "Mineral Mania",
                    Warning = "Lithophage Outbreak",
                },
                new()
                {
                    Primary = "Undefined Type",
                    Secondary = "Undefined Type",
                    Anomaly = "Rich Atmosphere",
                    Warning = "Low Oxygen",
                },
                new()
                {
                    Anomaly = "Volatile Guts",
                    Warning = "Mactera Plague",
                },
                new()
                {
                    Warning = "Parasites",
                },
                new()
                {
                    Warning = "Regenerative Bugs",
                },
                new()
                {
                    Warning = "Rival Presence",
                },
                new()
                {
                    Warning = "Shield Disruption",
                },
                new()
                {
                    Warning = "Swarmageddon",
                },
                new()
                {
                    Primary = "No Warning",
                },
            };

            List<DeepDive> dives = new()
            {
                new() {
                   Type = "Deep Dive",
                   Name = "Hunter's Nightmare",
                   Biome = "Azure Weald",
                   Stages = GetTestMissions(stages),
                },
                new() {
                   Type = "Elite Deep Dive",
                   Name = "Nathan's Sandwich",
                   Biome = "Crystalline Caverns",
                   Stages = GetTestMissions(stages),
                },
                new() {
                   Type = "Eliter Deep Dive",
                   Name = "A Pity.",
                   Biome = "Dense Biozone",
                   Stages = GetTestMissions(stages),
                },
                new() {
                   Type = "Elitest Dive",
                   Name = "MUSHROOM",
                   Biome = "Fungus Bogs",
                   Stages = GetTestMissions(stages),
                },
                new() {
                   Type = "Eliteerest Deep Dive",
                   Name = "Sam's House",
                   Biome = "Glacial Strata",
                   Stages = GetTestMissions(stages),
                },
                new() {
                   Type = "More Eliterest Deep Dive",
                   Name = "A Pity.",
                   Biome = "Hollow Bough",
                   Stages = GetTestMissions(stages),
                },
                new() {
                   Type = "Most Eliterest Deep Dive",
                   Name = "Instant Death",
                   Biome = "Magma Core",
                   Stages = GetTestMissions(stages),
                },
                new() {
                   Type = "Mostest Eliteerst Dive",
                   Name = "Fat Boy",
                   Biome = "Radioactive Exclusion Zone",
                   Stages = GetTestMissions(stages),
                },
                new() {
                   Type = "Mosterest Eliteerest Deep Dive",
                   Name = "Evelyn's Disaster",
                   Biome = "Salt Pits",
                   Stages = GetTestMissions(stages),
                },
                new() {
                   Type = "Karl's Dive",
                   Name = "The Pharaoh's Curse",
                   Biome = "Sandblasted Corridors",
                   Stages = GetTestMissions(stages),
                },
            };

            var response = new DRGResponse()
            {
                StartTime = "Now!",
                EndTime = "Later!",
                Variants = dives,
            };

            return new DRGResponseViewModel(response);
        }

        private List<Stage> GetTestMissions(List<Stage> pool)
        {
            List<Stage> result = new List<Stage>();
            int size = pool.Count - 1;
            var rng = new Random();
            result.Add(pool[rng.Next(0, size)]);
            result.Add(pool[rng.Next(0, size)]);
            result.Add(pool[rng.Next(0, size)]);

            return result;
        }
    }
}
