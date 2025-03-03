using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanCQRS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial_MigrationDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "TblExceptionLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblExceptionLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblProvidedServices",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblProvidedServices", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "TblRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblUser",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OTPGeneratedUTCTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsOtpVerified = table.Column<bool>(type: "bit", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUser", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_TblUser_TblRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TblRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblDoctor",
                columns: table => new
                {
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointmentFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfessionalBackground = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Certifications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PMCNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PracticingTenure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfessionMembership = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblDoctor", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK_TblDoctor_TblUser_UserId",
                        column: x => x.UserId,
                        principalTable: "TblUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblPatient",
                columns: table => new
                {
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPatient", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_TblPatient_TblUser_UserId",
                        column: x => x.UserId,
                        principalTable: "TblUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblDoctorAvailability",
                columns: table => new
                {
                    AvailabilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: true),
                    SpecificDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsRepeatable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblDoctorAvailability", x => x.AvailabilityId);
                    table.ForeignKey(
                        name: "FK_TblDoctorAvailability_TblDoctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "TblDoctor",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblDoctorService",
                columns: table => new
                {
                    DoctorServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblDoctorService", x => x.DoctorServiceId);
                    table.ForeignKey(
                        name: "FK_TblDoctorService_TblDoctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "TblDoctor",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblDoctorService_TblProvidedServices_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "TblProvidedServices",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblAppointment",
                columns: table => new
                {
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvailabilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblAppointment", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_TblAppointment_TblDoctorAvailability_AvailabilityId",
                        column: x => x.AvailabilityId,
                        principalTable: "TblDoctorAvailability",
                        principalColumn: "AvailabilityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TblAppointment_TblPatient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "TblPatient",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TblAppointmentMedia",
                columns: table => new
                {
                    AppointmentMediaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblAppointmentMedia", x => x.AppointmentMediaId);
                    table.ForeignKey(
                        name: "FK_TblAppointmentMedia_TblAppointment_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "TblAppointment",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblAppointment_AvailabilityId",
                table: "TblAppointment",
                column: "AvailabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_TblAppointment_PatientId",
                table: "TblAppointment",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_TblAppointmentMedia_AppointmentId",
                table: "TblAppointmentMedia",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TblDoctor_UserId",
                table: "TblDoctor",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblDoctorAvailability_DoctorId",
                table: "TblDoctorAvailability",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_TblDoctorService_DoctorId",
                table: "TblDoctorService",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_TblDoctorService_ServiceId",
                table: "TblDoctorService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TblPatient_UserId",
                table: "TblPatient",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblUser_RoleId",
                table: "TblUser",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "TblAppointmentMedia");

            migrationBuilder.DropTable(
                name: "TblDoctorService");

            migrationBuilder.DropTable(
                name: "TblExceptionLogs");

            migrationBuilder.DropTable(
                name: "TblAppointment");

            migrationBuilder.DropTable(
                name: "TblProvidedServices");

            migrationBuilder.DropTable(
                name: "TblDoctorAvailability");

            migrationBuilder.DropTable(
                name: "TblPatient");

            migrationBuilder.DropTable(
                name: "TblDoctor");

            migrationBuilder.DropTable(
                name: "TblUser");

            migrationBuilder.DropTable(
                name: "TblRole");
        }
    }
}
