using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uwp_Sqlite.Models
{
    public class Todo
    {
        public Todo()
        {

        }
        public Todo(long id, string customer, string title, string description, string status, DateTime created)
        {
            Id = id;
            Customer = customer;
            Title = title;
            Description = description;
            Status = status;
            Created = created;
        }
        public long Id { get; set; }
        public string Customer { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
    }

    }


