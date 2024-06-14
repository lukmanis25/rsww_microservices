namespace Tours.Application.DTO;

public class HotelResourceDto
{
    public Guid HotelResourceId { get; set; }
    public string HotelName { get; set; }
    public List<RoomDto> Rooms { get; set; }
}