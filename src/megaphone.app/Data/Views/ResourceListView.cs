using Megaphone.App.Data.Representations;
using System.Collections.Generic;
using Humanizer.Localisation;
using System.Linq;
using System;
using Humanizer;

namespace Megaphone.App.Data.Views
{
    public class ResourceListView
    {
        public Dictionary<string, List<ResourceView>> Resources { get; set; } = new();

        public static ResourceListView Make(List<ResourceListRepresentation> list)
        {
            ResourceListView listView = new();

            foreach (var l in list)
            {
                var key = (DateTime.UtcNow - l.Date).Humanize(minUnit: TimeUnit.Day, maxUnit: TimeUnit.Month);
                key = key == "0 days" ? "Today" : key + " ago";

                if (listView.Resources.ContainsKey(key))
                {
                    listView.Resources[key].AddRange(l.Resources.Select(r =>
                    new ResourceView
                    {
                        Display = r.Display,
                        Url = r.Url
                    }));
                }
                else
                {
                    listView.Resources.Add(key, l.Resources.Select(r =>
                    new ResourceView
                    {
                        Display = r.Display,
                        Url = r.Url
                    }).ToList());
                }                
            }

            return listView;
        }
    }

    public class ResourceView
    {
        public string Display { get; set; }
        public string Url { get; set; }
    }
}
