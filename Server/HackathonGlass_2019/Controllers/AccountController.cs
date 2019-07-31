using HackathonGlass_2019.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HackathonGlass_2019.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet]
        public Account GetAccountDetails([FromUri]int accountId)
        {
            Account account = new Account { AccountId = accountId };
            var query = string.Format("SELECT * FROM ACCOUNTS WHERE ACCOUNTID={0}", accountId);
            var dataBaseHandler = new DataBaseHandler();
            var dt = dataBaseHandler.RunQuery(query);

            if (dt.Rows.Count == 1)
            {
                account.FirstName = dt.Rows[0].ItemArray[1].ToString();
                account.LastName = dt.Rows[0].ItemArray[2].ToString();
                FillAccountCategoryDetails(account);
                return account;
            }
            account.FirstName = "Empty";
            account.LastName = "Empty";
            return account;
        }

        private void FillAccountCategoryDetails(Account account)
        {
            var query = string.Format("SELECT * FROM ACCOUNTSCATEGORIES WHERE ACCOUNTID={0}", account.AccountId);
            var dataBaseHandler = new DataBaseHandler();
            var dt = dataBaseHandler.RunQuery(query);
            var categoryList = new List<int>();
            foreach (DataRow row in dt.Rows)
            {
                categoryList.Add((Int32)row.ItemArray[1]);
            }
            account.CategoryList = categoryList;
        }
    }
}
