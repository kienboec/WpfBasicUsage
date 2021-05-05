using System;

namespace WpfBasicUsage.Models {
    public class MediaItem {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Annotation { get; set; }
        public DateTime CreationTime { get; set; }

        public MediaItem(int id, string name, string annotation, string url, DateTime creationTime) {
            this.Id = id;
            this.Name = name;
            this.Annotation = annotation;
            this.Url = url;
            this.CreationTime = creationTime;
        }
    }
}
