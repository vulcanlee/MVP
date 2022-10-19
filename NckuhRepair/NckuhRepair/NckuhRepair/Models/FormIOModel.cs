using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NckuhRepair.Models
{

    public class FormIOModel
    {
        public string display { get; set; }
        public string title { get; set; }
        public List<Component> components { get; set; } = new List<Component>();
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Component
    {
        public bool collapsible { get; set; }
        public string title { get; set; }
        public string key { get; set; }
        public string type { get; set; }
        public string label { get; set; }
        public bool input { get; set; }
        public bool tableView { get; set; }
        public List<Component> components { get; set; } = new List<Component>();
        public string tooltip { get; set; }
        public Data data { get; set; }
        public bool? webcam { get; set; }
        public List<FileType> fileTypes { get; set; } = new List<FileType>();
        public List<Value> values { get; set; } = new List<Value>();
        public bool? disableOnInvalid { get; set; }
        public string widget { get; set; }
        public Validate validate { get; set; }
        public Properties properties { get; set; }
        public bool? autoExpand { get; set; }
        public string Value { get; set; } = "";
    }

    public class FileType
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class Properties
    {
        public string APIEndPoint { get; set; }
    }

    public class Validate
    {
        public bool required { get; set; }
    }
    public class Data
    {
        public List<Value> values { get; set; } = new List<Value>();
    }
    public class Value
    {
        public string label { get; set; }
        public string value { get; set; }
        public string shortcut { get; set; }
        public string select { get; set; } = "";
    }

}