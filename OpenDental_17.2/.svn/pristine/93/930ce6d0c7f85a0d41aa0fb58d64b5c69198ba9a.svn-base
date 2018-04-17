using CodeBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
//using OpenDentBusiness;

namespace OpenDentBusiness {
	///<summary>Handles database commands related to the VersionRelease table in the db.</summary>
	public class VersionReleases {
		///<summary>Contains a list of versions that are to never be released to the public.  Currently, only the Major and Minor version numbers are considered.</summary>
		private static List<Version> _versionsBlackList=new List<Version> { new Version(4,9),new Version(13,3), };

		///<summary></summary>
		public static List<VersionRelease> Refresh() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<VersionRelease>>(MethodBase.GetCurrentMethod());
			}
			//Create an ODThread so that we can safely change the database connection settings without affecting the calling method's connection.
			ODThread odThread=new ODThread(new ODThread.WorkerDelegate((ODThread o) => {
				//Always set the thread static database connection variables to set the serviceshq db conn.
#if DEBUG
				new DataConnection().SetDbT("localhost","bugs","root","","","",DatabaseType.MySql,true);
#else
				new DataConnection().SetDbT("server","bugs","root","","","",DatabaseType.MySql,true);
#endif
				string command="SELECT * FROM versionrelease ";
				//Exclude black listed versions.
				for(int i=0;i<_versionsBlackList.Count;i++) {
					if(i==0) {
						command+="WHERE ";
					}
					else {
						command+="AND ";
					}
					command+="(MajorNum,MinorNum) != ("+POut.Int(_versionsBlackList[i].Major)+","+POut.Int(_versionsBlackList[i].Minor)+") ";
				}
				command+="ORDER BY MajorNum DESC,MinorNum DESC,BuildNum DESC,IsForeign";
				o.Tag=RefreshAndFill(command);
			}));
			odThread.AddExceptionHandler(new ODThread.ExceptionDelegate((Exception e) => { }));//Do nothing
			odThread.Name="versionGetterThread";
			odThread.Start(true);
			if(!odThread.Join(2000)) { //Give this thread up to 2 seconds to complete.
				return null;
			}
			return (List<VersionRelease>)odThread.Tag;
		}

		private static List<VersionRelease> RefreshAndFill(string command) {
			DataTable table=Db.GetTable(command);
			List<VersionRelease> retVal=new List<VersionRelease>();
			VersionRelease vers;
			for(int i=0;i<table.Rows.Count;i++) {
				vers=new VersionRelease();
				vers.VersionReleaseId= PIn.Int(table.Rows[i][0].ToString());
				vers.MajorNum        = PIn.Int(table.Rows[i][1].ToString());
				vers.MinorNum        = PIn.Int(table.Rows[i][2].ToString());
				vers.BuildNum        = PIn.Int(table.Rows[i][3].ToString());
				vers.IsForeign       = PIn.Bool(table.Rows[i][4].ToString());
				vers.DateRelease     = PIn.Date(table.Rows[i][5].ToString());
				vers.IsBeta          = PIn.Bool(table.Rows[i][6].ToString());
				retVal.Add(vers);
			}
			return retVal;
		}

		///<summary>Returns a fully formatted string of the most recent unreleased versions, from 1 to 3.</summary>
		public static string GetLastReleases(int versionsRequested) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),versionsRequested);
			}
			//Create an ODThread so that we can safely change the database connection settings without affecting the calling method's connection.
			ODThread odThread=new ODThread(new ODThread.WorkerDelegate((ODThread o) => {
				//Always set the thread static database connection variables to set the serviceshq db conn.
#if DEBUG
				new DataConnection().SetDbT("localhost","bugs","root","","","",DatabaseType.MySql,true);
#else
				new DataConnection().SetDbT("server","bugs","root","","","",DatabaseType.MySql,true);
#endif
				string command="SELECT * FROM versionrelease "
					+"WHERE DateRelease < '1880-01-01' "//we are only interested in non-released versions.
					+"AND IsForeign=0 ";
				//Exclude black listed versions.
				for(int i=0;i<_versionsBlackList.Count;i++) {
					command+="AND (MajorNum,MinorNum) != ("+POut.Int(_versionsBlackList[i].Major)+","+POut.Int(_versionsBlackList[i].Minor)+") ";
				}
				command+="ORDER BY MajorNum DESC,MinorNum DESC,BuildNum DESC "
					+"LIMIT 3";//Might not be 3.
				List<VersionRelease> releaseList=RefreshAndFill(command);
				string versionsString="";
				if(releaseList.Count>2 && versionsRequested>2) {
					versionsString+=releaseList[2].MajorNum.ToString()+"."+releaseList[2].MinorNum.ToString()+"."+releaseList[2].BuildNum.ToString()+".0";
				}
				if(releaseList.Count>1 && versionsRequested>1) {
					if(versionsString!="") {
						versionsString+=";";
					}
					versionsString+=releaseList[1].MajorNum.ToString()+"."+releaseList[1].MinorNum.ToString()+"."+releaseList[1].BuildNum.ToString()+".0";
				}
				if(releaseList.Count>0){
					if(versionsString!="") {
						versionsString+=";";
					}
					versionsString+=releaseList[0].MajorNum.ToString()+"."+releaseList[0].MinorNum.ToString()+"."+releaseList[0].BuildNum.ToString()+".0";
				}
				o.Tag=versionsString;
			}));
			odThread.AddExceptionHandler(new ODThread.ExceptionDelegate((Exception e) => { }));//Do nothing
			odThread.Name="versionGetterThread";
			odThread.Start(true);
			if(!odThread.Join(2000)) { //Give this thread up to 2 seconds to complete.
				return null;
			}
			return odThread.Tag.ToString();
		}



	}

	


	


}









