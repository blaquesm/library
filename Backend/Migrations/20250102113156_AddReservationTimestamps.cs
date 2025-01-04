using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationTimestamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservationDate",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "ReturnDate",
                table: "Reservations",
                newName: "ReturnedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "IssuedAt",
                table: "Reservations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservedAt",
                table: "Reservations",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssuedAt",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservedAt",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "ReturnedAt",
                table: "Reservations",
                newName: "ReturnDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Reservations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationDate",
                table: "Reservations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
