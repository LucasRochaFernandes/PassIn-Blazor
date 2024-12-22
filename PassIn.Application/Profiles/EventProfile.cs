using AutoMapper;
using PassIn.Infrastructure.Entities;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<RequestEventJson, Event>()
                .ForMember(
                    dest=> dest.Slug,
                    opt=> opt.MapFrom(src=> src.Title.ToLower().Replace(" ", "-"))
                ).ForMember(
                    dest=> dest.Maximum_Attendees,
                    opt => opt.MapFrom(src=> src.MaximumAttendees));
        CreateMap<Event, ResponseEventJson>();
    }
}
