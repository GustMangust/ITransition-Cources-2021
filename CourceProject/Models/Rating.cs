using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourceProject.Models
{
    public class Rating
    {
    public int Id { get; set; }
    public string UserId { get; set; }
    public int Mark { get; set; }
    public int FanficId { get; set; }
  }
}
