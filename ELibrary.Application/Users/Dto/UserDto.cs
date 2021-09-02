using ELibrary.Application.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Application.Users.Dto
{
    public class UserDto 
    {
        public string Id { get; set; }
        public string[] Roles { get; set; }
        public string ActingRole { get; set; }
    }
}
