using HackathonGlass_2019.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HackathonGlass_2019.Controllers
{
    public class ProductAccountsController : ApiController
    {
        private const string ImageGeneratorPath = @"C:\Retalix\Hackathon\LiveImages";
        private const string BarcodeFilePath = @"C:\Retalix\Hackathon\ItemsIDs";

        [HttpGet]
        public ProductAccount GetProductAccounts([FromUri]int accountId, [FromUri]string imageString)
        {
            GenerateImage(imageString);
            System.Threading.SpinWait.SpinUntil(() => Directory.GetFiles(BarcodeFilePath).Length > 0);
            var barcode = GetBarcode();
            var productAccount = GenerateProductAccount(accountId, barcode);
            return productAccount;
        }

        private ProductAccount GenerateProductAccount(int accountId, string barcode)
        {
            var productAccount = new ProductAccount();
            var query = string.Format("SELECT p.*, ac.CategoryId FROM Products p JOIN ProductsCategories pc ON p.ProductId=pc.ProductId JOIN AccountsCategories ac ON pc.CategoryId=ac.CategoryId WHERE P.ProductId={0} AND ac.AccountId={1}", barcode, accountId);
            var dataBaseHandler = new DataBaseHandler();
            var dt = dataBaseHandler.RunQuery(query);

            if (dt.Rows.Count == 0) return null;

            productAccount.ProductId = Int32.Parse(barcode);
            productAccount.ProductName = dt.Rows[0].ItemArray[1].ToString();
            productAccount.SalePrice = (decimal)dt.Rows[0].ItemArray[2];
            var categories = new List<int>();
            foreach (DataRow row in dt.Rows)
            {
                categories.Add((Int32)row.ItemArray[3]);
            }
            productAccount.Categories = categories;
            return productAccount;
        }

        private string GetBarcode()
        {
            var files = Directory.GetFiles(BarcodeFilePath);
            var barcode = Path.GetFileName(files[0]).Split('.')[0].TrimStart(new Char[] { '0' });
            File.Delete(files[0]);
            return barcode;
        }

        private void GenerateImage(string imageString)
        {
            var productAccount = new ProductAccount();
            var image = GenerateImageFromString(imageString);
            var filePath = Path.Combine(ImageGeneratorPath, "imageToFind.jpg");
            image.Save(filePath);
        }

        public Image GenerateImageFromString(string imageString)
        {
            byte[] bytes = Convert.FromBase64String(imageString);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }
            return image;
        }

    }
}
