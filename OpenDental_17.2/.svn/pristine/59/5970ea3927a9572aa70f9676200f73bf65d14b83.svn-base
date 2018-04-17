using CodeBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading;

namespace OpenDentBusiness {
	public class ReportsComplex {

		///<summary>Gets a table of data using normal permissions.</summary>
		public static DataTable GetTable(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),command);
			}
			return Db.GetTable(command);
		}

		///<summary>Wrapper method to call the passed-in func in a seperate thread connected to the reporting server.
		///The return type of this function is whatever the return type of the method you passed in is.
		///Throws an exception if anything went wrong executing func within the thread.</summary>
		public static T RunFuncOnReportServer<T>(Func<T> func) {
			Exception ex=null;
			ODThread threadGetTable = new ODThread(new ODThread.WorkerDelegate((ODThread o) => {
				DataAction.Run(() => { o.Tag=func(); } //set the tag to the func's output.
					,ConnectionNames.DentalOfficeReportServer); //run on the report server. if not set up, automatically runs on the current server.
			}));
			threadGetTable.AddExceptionHandler(new ODThread.ExceptionDelegate((Exception e) => {
				ex=e;
			}));
			threadGetTable.Name="ReportComplexGetTableThread";
			threadGetTable.Start(true);
			threadGetTable.Join(Timeout.Infinite);
			//Now that we are back on the main thread, it is now safe to throw exceptions.
			if(ex!=null) {
				throw ex;
			}
			return (T)threadGetTable.Tag;
		}

	}
}
