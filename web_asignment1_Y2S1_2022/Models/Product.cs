using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace web_asignment1_Y2S1_2022.Models
{
    public class Product
    {
        [DisplayName("Product Id")]
        public int ProductId { get; set; }

        // Make sure that Product Name is not blank
        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Product Name cannot be left blank!")]
        [MaxLength(255, ErrorMessage = "Maximum Length is only 255 characters!")]
        public string ProductName { get; set; }

        [DisplayName("Product Image")]
        [MaxLength(255, ErrorMessage = "Maximum Length is only 255 characters!")]
        public string ProductImage { get; set; }

        // Exclude GST
        [DisplayName("Product Price")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Price cannot be left blank")]
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        [Range(20, 500, ErrorMessage = "Price must be between S$20 and S$500 (inclusive)")]
        public decimal Price { get; set; }

        // Make sure that the date is not blank and the date is formatted correcly
        [DisplayName("Effective Date")]
        [Required(ErrorMessage = "Effective cannot be left blank!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EffectiveDate { get; set; }

        [DisplayName("Is Obsolete")]
        public bool isObsolete { get; set; }

        // Provide Default Constructor for ProductViewModel
        public Product() { }

        public Product(int productId)
        {
            SqlCommand sqlCommand = SQL.conn.CreateCommand();
            sqlCommand.CommandText = $"SELECT * FROM Product WHERE ProductID = {productId}";
            SQL.conn.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    ProductId = reader.GetInt32(0);
                    ProductName = reader.GetString(1);
                    ProductImage = reader.IsDBNull(2) ? null : reader.GetString(2);
                    Price = reader.GetDecimal(3);
                    EffectiveDate = reader.GetDateTime(4);
                    isObsolete = "1" == reader.GetString(5) ? true : false;
                }
            }
            reader.Close();
            SQL.conn.Close();
        }

        public Product(SqlDataReader reader)
        {
            ProductId = reader.GetInt32(0);
            ProductName = reader.GetString(1);
            ProductImage = reader.IsDBNull(2) ? "" : reader.GetString(2);
            Price = reader.GetDecimal(3);
            EffectiveDate = reader.GetDateTime(4);
            isObsolete = "1" == reader.GetString(5) ? true : false;
        }

        // Get All the Products from the database
        public static List<Product> getProducts()
        {
            SqlCommand cmd = SQL.conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Product ORDER BY EffectiveDate";
            SQL.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Product> productList = new List<Product>();
            while (reader.Read())
            {
                productList.Add(new Product(reader));
            }
            SQL.conn.Close();
            return productList;
        }

        // Update the product to the database
        public void updateProduct()
        {
            SqlCommand sqlCommand = SQL.conn.CreateCommand();
            sqlCommand.CommandText = @"UPDATE Product SET ProductTitle=@productName, ProductImage=@productImage,"+ 
                @" Price=@price, EffectiveDate=@effectiveDate, Obsolete=@isObsolete WHERE ProductID=@productId";
            sqlCommand.Parameters.AddWithValue("@productName", ProductName);
            sqlCommand.Parameters.AddWithValue("@productImage", (object)ProductImage ?? DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@price", Price);
            sqlCommand.Parameters.AddWithValue("@effectiveDate", EffectiveDate);
            sqlCommand.Parameters.AddWithValue("@isObsolete", isObsolete ? "1" : "0");
            sqlCommand.Parameters.AddWithValue("@productId", ProductId);

            SQL.conn.Open();
            sqlCommand.ExecuteNonQuery();
            SQL.conn.Close();
        }

        // Add new product to the database
        public int addNewProduct()
        {
            SqlCommand sqlCommand = SQL.conn.CreateCommand();
            sqlCommand.CommandText = @"INSERT INTO Product (ProductTitle, ProductImage, Price, EffectiveDate, Obsolete) OUTPUT INSERTED.ProductID" +
                " VALUES(@productName, @productImage, @price, @effectiveDate, @isObsolete)";
            sqlCommand.Parameters.AddWithValue("@productName", ProductName);
            sqlCommand.Parameters.AddWithValue("@productImage", (object)ProductImage ?? DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@price", Price);
            sqlCommand.Parameters.AddWithValue("@effectiveDate", EffectiveDate);
            sqlCommand.Parameters.AddWithValue("@isObsolete", isObsolete ? "1" : "0");

            SQL.conn.Open();
            int productId = (int)sqlCommand.ExecuteScalar();
            SQL.conn.Close();
            return productId;
        }
    }
}