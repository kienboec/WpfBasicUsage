namespace WpfBasicUsage.Models {
    public class MediaLog {
        public int Id { get; set; }
        public string LogText { get; set; }
        public MediaItem LogMediaItem { get; set; }

        public MediaLog(int id, string logText, MediaItem loggedItem) {
            this.Id = id;
            this.LogText = logText;
            this.LogMediaItem = loggedItem;
        }
    }
}
