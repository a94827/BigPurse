using System;
using System.Collections.Generic;
using System.Text;
using FreeLibSet.Core;
using FreeLibSet.Forms;
using FreeLibSet.Data;
using FreeLibSet.Data.Docs;
using FreeLibSet.UICore;
using System.Data;

namespace App
{
  /// <summary>
  /// Буферизация текстовых значений полей для выбора из списков
  /// </summary>
  public static class ProductBuffer
  {
    #region Информация из дерева товаров

    public class ProductData : ICloneable
    {
      #region Поля

      public PresenceType DescriptionPresence;

      public PresenceType Unit1Presence;

      /// <summary>
      /// Фиксированный список единиц измерения.
      /// Если длина списка равна 0, то можно использовать любые единицы из справочника
      /// </summary>
      public Int32[] MU1List;

      public PresenceType Unit2Presence;

      /// <summary>
      /// Фиксированный список единиц измерения.
      /// Если длина списка равна 0, то можно использовать любые единицы из справочника
      /// </summary>
      public Int32[] MU2List;

      #endregion

      #region ICloneable Members

      public ProductData Clone()
      {
        ProductData res = new ProductData();
        res.DescriptionPresence = this.DescriptionPresence;
        res.Unit1Presence = this.Unit1Presence;
        res.MU1List = this.MU1List;
        res.Unit2Presence = this.Unit2Presence;
        res.MU2List = this.MU2List;
        return res;
      }

      object ICloneable.Clone()
      {
        return Clone();
      }

      #endregion
    }

    private static ProductData _ZeroProductData = CreateZeroProductData();

    private static ProductData CreateZeroProductData()
    {
      ProductData obj = new ProductData();
      obj.DescriptionPresence = PresenceType.Optional;
      obj.Unit1Presence = PresenceType.Optional;
      obj.MU1List = DataTools.EmptyIds;
      obj.Unit2Presence = PresenceType.Optional;
      obj.MU2List = DataTools.EmptyIds;
      return obj;
    }

    private static Dictionary<Int32, ProductData> _ProductDict = new Dictionary<Int32, ProductData>();

    public static ProductData GetProductData(Int32 productId)
    {
      if (productId == 0)
        return _ZeroProductData;


      ProductData pd;
      if (!_ProductDict.TryGetValue(productId, out pd))
      {
        EFPApp.BeginWait("Просмотр дерева продуктов");
        try
        {
          IdList treeIds = new IdList(); // для отслеживания зацикливания дерева
          pd = DoInitProductData(productId, treeIds);
        }
        finally
        {
          EFPApp.EndWait();
        }
        // Добавлять в словарь не надо - это уже сделано
      }
      return pd;
    }

    /// <summary>
    /// Рекурсивный метод
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="treeIds"></param>
    /// <returns></returns>
    private static ProductData DoInitProductData(Int32 productId, IdList treeIds)
    {
      //if (productId == 0)
      //  return _ZeroProductData;
      if (treeIds.Contains(productId))
      {
        _ProductDict[productId] = _ZeroProductData;
        return _ZeroProductData; // Предотвращение зацикливания
      }

      object[] vals = ProgramDBUI.TheUI.DocProvider.GetValues("Products", productId,
        //                 0              1                2           3 
        new DBxColumns("ParentId,DescriptionPresence,Unit1Presence,Unit2Presence"));

      ProductData parentData;
      Int32 parentId = DataTools.GetInt(vals[0]);
      if (parentId == 0)
        parentData = _ZeroProductData;
      else
        parentData = DoInitProductData(parentId, treeIds);

      ProductData pd = parentData.Clone();
      PresenceType descripionPresence = DataTools.GetEnum<PresenceType>(vals[1]);
      if (descripionPresence != PresenceType.Inherited)
        pd.DescriptionPresence = descripionPresence;

      PresenceType unit1Presence = DataTools.GetEnum<PresenceType>(vals[2]);
      if (unit1Presence != PresenceType.Inherited)
        pd.Unit1Presence = unit1Presence;

      PresenceType unit2Presence = DataTools.GetEnum<PresenceType>(vals[3]);
      if (unit2Presence != PresenceType.Inherited)
        pd.Unit2Presence = unit2Presence;

      DataTable tbl;
      tbl = ProgramDBUI.TheUI.DocProvider.FillSelect("ProductMUs1", new DBxColumns("MU"),
        new AndFilter(new ValueFilter("DocId", productId), DBSSubDocType.DeletedFalseFilter));
      if (tbl.Rows.Count > 0)
        pd.MU1List = DataTools.GetIdsFromColumn(tbl, "MU");

      tbl = ProgramDBUI.TheUI.DocProvider.FillSelect("ProductMUs2", new DBxColumns("MU"),
        new AndFilter(new ValueFilter("DocId", productId), DBSSubDocType.DeletedFalseFilter));
      if (tbl.Rows.Count > 0)
        pd.MU2List = DataTools.GetIdsFromColumn(tbl, "MU");

#if DEBUG
      if (pd.DescriptionPresence == PresenceType.Inherited)
        throw new BugException("DescriptionPresence");
      if (pd.Unit1Presence == PresenceType.Inherited)
        throw new BugException("Unit1Presence");
      if (pd.Unit2Presence == PresenceType.Inherited)
        throw new BugException("Unit2Presence");
#endif

      _ProductDict[productId] = pd;
      return pd;
    }

    public static void ResetProductData()
    {
      _ProductDict.Clear();
    }

    /// <summary>
    /// Возвращает true, если поле доступно для ввода
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static bool GetColumnEnabled(Int32 productId, string columnName)
    {
      ProductData pd = GetProductData(productId);
      PresenceType prs;
      switch (columnName)
      {
        case "Description":
          prs = pd.DescriptionPresence;
          break;
        case "Quantity1":
        case "MU1":
          prs = pd.Unit1Presence;
          break;
        case "Quantity2":
        case "MU2":
          prs = pd.Unit2Presence;
          break;
        default:
          throw new ArgumentOutOfRangeException("columnName", columnName, "Неправильное имя поля");
      }

      return prs != PresenceType.Disabled;
    }

    public static string[] GetOpProductValues(Int32 productId, string columnName)
    {
      ProductData pd = GetProductData(productId);
      PresenceType prs;
      Int32[] fixedList;
      switch (columnName)
      {
        case "Description":
          prs = pd.DescriptionPresence;
          fixedList = null;
          break;
        case "MU1":
          prs = pd.Unit1Presence;
          fixedList = pd.MU1List;
          break;
        case "MU2":
          prs = pd.Unit2Presence;
          fixedList = pd.MU2List;
          break;
        default:
          throw new ArgumentOutOfRangeException("columnName", columnName, "Неправильное имя поля");
      }

      if (prs == PresenceType.Disabled)
        return DataTools.EmptyStrings;
      //if (fixedList == null)
        return DoGetOpProductValues(productId, columnName); // история
      //else
      //  return fixedList;
    }


    public static void ValidateProductValue(int productId, string columnName, string value, IUIValidableObject args)
    {
      if (args.ValidateState == UIValidateState.Error)
        return;

      ProductData pd = GetProductData(productId);
      PresenceType prs;
      Int32[] fixedList;
      switch (columnName)
      {
        case "Description":
          prs = pd.DescriptionPresence;
          fixedList = null;
          break;
        case "MU1":
          prs = pd.Unit1Presence;
          fixedList = pd.MU1List;
          break;
        case "MU2":
          prs = pd.Unit2Presence;
          fixedList = pd.MU2List;
          break;
        default:
          throw new ArgumentOutOfRangeException("columnName", columnName, "Неправильное имя поля");
      }

      if (String.IsNullOrEmpty(value))
      {
        switch (prs)
        { 
          case PresenceType.Required:
            args.SetError("Поле должно быть заполнено");
            break;
          case PresenceType.WarningIfNone:
            args.SetWarning("Поле обычно должно быть заполнено");
            break;
        }
        return;
      }

      if (prs == PresenceType.Disabled)
      {
        args.SetError("Поле не должно заполняться");
        return;
      }

      /*
      if (fixedList != null)
      {
        int p = Array.IndexOf<string>(fixedList, value);
        if (p < 0)
        {
          args.SetError("Значение должно быть выбрано из списка. Ввод других значений не разрешен");
          return;
        }
      } */

      /*
      switch (columnName)
      {
        case "Unit1":
        case "Unit2":
          string errorText;
          if (!Tools.IsValidUnit(value, out errorText))
            args.SetError(errorText);
          break;
      } */
    }

    #endregion

    #region Список ранее использовавшихся значений

    /// <summary>
    /// Словарь значений.
    /// Ключ "ProductId|ИмяПоля".
    /// Значение - отсортированный список строк для выбора.
    /// </summary>
    private static Dictionary<string, string[]> _OpProductDict = new Dictionary<string, string[]>();

    private static string GetOpProductKey(Int32 productId, string columnName)
    {
      return StdConvert.ToString(productId) + "|" + columnName;
    }

    /// <summary>
    /// Получить значения для выпадающего списка в редакторе поддокумента "OperationProducts"
    /// </summary>
    /// <param name="productId">Выбранный идентификатор товара</param>
    /// <param name="columnName">Имя поля ("Description", "Unit1" или "Unit2")</param>
    /// <returns>Список для выбора или пустой список</returns>
    private static string[] DoGetOpProductValues(Int32 productId, string columnName)
    {
      if (productId == 0)
        return DataTools.EmptyStrings;

      string[] a;
      if (!_OpProductDict.TryGetValue(GetOpProductKey(productId, columnName), out a))
      {
        EFPApp.BeginWait("Получение списка значений");
        try
        {
          DBxFilter[] filters = new DBxFilter[3];
          filters[0] = new ValueFilter("Product", productId);
          filters[1] = DBSSubDocType.DeletedFalseFilter;
          filters[2] = DBSSubDocType.DocIdDeletedFalseFilter;
          a = ProgramDBUI.TheUI.DocProvider.GetUniqueStringValues("OperationProducts", columnName,
            AndFilter.FromArray(filters));

          #region Оставляем только правильные единицы

          /*
          List<string> lst = null;
          for (int i = 0; i < a.Length; i++)
          {
            if (Tools.IsValidUnit(a[i]))
            {
              if (lst != null)
                lst.Add(a[i]);
            }
            else
            {
              if (lst == null)
              {
                lst = new List<string>(a.Length - 1);
                for (int j = 0; j < i; j++)
                  lst.Add(a[j]);
              }
            }
          }

          if (lst != null)
            a = lst.ToArray();
          */
          #endregion
        }
        finally
        {
          EFPApp.EndWait();
        }

        _OpProductDict[GetOpProductKey(productId, columnName)] = a;
      }
      return a;
    }

    public static void AddOpProductValues(Int32 productId, string columnName, string value)
    {
      if (productId == 0)
        return;
      if (String.IsNullOrEmpty(value))
        return;
      string[] a1;
      if (!_OpProductDict.TryGetValue(GetOpProductKey(productId, columnName), out a1))
        return; // не загружали список, и не надо.

      if (Array.IndexOf<string>(a1, value) >= 0)
        return; // Уже есть

      string[] a2 = a1;
      Array.Resize<string>(ref a2, a1.Length + 1);
      a2[a2.Length - 1] = value;
      Array.Sort<string>(a2);
      _OpProductDict[GetOpProductKey(productId, columnName)] = a2;
    }

    #endregion
  }
}
