using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace userApi.Migrations
{
    /// <inheritdoc />
    public partial class firstuser : Migration
    {
        /// <inheritdoc />
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData("AspNetUsers",
             new string[]{ "Id",
                             "UserName",
                              "NormalizedUserName",
                              "Email",
                              "NormalizedEmail",
                              "EmailConfirmed",
                              "PasswordHash",
                              "SecurityStamp",
                              "ConcurrencyStamp",
                              "PhoneNumber",
                              "PhoneNumberConfirmed",
                              "TwoFactorEnabled",
                              "LockoutEnd",
                              "LockoutEnabled",
                              "AccessFailedCount"
                          },

             new object[] {
                   "7a98e5a2-de61-4803-ab57-5a432742549d",
                   "osmargv100@gmail.com",
                   "OSMARGV100@GMAIL.COM",
                   "osmargv100@gmail.com",
                   "OSMARGV100@GMAIL.COM",
                   true,
                   "AQAAAAEAACcQAAAAELA5CUjdismATH7+1NrzT4v+BPME2aUi2eEjDIyBOdaHrbvu+GeUmkFwEiuHvO6I9Q==",
                   "MLVZFNZTPJ7ZWF3CPJOR7QQPSJCMLJ2W",
                   "cd67a3fe-8700-4287-8f2d-961c884fdf15",
                   null,
                   false,
                   false,
                   null,
                   true,
                   0

                 }
              );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("AspNetUsers", "Id", "7a98e5a2-de61-4803-ab57-5a432742549d");
        }
    }
}
