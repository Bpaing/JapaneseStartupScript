using RoutineManager.MVVM.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineManager.MVVM.ViewModel
{
    public class CalendarViewModel : ViewModelBase
    {
        private readonly ICalendarService? _calendarService;
        public CalendarViewModel(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }
    }
}
