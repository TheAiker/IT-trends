﻿using System.ComponentModel.DataAnnotations;

namespace testapp.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber{ get; set; }

    }
}
