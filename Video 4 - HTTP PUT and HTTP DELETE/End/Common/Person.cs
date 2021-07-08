using System;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
