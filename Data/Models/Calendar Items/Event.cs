using Heron.MudCalendar;
using MudBlazor;

namespace Calendar_Items
{
    public class Event : CalendarItem
    {
        public int EventId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public Color Color { get; set; }
        public static Color DefaultEventColor { get; } = Color.Primary;
        public static Color UserRegisteredEventColor { get; } = Color.Tertiary;
    }
}
