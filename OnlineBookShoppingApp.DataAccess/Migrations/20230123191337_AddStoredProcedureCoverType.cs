using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBookShoppingApp.DataAccess.Migrations
{
    public partial class AddStoredProcedureCoverType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE GetCoverTypes
                                   AS
                                  select * from CoverTypes");
            migrationBuilder.Sql(@"CREATE PROCEDURE GetCoverType
                                 @Id int
                                   AS
                                  select * from CoverTypes where Id=@Id");
            migrationBuilder.Sql(@"CREATE PROCEDURE CreateCoverType
                                   @Name varchar(50)
                                   AS
                                  insert CoverTypes values(@Name)");
            migrationBuilder.Sql(@"CREATE PROCEDURE UpdateCoverType
                                    @Id int,
                                    @Name varchar(50)
                                   AS
                                  update CoverTypes set Name=@Name where Id=@Id");
            migrationBuilder.Sql(@"CREATE PROCEDURE DeleteCoverTypes
                                  @Id int
                                   AS
                                  delete CoverTypes where Id=@Id");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
