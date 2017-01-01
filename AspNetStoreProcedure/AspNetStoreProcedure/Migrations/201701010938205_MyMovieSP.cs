namespace AspNetStoreProcedure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyMovieSP : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Movie_Insert",
                p => new
                    {
                        Name = p.String(),
                        ReleaseDate = p.DateTime(),
                        Category = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Movies]([Name], [ReleaseDate], [Category])
                      VALUES (@Name, @ReleaseDate, @Category)
                      
                      DECLARE @ID int
                      SELECT @ID = [ID]
                      FROM [dbo].[Movies]
                      WHERE @@ROWCOUNT > 0 AND [ID] = scope_identity()
                      
                      SELECT t0.[ID]
                      FROM [dbo].[Movies] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            CreateStoredProcedure(
                "dbo.Movie_Update",
                p => new
                    {
                        ID = p.Int(),
                        Name = p.String(),
                        ReleaseDate = p.DateTime(),
                        Category = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Movies]
                      SET [Name] = @Name, [ReleaseDate] = @ReleaseDate, [Category] = @Category
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Movie_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Movies]
                      WHERE ([ID] = @ID)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Movie_Delete");
            DropStoredProcedure("dbo.Movie_Update");
            DropStoredProcedure("dbo.Movie_Insert");
        }
    }
}
