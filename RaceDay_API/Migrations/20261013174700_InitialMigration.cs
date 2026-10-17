using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaceDay_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Events_ID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Event_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Event_Description = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Event_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Event_Location = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Event_Type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Event_Distance = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Events__E143B6CB91BF4A31", x => x.Events_ID);
                });

            migrationBuilder.CreateTable(
                name: "User_Role",
                columns: table => new
                {
                    User_Role_ID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    User_Role_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    User_FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    User_LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_Rol__134E48EC9C6062B9", x => x.User_Role_ID);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Category_ID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Events_ID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Age_Category = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Distance_Category = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__6DB38D4E66E57DCD", x => x.Category_ID);
                    table.ForeignKey(
                        name: "FK__Category__Events__251C81ED",
                        column: x => x.Events_ID,
                        principalTable: "Events",
                        principalColumn: "Events_ID");
                });

            migrationBuilder.CreateTable(
                name: "User_Profile",
                columns: table => new
                {
                    User_Profile_ID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    User_Role_ID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    User_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    User_Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    User_Age = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Phone_Number = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Registration_Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_Pro__C69B990E39B5B264", x => x.User_Profile_ID);
                    table.ForeignKey(
                        name: "FK__User_Prof__User___336AA144",
                        column: x => x.User_Role_ID,
                        principalTable: "User_Role",
                        principalColumn: "User_Role_ID");
                });

            migrationBuilder.CreateTable(
                name: "Event_Enrolment",
                columns: table => new
                {
                    Event_Enrolment_ID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Events_ID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Category_ID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    User_Profile_ID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Event_En__5D541F9D9A3047F9", x => x.Event_Enrolment_ID);
                    table.ForeignKey(
                        name: "FK__Event_Enr__Categ__39237A9A",
                        column: x => x.Category_ID,
                        principalTable: "Category",
                        principalColumn: "Category_ID");
                    table.ForeignKey(
                        name: "FK__Event_Enr__Event__382F5661",
                        column: x => x.Events_ID,
                        principalTable: "Events",
                        principalColumn: "Events_ID");
                    table.ForeignKey(
                        name: "FK__Event_Enr__User___3A179ED3",
                        column: x => x.User_Profile_ID,
                        principalTable: "User_Profile",
                        principalColumn: "User_Profile_ID");
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Results_ID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Finish_Times = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Finishing_Positions = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Event_Enrolment_ID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Results__63E95BDD3EAA4169", x => x.Results_ID);
                    table.ForeignKey(
                        name: "FK__Results__Event_E__3CF40B7E",
                        column: x => x.Event_Enrolment_ID,
                        principalTable: "Event_Enrolment",
                        principalColumn: "Event_Enrolment_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_Events_ID",
                table: "Category",
                column: "Events_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Enrolment_Category_ID",
                table: "Event_Enrolment",
                column: "Category_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Enrolment_Events_ID",
                table: "Event_Enrolment",
                column: "Events_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Enrolment_User_Profile_ID",
                table: "Event_Enrolment",
                column: "User_Profile_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Results_Event_Enrolment_ID",
                table: "Results",
                column: "Event_Enrolment_ID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Profile_User_Role_ID",
                table: "User_Profile",
                column: "User_Role_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__User_Pro__681E8A607DCF2056",
                table: "User_Profile",
                column: "User_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__User_Pro__A9D1053433225967",
                table: "User_Profile",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Event_Enrolment");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User_Profile");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "User_Role");
        }
    }
}
