using Microsoft.EntityFrameworkCore.Migrations;

namespace BoiGhor.DataAccess.Migrations
{
    public partial class AddStoredProcForCoverType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                                CREATE PROC [dbo].[GetCoverTypesData] 
                                @Id int,
                                @Name varchar(100),
                                @action varchar(2)
                                AS 
                                --**software** 
                                --exec GetCoverTypesData_temp '','','g'
                                If @action='g'
                                    begin
		                                If @Id=0
			                                begin
				                                SELECT * FROM   dbo.CoverTypes
			                                end

		                                Else
			                                begin
				                                SELECT * FROM   dbo.CoverTypes  WHERE  (Id = @Id) 
			                                end
	                                end
                                If @action='u'
                                    begin 
                                          UPDATE dbo.CoverTypes SET  Name = @Name WHERE  Id = @Id                           
                                    end

                                If @action='a'
                                     begin 
                                          INSERT INTO dbo.CoverTypes(Name) VALUES (@Name)
                                     end

                                Else 
                                     begin
	                                     DELETE FROM dbo.CoverTypes WHERE  Id = @Id
	                                 end
                  ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE GetCoverTypesData ");
        }
    }
}
