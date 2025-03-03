using CleanCQRS.Core.Dtos.Doctor;
using CleanCQRS.Core.Interfaces;
using MediatR;

namespace CleanCQRS.Application.Command
{
    public record CreateDoctorScheduleCommand(List<DoctorAvailabilityDTO> DoctorAvailabilityDtoList) : IRequest<DoctorAvailabilityResponseDTO>;
    public class CreateDoctorScheduleCommandHandler(IDoctorRepository doctorRepository, IDoctorScheduleRepository doctorScheduleRepository, IMediator mediator) : IRequestHandler<CreateDoctorScheduleCommand, DoctorAvailabilityResponseDTO>
    {
        public async Task<DoctorAvailabilityResponseDTO> Handle(CreateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            // Get the existing doctor schedules
            var existingDoctorAvailability = await doctorScheduleRepository.getDoctorAvailabilityAsync(
                request.DoctorAvailabilityDtoList.FirstOrDefault().DoctorId
            );

            List<DoctorAvailabilityDTO> newSchedules = new List<DoctorAvailabilityDTO>();

            foreach (var newSchedule in request.DoctorAvailabilityDtoList)
            {
                var existingSchedule = existingDoctorAvailability.FirstOrDefault(existing =>
                     existing.DoctorId == newSchedule.DoctorId &&
                     existing.DayOfWeek == newSchedule.DayOfWeek &&
                     existing.SpecificDate?.Date == newSchedule.SpecificDate?.Date && // Compare only Date
                     existing.StartTime == newSchedule.StartTime &&
                     existing.EndTime == newSchedule.EndTime
                );


                if (existingSchedule != null)
                {
                    // If the schedule exists but the repeat flag is different, update the existing record
                    if (existingSchedule.IsRepeatable && !newSchedule.IsRepeatable)
                    {
                        existingSchedule.IsRepeatable = false;
                        await doctorRepository.updateDoctorAvailability(existingSchedule);
                    }

                    if (!existingSchedule.IsRepeatable && newSchedule.IsRepeatable)
                    {
                        existingSchedule.IsRepeatable = true;
                        await doctorRepository.updateDoctorAvailability(existingSchedule);
                    }
                }
                else
                {
                    // If it's a completely new schedule, add it to the list for insertion
                    newSchedules.Add(newSchedule);
                }
            }

            // Insert only new schedules that were not found in the DB
            if (newSchedules.Any())
            {
                return await doctorRepository.createDoctorAvailability(newSchedules);
            }
            return new DoctorAvailabilityResponseDTO
            {
                ResponseMessage = "No new schedules were added."
            };
        }
    }
}
