﻿namespace CMSWebAPI
{
    public class UserDetailModel
    {
        public int UserId { get; set; }        
    }


    public class UserInfo
    {        
        public int UserId { get; set; }        
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
    }
}
