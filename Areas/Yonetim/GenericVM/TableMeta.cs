using GenericFW.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GenericFW.Areas.Yonetim.GenericVM
{
	public class TableMeta
	{
		public List<ColumnMeta> Columns { get; set; }
		public PropertyInfo PrimaryColumn { get; set; }
		public PropertyInfo IdentityColumn { get; set; }
		public string ControllerName { get; set; }

		public string ActualTableName { get; set; }

		public DisplayColumnAttribute DisplayColumn { get; internal set; }
		public DisplayNameAttribute Caption { get; internal set; }

		public TableMeta()
		{
			Columns = new List<ColumnMeta>();
		}
		public void Add(ColumnMeta column)
		{
			Columns.Add(column);
		}

	}
}