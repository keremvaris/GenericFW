using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using System.Reflection;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Dynamic;    
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Data;
using GenericFW;
using GenericFW.Core;

namespace GenericFW.Areas.Yonetim.GenericVM
{
	public static class CrudExtensions
	{
		private static ConcurrentDictionary<Type, TableMeta> _metaCache =
				new ConcurrentDictionary<Type, TableMeta>();
		public static IDictionary<string, object> ToDynamic(this object value)
		{
			IDictionary<string, object> expando = new ExpandoObject();

			foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
				expando.Add(property.Name, property.GetValue(value));
			expando.Add("_type", value.GetType());

			return expando;
		}

		public static void ResolveFkAttributes(Type foreignType, ColumnMeta fkColumn)
		{
			var foreignMeta = _metaCache.GetOrAdd(foreignType, AddMeta);
			fkColumn.PrimaryTable = foreignMeta;
			fkColumn.Template = "Lookup";
		}

		private static Dictionary<Type, DataType> _type2dt = new Dictionary<Type, DataType>
		{
			{ typeof(string), DataType.Text },
			{ typeof(DateTime), DataType.DateTime }
		};

		class OrderComparer : IComparer<ColumnMeta>
		{
			public int Compare(ColumnMeta x, ColumnMeta y)
			{
				var xo = x?.Display.GetOrder() ?? 0;
				var yo = y?.Display.GetOrder() ?? 0;
				if (xo < yo) return -1;
				if (xo > yo) return 1;
				return 0;
			}
		}

		private static OrderComparer defaultComparer = new OrderComparer();

		private static TableMeta AddMeta(Type _type)
		{
			var tm = new TableMeta();

			tm.Caption = _type.GetCustomAttribute<DisplayNameAttribute>();
			tm.DisplayColumn = _type.GetCustomAttribute<DisplayColumnAttribute>();
			tm.ActualTableName = _type.GetCustomAttribute<TableAttribute>()?.Name ?? _type.Name;

			List<PropertyInfo> ShowColumns = new List<PropertyInfo>();
			PropertyInfo[] columns = _type.GetProperties();

			foreach (var col in columns)
			{
				var colMeta = new ColumnMeta(col);
				tm.Add(colMeta);
			}

			var removeList = new List<ColumnMeta>();

			foreach (var colMeta in tm.Columns)
			{
				var col = colMeta.Property;
				var skipColumn = false;
				foreach (var attr in col.GetCustomAttributes())
				{
					if (attr is KeyAttribute)
					{
						tm.PrimaryColumn = col;
					}
					else if (attr is DisplayAttribute)
					{
						colMeta.Display = attr as DisplayAttribute;
					}
					else if (attr is RequiredAttribute)
					{
						colMeta.Required = true;
					}
					else if (attr is ForeignKeyAttribute)
					{
						var fkAttr = attr as ForeignKeyAttribute;
						if (fkAttr != null)
						{
							// Ilgili kolonu bul
							var fkCol = tm.Columns.First(c => c.Property.Name == (fkAttr as ForeignKeyAttribute).Name);

							ResolveFkAttributes(col.PropertyType, fkCol);
							// bu kolonu atla
							skipColumn = true;
						}
					}
					else if (attr is DataTypeAttribute)
					{
						colMeta.DataType = attr as DataTypeAttribute;
					}
					else if (attr is ScaffoldColumnAttribute)
					{
						colMeta.IsVisible = (attr as ScaffoldColumnAttribute).Scaffold;
					}
					else if (attr is UIHintAttribute)
					{
						colMeta.UIHint = (attr as UIHintAttribute);
					}
				}
				if (skipColumn)
				{
					removeList.Add(colMeta);
					continue;
				}
				colMeta.InnerType = Nullable.GetUnderlyingType(col.PropertyType);
				if (colMeta.InnerType != null)
				{
					colMeta.Required = false;
				}
				else
				{
					colMeta.InnerType = col.PropertyType;
				}
				// Eğer enum varsa
				if(colMeta.InnerType.IsEnum)
				{
					var names = Enum.GetNames(colMeta.InnerType).AsQueryable();
					var values = Enum.GetValues(colMeta.InnerType).Cast<int>().AsQueryable();

					colMeta.SetManualLookup(
						names.Zip(values, (n, v) => new ListItem { Id = Convert.ToInt32(v), Text = n }).ToList()
						);
				}

				// Datatype verilmemişse, anlamaya çalış.
				if(colMeta.DataType == null)
				{
					DataType dt;
					if(_type2dt.TryGetValue(colMeta.InnerType, out dt)) {
						colMeta.DataType = new DataTypeAttribute(dt);
					}
				}

			}
			removeList.ForEach(c => tm.Columns.Remove(c));

			// Order'a göre sırala.
			tm.Columns.Sort(defaultComparer);
			return tm;

		}

		public static GenericListVM ToListVM<T>(this List<T> values) where T : class
		{
			var _type = typeof(T);

			TableMeta meta = _metaCache.GetOrAdd(_type, AddMeta);


			var result = new GenericListVM(meta);


			result.Items = new List<IDictionary<string, object>>();

			foreach (var v in values)
				result.Items.Add(v.ToDynamic());

			return result;

		}

		public static GenericVM ToVM(this object value)
		{
			var _type = value.GetType();

			TableMeta meta = _metaCache.GetOrAdd(_type, AddMeta);


			var result = new GenericVM(meta);
			result.Data = value.ToDynamic();


			return result;
		}

        public static GenericVM ToVM(this Type type, object data)
        {
            TableMeta meta = _metaCache.GetOrAdd(type, AddMeta);


            var result = new GenericVM(meta);
            result.Data = data.ToDynamic();


            return result;
        }

        public static TableMeta Get(Type type)
        {
            return _metaCache[type];
        }
	}
}