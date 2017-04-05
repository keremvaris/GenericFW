using GenericFW.Attr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace GenericFW.Areas.Yonetim.GenericVM
{
	public class ColumnMeta
	{
		public PropertyInfo Property { get; private set; }
		public DisplayAttribute Display { get; set; }
		public bool Required { get; internal set; }
		public Type InnerType { get; internal set; }
		public TableMeta PrimaryTable { get; internal set; }
		public DataTypeAttribute DataType { get; internal set; }
		public bool IsVisible { get; internal set; } = true;
		public UIHintAttribute UIHint { get; set; }
		public string Template { get; set; }	

		public List<ListItem> ManualLookup { get; private set; }

		public ColumnMeta(PropertyInfo property)
		{
			Property = property;
		}

		public void SetManualLookup(List<ListItem> items)
		{
			ManualLookup = items;
			Template = "Enum";
		}
	}
}