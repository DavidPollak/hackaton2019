using HackathonGlass_2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;

namespace HackathonGlass_2019.Controllers
{
    public class CategoriesController : ApiController
    {
        [HttpGet]
        public List<Category> GetCategories()
        {
            var categories = new List<Category>();
            var query = "SELECT * FROM CATEGORIES";
            var dataBaseHandler = new DataBaseHandler();
            var dt = dataBaseHandler.RunQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                categories.Add(new Category { CategoryId=(Int32)row.ItemArray[0], CategoryName = row.ItemArray[1].ToString() });
            }
            return categories;
        }
    }
}
