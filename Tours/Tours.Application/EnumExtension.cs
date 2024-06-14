using Tours.Application.Events;

namespace Tours.Application;

public static class EnumExtension
{
    public static string RoomTypeAsString(this RoomType roomType)
    {
        return roomType switch
        {
            RoomType.Small => "Small",
            RoomType.Medium => "Medium",
            RoomType.Large => "Large",
            RoomType.Apartment => "Apartment",
            RoomType.Studio => "Studio",
            _ => string.Empty
        };
    }

    public static RoomType RoomTypeFromString(this string roomType)
    {
        return roomType switch
        {
            "Small" => RoomType.Small,
            "Medium" => RoomType.Medium,
            "Large" => RoomType.Large,
            "Apartment" => RoomType.Apartment,
            "Studio" => RoomType.Studio,
            _ => RoomType.Small
        };
    }
}