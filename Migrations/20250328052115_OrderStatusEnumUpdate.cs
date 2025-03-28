using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cheez_ims_api.Migrations
{
    /// <inheritdoc />
    public partial class OrderStatusEnumUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:activity_type", "create_order,create_product,create_supplier,low_stock_product,restock_product,shipped_order")
                .Annotation("Npgsql:Enum:order_status", "canceled,delivered,pending,processing,returned,shipped")
                .Annotation("Npgsql:Enum:payment_method", "bitcoin,cash,credit_card")
                .Annotation("Npgsql:Enum:payment_status", "paid,pending,refunded")
                .Annotation("Npgsql:Enum:product_status", "discontinued,in_stock,low_stock,out_of_stock")
                .OldAnnotation("Npgsql:Enum:activity_type", "create_order,create_product,create_supplier,low_stock_product,restock_product,shipped_order")
                .OldAnnotation("Npgsql:Enum:order_status", "canceled,delivered,pending,returned,shipped")
                .OldAnnotation("Npgsql:Enum:payment_method", "bitcoin,cash,credit_card")
                .OldAnnotation("Npgsql:Enum:payment_status", "paid,pending,refunded")
                .OldAnnotation("Npgsql:Enum:product_status", "discontinued,in_stock,low_stock,out_of_stock");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:activity_type", "create_order,create_product,create_supplier,low_stock_product,restock_product,shipped_order")
                .Annotation("Npgsql:Enum:order_status", "canceled,delivered,pending,returned,shipped")
                .Annotation("Npgsql:Enum:payment_method", "bitcoin,cash,credit_card")
                .Annotation("Npgsql:Enum:payment_status", "paid,pending,refunded")
                .Annotation("Npgsql:Enum:product_status", "discontinued,in_stock,low_stock,out_of_stock")
                .OldAnnotation("Npgsql:Enum:activity_type", "create_order,create_product,create_supplier,low_stock_product,restock_product,shipped_order")
                .OldAnnotation("Npgsql:Enum:order_status", "canceled,delivered,pending,processing,returned,shipped")
                .OldAnnotation("Npgsql:Enum:payment_method", "bitcoin,cash,credit_card")
                .OldAnnotation("Npgsql:Enum:payment_status", "paid,pending,refunded")
                .OldAnnotation("Npgsql:Enum:product_status", "discontinued,in_stock,low_stock,out_of_stock");
        }
    }
}
