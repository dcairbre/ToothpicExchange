using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary></summary>
	[Serializable]
	public class UserOdPref:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey = true)]
		public long UserOdPrefNum;
		///<summary>FK to userod.UserNum.</summary>
		public long UserNum;
		///<summary>Foreign key to a table associated with FkeyType.  Can be 0 if the user preference does not need a foreign key.</summary>
		public long Fkey;
		///<summary>Enum:UserOdFkeyType Specifies which flag is overridden for the specified definition, since an individual definition can have multiple flags.</summary>
		public UserOdFkeyType FkeyType;
		///<summary>Used to hold the override, which might be a simple primitive value, a comma separated list, or a complex document in xml.</summary>
		public string ValueString;

		///<summary></summary>
		public UserOdPref Clone() {
			return (UserOdPref)this.MemberwiseClone();
		}
	}

	///<summary>These FKey Types are to be used as an identifier for what table the Fkey column is associated to.
	///This enum is not stored as a string so DO NOT reorder it.</summary>
	public enum UserOdFkeyType {
		///<summary>0</summary>
		Definition,
		///<summary>1</summary>
		ClinicLast,
		///<summary>2 - Wiki home pages use ValueString to store the name of the wiki page instead of Fkey due to how FormWiki loads pages.</summary>
		WikiHomePage,
		///<summary>3 - ValueString will be a comma delimited list of DefNums for the last expanded categories for the user.  When FormAutoNotes loads,
		///these categories will be expanded again.</summary>
		AutoNoteExpandedCats,
		///<summary>4 - Controls whether tasks will be collapsed or not by default</summary>
		TaskCollapse,
		///<summary>5 - When FormCommItem is in Persistent mode, clear the note text box after the user creates a commlog.</summary>
		CommlogPersistClearNote,
		///<summary>6 - When FormCommItem is in Persistent mode, clear the End text box after the user creates a commlog.</summary>
		CommlogPersistClearEndDate,
		///<summary>7 - When FormCommItem is in Persistent mode, update the Date / Time text box with NOW() whenver the patient changes.</summary>
		CommlogPersistUpdateDateTimeWithNewPatient,
		///<summary>8 - Whether or not to display just the currently selected exam in the Perio Chart.</summary>
		PerioCurrentExamOnly,
	}
}