using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
  public class PagerModel
  {
    public PagerModel()
    {

    }

    public int CurrentPage { get; set; }
    public int EndIndex { get; set; }
    public int EndPage { get; set; }
    public int PageSize { get; set; }
    public List<int> Pages { get; set; } = new List<int>();
    public int StartIndex { get; set; }
    public int StartPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
  }
}
