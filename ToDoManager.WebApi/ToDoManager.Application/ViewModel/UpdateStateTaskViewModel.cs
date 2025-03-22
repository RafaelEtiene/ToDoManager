using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoManager.Application.ViewModel
{
    public class UpdateStateTaskViewModel
    {
        public Guid Id { get; set; }
        public bool IsCompleted { get; set; }
    }
}
