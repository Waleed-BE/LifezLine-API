using CleanCQRS.Core.Dtos.Appointment;
using CleanCQRS.Core.Dtos.AppointmentMedia;
using CleanCQRS.Core.Interfaces;
using CleanCQRS.Core.IService;
using MediatR;

namespace CleanCQRS.Application.Command
{
    public record CreateAppointmentCommand(AddAppointmentDto addAppointmentDto) : IRequest<AddAppointmentResponseDto>;

    public class AddAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IAppointmentMediaRepository appointmentMediaRepository,
        IAppointmentMediaUploadService appointmentMediaUploadService ,IMediator mediator) : IRequestHandler<CreateAppointmentCommand, AddAppointmentResponseDto>
    {
        public async Task<AddAppointmentResponseDto> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointmentScheduleResponse = await appointmentRepository.ScheduleAppointment(request.addAppointmentDto);
            var urlPath = await appointmentMediaUploadService.uploadMedia(request.addAppointmentDto.mediaFile);

            AddAppointmentMediaDto addAppointmentMediaDto = new AddAppointmentMediaDto()
            {
                AppointmentId = appointmentScheduleResponse.AppointmentId,
                AppointmentMediaPath = urlPath,
            };
            
            var appointmentMediaResponse = await appointmentMediaRepository.AddAppointmentMedia(addAppointmentMediaDto);
            return appointmentScheduleResponse;
        }
    }
}
