using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary.DataAccess.Entites
{
    public class BookEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public string Publisher { get; set; } = string.Empty;

        public bool IsBooked { get; set; } = false;

    }
}
