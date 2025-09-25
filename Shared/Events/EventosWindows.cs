using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class EventosWindows
    {
        System.Diagnostics.EventLog eventLog = new();

        public void AgregarEvento(string Mensaje, int id, int tipo)
        {

            var source = Constantes.strEvento;
            if (!System.Diagnostics.EventLog.SourceExists(source))
            {
                System.Diagnostics.EventLog.CreateEventSource(source, "Application");
            }

            // Set the source name for writing log entries.
            eventLog.Source = source;

            // Create an event ID to add to the event log
            var tEvento = System.Diagnostics.EventLogEntryType.Information;
            switch (tipo)
            {
                case 1:
                    tEvento = System.Diagnostics.EventLogEntryType.Error;
                    break;
                case 2:
                    tEvento = System.Diagnostics.EventLogEntryType.Warning;
                    break;
                case 4:
                    tEvento = System.Diagnostics.EventLogEntryType.Information;
                    break;
                case 8:
                    tEvento = System.Diagnostics.EventLogEntryType.SuccessAudit;
                    break;
                case 16:
                    tEvento = System.Diagnostics.EventLogEntryType.FailureAudit;
                    break;
            }
            // Write an entry to the event log.
            eventLog.WriteEntry(Mensaje, tEvento, id);

            // Close the Event Log
            eventLog.Close();

        }

        public enum TipoEvento
        {
            Error = 1,
            FailureAudit = 16,
            Information = 4,
            SuccessAudit = 8,
            Warning = 2
        }

        public enum IdEvento
        {
            ErrorProcesarTag = 2,
        }

    }
}
