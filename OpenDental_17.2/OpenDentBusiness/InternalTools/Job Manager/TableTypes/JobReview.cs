﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness {
	///<summary>This table is not part of the general release.  User would have to add it manually.  All schema changes are done directly on our live database as needed.</summary>
	[Serializable]
	[CrudTable(IsMissingInGeneral=true,IsSynchable=true)]
	//[CrudTable(IsSynchable=true)]
	public class JobReview:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long JobReviewNum;
		///<summary>FK to job.JobNum.</summary>
		public long JobNum;
		///<summary>FK to userod.UserNum.  Links this project to the source project.</summary>
		public long ReviewerNum;
		///<summary>Date/Time the review was created.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeStamp)]
		public DateTime DateTStamp;
		///<summary>The text in this review.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string Description;
		///<summary>The status of this review.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.EnumAsString)]
		public JobReviewStatus ReviewStatus;

		///<summary></summary>
		public JobReview Copy() {
			return (JobReview)this.MemberwiseClone();
		}
	}

	public enum JobReviewStatus {
		///<summary>0 -</summary>
		Sent,
		///<summary>1 -</summary>
		Seen,
		///<summary>2 -</summary>
		UnderReview,
		///<summary>3 -</summary>
		NeedsAdditionalWork,
		///<summary>4 -</summary>
		Done,
		///<summary>5 -</summary>
		NeedsAdditionalReview
	}

}


