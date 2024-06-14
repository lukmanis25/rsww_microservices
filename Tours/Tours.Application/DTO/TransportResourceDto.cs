namespace Tours.Application.DTO;

public class TransportResourceDto
{
    public Guid TransportResourceId { get; set; }
    public int SeatNumber { get; set; }
    public int ReservedSeatNumber { get; set; }
    public float Price { get; set; }
}