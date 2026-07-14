using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace InventoryManagementSystem
{
    public partial class Form1 : Form
    {
        private DataTable productTable = new DataTable();

        private void UpdateTotalInventoryValue()
        {
            decimal total = 0;

            foreach (DataRow row in productTable.Rows)
            {
                total += Convert.ToDecimal(row["Total Value"]);
            }

            lblTotalValue.Text = $"$ {total:0.00}";
        }
        private void ClearInputFields()
        {
            txtProductId.Clear();
            txtProductName.Clear();
            cmbCategory.SelectedIndex = -1;
            txtQuantity.Clear();
            txtUnitPrice.Clear();

            txtProductId.Focus();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to update.",
                                "No Product Selected",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                return;
            }

            string productId = txtProductId.Text.Trim();
            string productName = txtProductName.Text.Trim();
            string quantityText = txtQuantity.Text.Trim();
            string unitPriceText = txtUnitPrice.Text.Trim();

            if (productId == "")
            {
                MessageBox.Show("Please enter the Product ID.",
                                "Missing Product ID",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtProductId.Focus();
                return;
            }

            if (productName == "")
            {
                MessageBox.Show("Please enter the Product Name.",
                                "Missing Product Name",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtProductName.Focus();
                return;
            }

            if (cmbCategory.SelectedIndex < 0 && cmbCategory.Text == "")
            {
                MessageBox.Show("Please select a category.",
                                "Missing Category",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                cmbCategory.Focus();
                return;
            }

            if (!int.TryParse(quantityText, out int quantity))
            {
                MessageBox.Show("Please enter a valid quantity.",
                                "Invalid Quantity",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtQuantity.Focus();
                return;
            }

            if (quantity < 0)
            {
                MessageBox.Show("Quantity cannot be negative.",
                                "Invalid Quantity",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtQuantity.Focus();
                return;
            }

            if (!decimal.TryParse(unitPriceText, out decimal unitPrice))
            {
                MessageBox.Show("Please enter a valid unit price.",
                                "Invalid Unit Price",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtUnitPrice.Focus();
                return;
            }

            if (unitPrice < 0)
            {
                MessageBox.Show("Unit price cannot be negative.",
                                "Invalid Unit Price",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtUnitPrice.Focus();
                return;
            }

            string category = cmbCategory.Text;
            decimal totalValue = quantity * unitPrice;

            int rowIndex = dgvProducts.SelectedRows[0].Index;

            productTable.Rows[rowIndex]["Product ID"] = productId;
            productTable.Rows[rowIndex]["Product Name"] = productName;
            productTable.Rows[rowIndex]["Category"] = category;
            productTable.Rows[rowIndex]["Quantity"] = quantity;
            productTable.Rows[rowIndex]["Unit Price"] = unitPrice;
            productTable.Rows[rowIndex]["Total Value"] = totalValue;

            UpdateTotalInventoryValue();
            ClearInputFields();

            MessageBox.Show("Product updated successfully.",
                            "Product Updated",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbCategory.Items.Add("Computer");
            cmbCategory.Items.Add("Stationery");
            cmbCategory.Items.Add("Electronics");
            cmbCategory.Items.Add("Furniture");
            cmbCategory.Items.Add("Accessories");
            cmbCategory.Items.Add("Others");

            cmbCategory.SelectedIndex = -1;

            productTable.Columns.Add("Product ID", typeof(string));
            productTable.Columns.Add("Product Name", typeof(string));
            productTable.Columns.Add("Category", typeof(string));
            productTable.Columns.Add("Quantity", typeof(int));
            productTable.Columns.Add("Unit Price", typeof(decimal));
            productTable.Columns.Add("Total Value", typeof(decimal));

            dgvProducts.DataSource = productTable;

            dgvProducts.Columns["Unit Price"].DefaultCellStyle.Format = "0.00";
            dgvProducts.Columns["Total Value"].DefaultCellStyle.Format = "0.00";

            txtProductId.Focus();

            UpdateTotalInventoryValue();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string productId = txtProductId.Text.Trim();
            string productName = txtProductName.Text.Trim();
            string quantityText = txtQuantity.Text.Trim();
            string unitPriceText = txtUnitPrice.Text.Trim();

            if (productId == "")
            {
                MessageBox.Show("Please enter the Product ID.",
                                "Missing Product ID",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtProductId.Focus();
                return;
            }

            if (productName == "")
            {
                MessageBox.Show("Please enter the Product Name.",
                                "Missing Product Name",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtProductName.Focus();
                return;
            }

            if (cmbCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a category.",
                                "Missing Category",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                cmbCategory.Focus();
                return;
            }

            if (!int.TryParse(quantityText, out int quantity))
            {
                MessageBox.Show("Please enter a valid quantity.",
                                "Invalid Quantity",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtQuantity.Focus();
                return;
            }

            if (quantity < 0)
            {
                MessageBox.Show("Quantity cannot be negative.",
                                "Invalid Quantity",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtQuantity.Focus();
                return;
            }

            if (!decimal.TryParse(unitPriceText, out decimal unitPrice))
            {
                MessageBox.Show("Please enter a valid unit price.",
                                "Invalid Unit Price",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtUnitPrice.Focus();
                return;
            }

            if (unitPrice < 0)
            {
                MessageBox.Show("Unit price cannot be negative.",
                                "Invalid Unit Price",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtUnitPrice.Focus();
                return;
            }

            string category = cmbCategory.SelectedItem.ToString();
            decimal totalValue = quantity * unitPrice;

            productTable.Rows.Add(productId, productName, category, quantity, unitPrice, totalValue);

            UpdateTotalInventoryValue();
            ClearInputFields();

            MessageBox.Show("Product added successfully.",
                            "Product Added",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

        }

        private void btnClearInput_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvProducts.Rows[e.RowIndex];

            txtProductId.Text = row.Cells["Product ID"].Value.ToString();
            txtProductName.Text = row.Cells["Product Name"].Value.ToString();
            cmbCategory.Text = row.Cells["Category"].Value.ToString();
            txtQuantity.Text = row.Cells["Quantity"].Value.ToString();
            txtUnitPrice.Text = row.Cells["Unit Price"].Value.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to delete.",
                                "No Product Selected",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete the selected product?",
                                                  "Confirm Delete",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int rowIndex = dgvProducts.SelectedRows[0].Index;

                productTable.Rows.RemoveAt(rowIndex);

                UpdateTotalInventoryValue();
                ClearInputFields();

                MessageBox.Show("Product deleted successfully.",
                                "Product Deleted",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (productTable.Rows.Count == 0)
            {
                MessageBox.Show("There are no products to clear.",
                                "Empty Inventory",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to clear all products?",
                                                  "Confirm Clear All",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                productTable.Rows.Clear();

                UpdateTotalInventoryValue();
                ClearInputFields();

                MessageBox.Show("All product records have been cleared.",
                                "Inventory Cleared",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            string message = "Are you sure you want to exit?";

            if (productTable.Rows.Count > 0)
            {
                message = "You have product records that are not saved permanently.\n\nAre you sure you want to exit?";
            }

            DialogResult result = MessageBox.Show(message,
                                                  "Confirm Exit",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }

        }
    }
}
