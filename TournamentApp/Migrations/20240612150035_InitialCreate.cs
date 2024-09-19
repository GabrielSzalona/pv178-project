using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TournamentApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameStrategies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameStrategyType = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStrategies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    TournamentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameStrategyId = table.Column<int>(type: "int", nullable: false),
                    Started = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.TournamentId);
                    table.ForeignKey(
                        name: "FK_Tournaments_GameStrategies_GameStrategyId",
                        column: x => x.GameStrategyId,
                        principalTable: "GameStrategies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Members = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Losses = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Teams_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTests",
                columns: table => new
                {
                    MatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    T1Id = table.Column<int>(type: "int", nullable: true),
                    T2Id = table.Column<int>(type: "int", nullable: true),
                    Score1 = table.Column<int>(type: "int", nullable: false),
                    Score2 = table.Column<int>(type: "int", nullable: false),
                    WinnerId = table.Column<int>(type: "int", nullable: true),
                    IsWinner = table.Column<bool>(type: "bit", nullable: false),
                    PrevMatch1Id = table.Column<int>(type: "int", nullable: true),
                    PrevMatch2Id = table.Column<int>(type: "int", nullable: true),
                    Round = table.Column<int>(type: "int", nullable: false),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    IsFinal = table.Column<bool>(type: "bit", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTests", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_MatchTests_MatchTests_PrevMatch1Id",
                        column: x => x.PrevMatch1Id,
                        principalTable: "MatchTests",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchTests_MatchTests_PrevMatch2Id",
                        column: x => x.PrevMatch2Id,
                        principalTable: "MatchTests",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchTests_Teams_T1Id",
                        column: x => x.T1Id,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchTests_Teams_T2Id",
                        column: x => x.T2Id,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchTests_Teams_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchTests_Tournaments_TourId",
                        column: x => x.TourId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchTests_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId");
                });

            migrationBuilder.InsertData(
                table: "GameStrategies",
                columns: new[] { "Id", "GameStrategyType", "Name" },
                values: new object[,]
                {
                    { 1, "RoundRobin", "Round Robin" },
                    { 2, "Brackets", "Brackets" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchTests_PrevMatch1Id",
                table: "MatchTests",
                column: "PrevMatch1Id");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTests_PrevMatch2Id",
                table: "MatchTests",
                column: "PrevMatch2Id");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTests_T1Id",
                table: "MatchTests",
                column: "T1Id");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTests_T2Id",
                table: "MatchTests",
                column: "T2Id");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTests_TourId",
                table: "MatchTests",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTests_TournamentId",
                table: "MatchTests",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTests_WinnerId",
                table: "MatchTests",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TournamentId",
                table: "Teams",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_GameStrategyId",
                table: "Tournaments",
                column: "GameStrategyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchTests");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "GameStrategies");
        }
    }
}
