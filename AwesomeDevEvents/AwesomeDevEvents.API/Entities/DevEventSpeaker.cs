namespace AwesomeDevEvents.API.Entities
{
    public class DevEventSpeaker
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TalkTitle { get; set; }
        public string TalkDescription { get; set; }
        public string LinKedInProfile { get; set; }
        public Guid DevEventId { get; set; }
    }
}