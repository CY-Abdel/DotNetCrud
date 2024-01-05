using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketsCRUD.Core.Context;
using TicketsCRUD.Core.DTO;
using TicketsCRUD.Core.Entities;

namespace TicketsCRUD.Controllers
{
    [Route("TicketAPI/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        // on a besoin de BDD, on l'injecte en utilisant le constructeur
        // on a besoin de autoMapper on l'inject dans le constructeur
        private readonly ApplicationDbContext _context;

        // ce mapper viens de la ligne dans program.cs
        // builder.Services.AddAutoMapper(typeof(AutoMapperConfigProfil));
        private readonly IMapper _mapper;

        public TicketsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // CRUD

        // Create
        [HttpPost]
        [Route("create")]

        // Déclaration d'une action asynchrone appelée "CreateTicket" dans un contrôleur ASP.NET Core
        //le type de retour IActionResult indique que la méthode peut retourner différents types de résultats HTTP, tels que OkResult (200 OK), BadRequestResult (400 Bad Request), NotFoundResult (404 Not Found), etc. 
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto createTicketDto)
        {
            var newTicket = new Ticket();

            _mapper.Map(createTicketDto, newTicket);

            // Ajouter le nouvel objet Ticket à la base de données de manière asynchrone
            await _context.Tickets.AddAsync(newTicket);
            // Enregistrer les modifications de la base de données de manière asynchrone
            await _context.SaveChangesAsync();

            // Retourner une réponse HTTP 200 OK avec un message succès
            return Ok("le ticket a été enregistrer avec succes");
        }

        // Read All
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTicketDto>>> GetTickets(string? q)
        {
            // check si on a un parametre de recherche
            IQueryable<Ticket> query = _context.Tickets;

            if (q is not null)
            {
                query = query.Where(t => t.PassengerName.Contains(q));
            }

            // Récupérer tous les tickets de la base de données de manière asynchrone
            //var tickets = await _context.Tickets.ToListAsync();
            var tickets = await query.ToListAsync();

            // Mapper la liste de tickets vers une liste de DTO (Data Transfer Object)
            var convertedTickets = _mapper.Map<IEnumerable<GetTicketDto>>(tickets);

            return Ok(convertedTickets);
        }


        // Read By Id
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GetTicketDto>> GetTicketById([FromRoute] long id)
        {
            // Récupérer le premier ticket correspondant à l'identifiant spécifié de manière asynchrone
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
            if (ticket is null)
            {
                return NotFound("ticket not found");
            }

            var convertedTicket = _mapper.Map<GetTicketDto>(ticket);

            return Ok(convertedTicket);
        }

        // Update
        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditTicket([FromRoute] long id, [FromBody] UpdateTicketDto updateTicketDto)
        {
            // Récupérer le premier ticket correspondant à l'identifiant spécifié de manière asynchrone
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
            if (ticket is null)
            {
                return NotFound("ticket not found");
            }

            _mapper.Map(updateTicketDto, ticket);
            ticket.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok("ticket modifié avec succès");

        }

        // Delete
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteTask([FromRoute] long id)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket is null)
            {
                return NotFound("ticket introuvable");
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return Ok("Le Ticket a été supprimé!");
        }
    }
}
