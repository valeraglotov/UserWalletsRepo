using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
  public class SortModel
  {
    public SortModel()
    {

    }
    public PagerModel PagerModel { get; set; }
    public string Key { get; set; }
    public string SortedFilter { get; set; }

    public string Search { get; set; }
  }
}
