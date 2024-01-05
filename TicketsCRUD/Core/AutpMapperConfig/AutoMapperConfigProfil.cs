using AutoMapper;
using TicketsCRUD.Controllers;
using TicketsCRUD.Core.DTO;
using TicketsCRUD.Core.Entities;

namespace TicketsCRUD.Core.AutpMapperConfig
{
    public class AutoMapperConfigProfil : Profile
    {
        public AutoMapperConfigProfil()
        {
            // Tickets
            CreateMap<CreateTicketDto, Ticket>();
            CreateMap<Ticket, GetTicketDto>();
            CreateMap<UpdateTicketDto, Ticket>();

        }
    }
}
