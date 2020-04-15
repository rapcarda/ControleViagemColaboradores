using System;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModel
{
    public class BaseViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public BaseViewModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
