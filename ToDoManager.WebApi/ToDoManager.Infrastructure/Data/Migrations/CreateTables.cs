using FluentMigrator;

namespace ToDoManager.Infrastructure.Data.Migrations
{
    [Migration(20250317)] // Use um timestamp ou uma sequência para controle das migrações
    public class CreateTables : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                .WithColumn("Username").AsString(100).NotNullable().Unique()
                .WithColumn("PasswordHash").AsString(256).NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);

            Create.Table("Tasks")
                .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                .WithColumn("UserId").AsGuid().NotNullable().ForeignKey("Users", "Id")
                .WithColumn("Title").AsString(255).NotNullable()
                .WithColumn("Description").AsString(int.MaxValue).Nullable()
                .WithColumn("IsCompleted").AsBoolean().WithDefaultValue(false)
                .WithColumn("CreatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);
        }

        public override void Down()
        {
            Delete.Table("Tasks");
            Delete.Table("Users");
        }
    }

}
