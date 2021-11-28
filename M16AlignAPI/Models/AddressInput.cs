using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace M16AlignAPI.Models
{
    public class InputAddress
    {
        [JsonProperty("target-location")]    
        public string TargetLocation { get; set; }
    }
}