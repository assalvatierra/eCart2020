using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace eCartModels
{
    public class AccountLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AccountRegistration
    {
        public string Username { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public int UserStatusId { get; set; }
        public int MasterCityId { get; set; }
        public int MasterAreaId { get; set; }
        public string Password  { get; set; }
        public string Password2 { get; set; }
    }

    public class StoreRegistration
    {
        public string LoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public int StoreStatusId { get; set; }
        public int StoreCategoryId { get; set; }
        public int MasterCityId { get; set; }
        public int MasterAreaId { get; set; }

    }

    public class RiderRegistration
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public string Mobile { get; set; }
        public string Mobile2 { get; set; }
        public int RiderStatusId { get; set; }
        public int MasterCityId { get; set; }

    }
}