using System;
using System.Collections.Generic;

namespace MobAnnouncApp.Models
{
    public struct Menu
    {
        public string Welcome { get; set; }
        public List<Item> GeneralItems { get; set; }
        public List<Item> ProfileItems { get; set; }
        public List<Item> ActionItems { get; set; }

        public struct Item
        {
            public string Title { get; set; }
            public Type TargetType { get; set; }
            public bool IsPage { get; set; }
        }
    }
}
