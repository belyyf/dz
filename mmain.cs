// задание 2
public abstract class Reservation
{
    public int ReservationID { get; set; }
    public string CustomerName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public abstract decimal CalculatePrice();

    public virtual void DisplayDetails()
    {
        Console.WriteLine($"Бронирование #{ReservationID} для {CustomerName} с {StartDate.ToShortDateString()} по {EndDate.ToShortDateString()}");
    }
}

public class HotelReservation : Reservation
{
    public string RoomType { get; set; }
    public string MealPlan { get; set; }

    public override decimal CalculatePrice() => 100m * (decimal)(EndDate - StartDate).TotalDays;
}

public class FlightReservation : Reservation
{
    public string DepartureAirport { get; set; }
    public string ArrivalAirport { get; set; }

    public override decimal CalculatePrice() => 300m;
}

public class CarRentalReservation : Reservation
{
    public string CarType { get; set; }
    public bool InsuranceOptions { get; set; }

    public override decimal CalculatePrice() => (InsuranceOptions ? 80m : 50m) * (decimal)(EndDate - StartDate).TotalDays;
}

public class BookingSystem
{
    private List<Reservation> reservations = new List<Reservation>();
    private int nextId = 1;

    public void CreateReservation(Reservation reservation)
    {
        reservation.ReservationID = nextId++;
        reservations.Add(reservation);
        Console.WriteLine("Бронирование создано.");
    }

    public void CancelReservation(int reservationID)
    {
        reservations.RemoveAll(r => r.ReservationID == reservationID);
        Console.WriteLine("Бронирование отменено.");
    }

    public decimal GetTotalBookingValue() =>
        reservations.Sum(r => r.CalculatePrice());
}
