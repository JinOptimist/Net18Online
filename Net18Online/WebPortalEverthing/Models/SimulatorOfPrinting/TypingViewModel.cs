using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPortalEverthing.Models.SimulatorOfPrinting
{
    public class TypingViewModel
    {
    public string CurrentText { get; set; }
    public string UserInput { get; set; }
    public string Result { get; set; }
    public TimeSpan ElapsedTime { get; set; }
    }
}