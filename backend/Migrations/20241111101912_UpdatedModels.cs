using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {   
            migrationBuilder.Sql("DELETE FROM \"Tasks\";");
            migrationBuilder.Sql("ALTER TABLE \"Tasks\" ALTER COLUMN \"Id\" DROP IDENTITY;");
            migrationBuilder.Sql("ALTER TABLE \"Tasks\" ALTER COLUMN \"Id\" TYPE uuid USING gen_random_uuid();");
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Tasks",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            // migrationBuilder.AlterColumn<Guid>(
                // name: "Id",
                // table: "Tasks",
                // type: "uuid",
                // nullable: false,
                // oldClrType: typeof(int),
                // oldType: "integer")
                // .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
                
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"Tasks\" ALTER COLUMN \"Id\" SET DATA TYPE integer;");
            migrationBuilder.Sql("ALTER TABLE \"Tasks\" ALTER COLUMN \"Id\" ADD IDENTITY;");
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Tasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            // migrationBuilder.AlterColumn<int>(
            //     name: "Id",
            //     table: "Tasks",
            //     type: "integer",
            //     nullable: false,
            //     oldClrType: typeof(Guid),
            //     oldType: "uuid")
            //     .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}
