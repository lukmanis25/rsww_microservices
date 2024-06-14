namespace Tours.Application.DTO;

public class TourDto
{
    public Guid Id { get; set; }
    public DateTime StartDatetime { get; set; }
    public DateTime EndDatetime { get; set; }
    public string DestinationPlace { get; set; }
    public string DeparturePlace { get; set; }
    public HotelResourceDto HotelResource { get; set; }
    public TransportResourceDto TransportToResource { get; set; }
    public TransportResourceDto TransportBackResource { get; set; }
}