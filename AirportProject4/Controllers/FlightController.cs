using AutoMapper;
using AirportProject4.CQRS.CQRSContracts;
using AirportProject4.CQRS.Flights.Commends;
using AirportProject4.CQRS.Flights.FlightOrchestor;
using AirportProject4.CQRS.Flights.Quries;
using AirportProject4.CQRS.Tickets.Queries;
using AirportProject4.Project.core.DTOS.FlightsDto;
using AirportProject4.Project.core.DTOS.TicktDto;
using AirportProject4.Project.core.DTOS.ViewModles;
using AirportProject4.Project.core.Entities.main;
using AirportProject4.Project.core.ServiceContrect;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirportProject4.Controllers
{
    public class FlightController : Controller
    {
        private readonly ILogger<FlightController> _logger;
        private readonly IMediator _mediator;
        private readonly IUpdateFlightsCommandOrchestrator updateFlightsCommandOrchestrator;
        private readonly IMapper mapper;
        private readonly IFlightBookOrchestor flightBookOrchestor;

        public FlightController(
            ILogger<FlightController> logger,
            IMediator mediator,
            IUpdateFlightsCommandOrchestrator updateFlightsCommandOrchestrator,
            IMapper mapper,
            IFlightBookOrchestor flightBookOrchestor
            )
        {
            this._logger = logger;
            this._mediator = mediator;
            this.updateFlightsCommandOrchestrator = updateFlightsCommandOrchestrator;
            this.mapper = mapper;
            this.flightBookOrchestor = flightBookOrchestor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllFlights([FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] string? search = null,
            [FromQuery] string? sortBy = "createdAt",
            [FromQuery] string? sortDir = "desc")
        {
            try
            {
                var flights = await _mediator.Send(new GetAllFlightsQuery(pageSize,pageNumber,sortBy,sortDir,search));
                return View(flights);
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ
                _logger.LogError(ex, "Error occurred while getting flights.");

                // ممكن ترجع View فيها رسالة خطأ
                // أو Redirect لصفحة Error عامة
                return View("Error");
            }
        }

        public async Task<IActionResult> GetFlightByFlightNumber(string flightNumber)
        {
            try
            {
                var flight = await _mediator.Send(new GetFlightByFlightNumber(flightNumber));

                if (flight == null)
                {

                    _logger.LogWarning("No flight found with number {FlightNumber}", flightNumber);
                    return NotFound($"No flight found with number {flightNumber}");

                }

                return View("getAvailableFlights", flight);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error occurred while getting flight by number {FlightNumber}", flightNumber);


                return StatusCode(500, "An error occurred while processing your request.");
            }




        }

        public async Task<IActionResult> getAvailableFlights([FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] string? search = null,
            [FromQuery] string? sortBy = "createdAt",
            [FromQuery] string? sortDir = "desc")
        {
            try
            {
                var flights = await _mediator.Send(new GetAvailableFlightOrchestrator(pageSize, pageNumber, sortBy, sortDir, search));

                if (flights == null)
                {
                    _logger.LogWarning("No Available Flights");

                    return NotFound($"No Available Flights");

                }
                return View(flights);

            }
            catch (Exception)
            {

                throw new Exception("error while geting flights") ;
            }

        }
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var flight = await _mediator.Send(new GetFlightbyidQyery(id));

                if (flight == null)
                {
                    _logger.LogWarning("No Available Flights");

                    return NotFound($"No Available Flights");

                }
                var mappedFlight = mapper.Map<UpdateFlightviewmodle>(flight);
                return View(mappedFlight);

            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<IActionResult> FlightUpdate(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var flight = await _mediator.Send(new GetFlightbyidQyery(id));
            var mappedFlight = new UpdateFlightviewmodle
            {
                Id = flight.Id,
                FlightNumber = flight.FlightNumber,
                DepartureAirport = flight.DepartureAirport,
                ArrivalAirport = flight.ArrivalAirport,
                DepartureTime = flight.DepartureTime,
                ArrivalTime = flight.ArrivalTime,
                AirCraftModel = flight.Aircraft?.Model,
                AircraftCapacity = flight.Aircraft?.Capacity,
                AirlineName = flight.Aircraft?.Airline?.Name,
                AirlineCode = flight.Aircraft?.Airline?.Code
            };


            return View(mappedFlight);

        }

        [HttpPost]
        public async Task<IActionResult> FlightUpdate(int? id, UpdateFlightviewmodle updateDto)
        {
            if (!ModelState.IsValid) return View(updateDto);
            try
            {
                if (!id.HasValue) return BadRequest();
                var flight = await updateFlightsCommandOrchestrator.UpdateFlightsAsync(id, updateDto);
                if (flight != null)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    _logger.LogWarning("some thing went wronge while updating");
                    ModelState.AddModelError(string.Empty, "flight is not updated");

                    return View(updateDto);
                }


            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(updateDto);


            }
        }

        public async Task<IActionResult> Book(int id)
        {
            if (id <= 0) return BadRequest();
            var flight = await _mediator.Send(new GetFlightbyidQyery(id));
            if (flight == null) return BadRequest();
            var mappedFlight = new FlightBookviewmodle()
            {
                FlightId = id,
                FlightNumber = flight.FlightNumber,

            };
            return View(mappedFlight);

        }
        [HttpPost]
        public async Task<IActionResult> Book(FlightBookviewmodle FlightBookviewmodle)
        {

            try
            {
                if (!ModelState.IsValid) return View(FlightBookviewmodle);
                var MappedTicket = new CreateTicktDto()
                {
                    FlightId = FlightBookviewmodle.FlightId,
                    SeatType = FlightBookviewmodle.SeatType,
                    PassengerFullName = FlightBookviewmodle.PassengerFullName,
                    PassengerPassportNumber = FlightBookviewmodle.PassengerPassportNumber
                };
                var Tiket = await flightBookOrchestor.FlightBook(MappedTicket);
                if (Tiket == null) ViewBag.ErrorMessage = "❌ there is no sets.";

                //return View(Tiket);

                return RedirectToAction("TicketDetails", new { id = Tiket.Id });
            }
            catch (Exception ex)
            {
                // لو حصل أي خطأ غير متوقع
                ViewBag.ErrorMessage = "⚠ there is no sets";
                // ممكن تسجل الخطأ في Log
                // _logger.LogError(ex, "Booking Error");
                return View(FlightBookviewmodle);

            }


        }

        public async Task<IActionResult> TicketDetails(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var ticket = await _mediator.Send(new GetTicketByidQuery(id));
            if (ticket == null) return NotFound();
            //var mappedTicket = mapper.Map<TicketDetailsViewModel>(ticket);
            return View(ticket);

        }

    }
}
