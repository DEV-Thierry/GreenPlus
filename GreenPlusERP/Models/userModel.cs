﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPlusERP.Models
{
    [Table("User")]
    public class userModel
    {
        public string Id {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Cargo { get; set; }
        public string Grupo{ get; set; }
        public string Tipo{ get; set; }
    }
}
