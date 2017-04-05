using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericFW.Areas.Yonetim.GenericVM
{
	public class TemplateItemModel
	{
		public GenericVM Model { get; set; }
        public GenericListVM ModelList { get; set; }
        public ColumnMeta CurrentColumn { get; set; }
		public RenderMode Mode { get; set; }
	}

	public enum RenderMode
	{
		Readonly,
		Create,
		Edit,
		Delete
	}
}