using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepDives.Models
{

    public record class DRGResponse
    (
        string? StartTime = null,
        string? EndTime = null,
        List<DeepDive>? Variants = null
    );
    public record class DeepDive
    (
        string? Type = null,
        string? Name = null,
        string? Biome = null,
        List<Stage>? Stages = null
    );

    public record class Stage
    (
        int Id = 0,
        string Primary = "",
        string Secondary = "",
        string Anomaly = "",
        string Warning = ""
    );
}
